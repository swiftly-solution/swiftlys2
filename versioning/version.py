#!/usr/bin/env python3
"""Calculate SwiftlyS2 versions from reachable Git tags.

This is intentionally repository-specific.  It replaces the subset of
GitVersion used by the build workflow while making beta numbering tag-based:

* master: the next stable version after the newest reachable stable tag;
* beta:   the next beta tag after the newest reachable stable tag;
* other:  GitVersion-compatible branch-labelled preview versions.

Stable tags use ``vMAJOR.MINOR.PATCH`` and beta tags use
``vMAJOR.MINOR.PATCH-beta.NUMBER``.  Existing tags on HEAD are returned
unchanged so rerunning a build is idempotent.
"""

from __future__ import annotations

import argparse
import json
import os
import re
import subprocess
import sys
from dataclasses import asdict, dataclass
from pathlib import Path
from typing import Iterable, Sequence


STABLE_TAG_RE = re.compile(r"^v?(\d+)\.(\d+)\.(\d+)$")
BETA_TAG_RE = re.compile(r"^v?(\d+)\.(\d+)\.(\d+)-beta\.(\d+)$")
MAJOR_BUMP_RE = re.compile(r"\+semver:\s*(?:breaking|major)\b", re.IGNORECASE)
MINOR_BUMP_RE = re.compile(r"\+semver:\s*(?:feature|minor)\b", re.IGNORECASE)
PATCH_BUMP_RE = re.compile(r"\+semver:\s*(?:fix|patch)\b", re.IGNORECASE)
PULL_REQUEST_RE = re.compile(
    r"^(?:refs/)?(?:pull|pull-requests|pr)[/-](\d+)(?:[/-].*)?$",
    re.IGNORECASE,
)


class VersionError(RuntimeError):
    """Raised when the repository cannot provide a deterministic version."""


@dataclass(frozen=True, order=True)
class CoreVersion:
    major: int
    minor: int
    patch: int

    def bump(self, part: str) -> "CoreVersion":
        if part == "major":
            return CoreVersion(self.major + 1, 0, 0)
        if part == "minor":
            return CoreVersion(self.major, self.minor + 1, 0)
        if part == "patch":
            return CoreVersion(self.major, self.minor, self.patch + 1)
        raise VersionError(f"Unsupported version increment: {part}")

    def __str__(self) -> str:
        return f"{self.major}.{self.minor}.{self.patch}"


@dataclass(frozen=True)
class StableTag:
    name: str
    version: CoreVersion
    commit: str


@dataclass(frozen=True)
class BetaTag:
    name: str
    version: CoreVersion
    number: int
    commit: str


@dataclass(frozen=True)
class StableBase:
    version: CoreVersion
    commit: str


@dataclass(frozen=True)
class VersionResult:
    fullSemVer: str
    semVer: str
    informationalVersion: str
    preReleaseLabel: str
    branchName: str
    sha: str


def run_git(
    repo: Path,
    arguments: Sequence[str],
    *,
    check: bool = True,
) -> subprocess.CompletedProcess[str]:
    process = subprocess.run(
        ["git", *arguments],
        cwd=repo,
        check=False,
        capture_output=True,
        text=True,
        encoding="utf-8",
    )
    if check and process.returncode != 0:
        detail = process.stderr.strip() or process.stdout.strip()
        raise VersionError(f"git {' '.join(arguments)} failed: {detail}")
    return process


def git_output(repo: Path, *arguments: str) -> str:
    return run_git(repo, arguments).stdout.strip()


def resolve_commit(repo: Path, ref: str) -> str:
    return git_output(repo, "rev-parse", f"{ref}^{{commit}}")


def ref_exists(repo: Path, ref: str) -> bool:
    return run_git(
        repo,
        ["rev-parse", "--verify", "--quiet", f"{ref}^{{commit}}"],
        check=False,
    ).returncode == 0


def is_ancestor(repo: Path, ancestor: str, descendant: str = "HEAD") -> bool:
    return run_git(
        repo,
        ["merge-base", "--is-ancestor", ancestor, descendant],
        check=False,
    ).returncode == 0


def reachable_tag_names(repo: Path, ref: str = "HEAD") -> list[str]:
    output = git_output(repo, "tag", "--merged", ref)
    return [line for line in output.splitlines() if line]


def tag_names_at(repo: Path, ref: str = "HEAD") -> list[str]:
    output = git_output(repo, "tag", "--points-at", ref)
    return [line for line in output.splitlines() if line]


def parse_stable_tag(repo: Path, name: str) -> StableTag | None:
    match = STABLE_TAG_RE.fullmatch(name)
    if not match:
        return None
    version = CoreVersion(*(int(part) for part in match.groups()))
    return StableTag(name=name, version=version, commit=resolve_commit(repo, name))


def parse_beta_tag(repo: Path, name: str) -> BetaTag | None:
    match = BETA_TAG_RE.fullmatch(name)
    if not match:
        return None
    major, minor, patch, number = (int(part) for part in match.groups())
    return BetaTag(
        name=name,
        version=CoreVersion(major, minor, patch),
        number=number,
        commit=resolve_commit(repo, name),
    )


def stable_tags(repo: Path, ref: str = "HEAD") -> list[StableTag]:
    return [
        parsed
        for name in reachable_tag_names(repo, ref)
        if (parsed := parse_stable_tag(repo, name)) is not None
    ]


def beta_tags(repo: Path, ref: str = "HEAD") -> list[BetaTag]:
    return [
        parsed
        for name in reachable_tag_names(repo, ref)
        if (parsed := parse_beta_tag(repo, name)) is not None
    ]


def commit_distance(repo: Path, ancestor: str, descendant: str = "HEAD") -> int:
    output = git_output(repo, "rev-list", "--count", f"{ancestor}..{descendant}")
    return int(output)


def repository_root_commit(repo: Path, ref: str = "HEAD") -> str:
    roots = git_output(repo, "rev-list", "--max-parents=0", ref).splitlines()
    if not roots:
        raise VersionError("The repository has no commits")
    return roots[-1]


def newest_stable_tag(repo: Path, ref: str = "HEAD") -> StableTag | None:
    tags = stable_tags(repo, ref)
    if not tags:
        return None
    return max(
        tags,
        key=lambda tag: (tag.version, -commit_distance(repo, tag.commit, ref)),
    )


def commit_messages(repo: Path, ancestor: str, descendant: str) -> str:
    if resolve_commit(repo, ancestor) == resolve_commit(repo, descendant):
        return ""
    return git_output(repo, "log", "--format=%B%x00", f"{ancestor}..{descendant}")


def requested_increment(messages: str, default: str = "patch") -> str:
    if MAJOR_BUMP_RE.search(messages):
        return "major"
    if MINOR_BUMP_RE.search(messages):
        return "minor"
    if PATCH_BUMP_RE.search(messages):
        return "patch"
    return default


def effective_stable_version(repo: Path, ref: str) -> StableBase:
    ref_commit = resolve_commit(repo, ref)
    source = newest_stable_tag(repo, ref)
    if source is None:
        root = repository_root_commit(repo, ref)
        if ref_commit == root:
            return StableBase(CoreVersion(0, 1, 0), root)
        messages = commit_messages(repo, root, ref)
        return StableBase(
            CoreVersion(0, 1, 0).bump(requested_increment(messages)),
            ref_commit,
        )
    if source.commit == ref_commit:
        return StableBase(source.version, ref_commit)
    messages = commit_messages(repo, source.commit, ref)
    return StableBase(
        source.version.bump(requested_increment(messages)),
        ref_commit,
    )


def merged_stable_base(
    repo: Path,
    main_refs: Iterable[str],
    ref: str = "HEAD",
    *,
    include_untagged_main: bool = True,
) -> StableBase:
    candidates: list[StableBase] = []
    source = newest_stable_tag(repo, ref)
    if source is not None:
        candidates.append(StableBase(source.version, source.commit))

    if include_untagged_main:
        for main_ref in main_refs:
            if not ref_exists(repo, main_ref) or not is_ancestor(repo, main_ref, ref):
                continue
            candidates.append(effective_stable_version(repo, main_ref))

    if not candidates:
        root = repository_root_commit(repo, ref)
        candidates.append(StableBase(CoreVersion(0, 1, 0), root))

    return max(
        candidates,
        key=lambda candidate: (
            candidate.version,
            -commit_distance(repo, candidate.commit, ref),
        ),
    )


def normalize_branch_name(branch: str) -> str:
    branch = re.sub(r"^refs/heads/", "", branch)
    normalized = re.sub(r"[^0-9A-Za-z-]+", "-", branch).strip("-")
    return normalized or "unknown"


def detect_branch(repo: Path, explicit: str | None = None) -> str:
    if explicit:
        return explicit

    github_ref = os.environ.get("GITHUB_REF", "")
    pull_request = re.fullmatch(r"refs/pull/(\d+)/merge", github_ref)
    if pull_request:
        return f"pull/{pull_request.group(1)}/merge"

    github_ref_name = os.environ.get("GITHUB_REF_NAME")
    if github_ref_name:
        return github_ref_name

    symbolic = run_git(
        repo,
        ["symbolic-ref", "--quiet", "--short", "HEAD"],
        check=False,
    )
    if symbolic.returncode == 0 and symbolic.stdout.strip():
        return symbolic.stdout.strip()

    github_head_ref = os.environ.get("GITHUB_HEAD_REF")
    if github_head_ref:
        return github_head_ref

    raise VersionError(
        "Cannot determine the branch from detached HEAD; pass --branch explicitly"
    )


def exact_version_on_head(
    repo: Path,
    branch: str,
    sha: str,
) -> VersionResult | None:
    names = tag_names_at(repo)
    stable = [
        parsed
        for name in names
        if (parsed := parse_stable_tag(repo, name)) is not None
    ]
    betas = [
        parsed
        for name in names
        if (parsed := parse_beta_tag(repo, name)) is not None
    ]

    if branch in {"master", "main"}:
        if stable:
            selected_stable = max(stable, key=lambda tag: tag.version)
            return make_result(selected_stable.version, "", None, branch, sha)
        # A cherry-pick can be byte-for-byte identical to a beta commit when
        # both commits have the same parent.  A beta tag on that object must
        # never turn a master build into a prerelease.
        return None
    if branch == "beta" and betas:
        selected = max(betas, key=lambda tag: (tag.version, tag.number))
        return make_result(selected.version, "beta", selected.number, branch, sha)
    if stable:
        selected_stable = max(stable, key=lambda tag: tag.version)
        return make_result(selected_stable.version, "", None, branch, sha)
    if betas:
        selected_beta = max(betas, key=lambda tag: (tag.version, tag.number))
        return make_result(
            selected_beta.version,
            "beta",
            selected_beta.number,
            branch,
            sha,
        )
    return None


def make_result(
    core: CoreVersion,
    label: str,
    number: int | None,
    branch: str,
    sha: str,
) -> VersionResult:
    semver = str(core)
    if label:
        if number is None:
            raise VersionError("A prerelease label requires a sequence number")
        semver = f"{semver}-{label}.{number}"
    normalized_branch = normalize_branch_name(branch)
    informational = f"{semver}+Branch.{normalized_branch}.Sha.{sha}"
    return VersionResult(
        fullSemVer=semver,
        semVer=semver,
        informationalVersion=informational,
        preReleaseLabel=label,
        branchName=branch,
        sha=sha,
    )


def calculate_version(
    repo: Path,
    *,
    branch: str | None = None,
    main_refs: Sequence[str] = ("origin/master", "master", "origin/main", "main"),
) -> VersionResult:
    repo = Path(git_output(repo, "rev-parse", "--show-toplevel"))
    sha = resolve_commit(repo, "HEAD")
    branch_name = detect_branch(repo, branch)

    exact = exact_version_on_head(repo, branch_name, sha)
    if exact is not None:
        return exact

    if branch_name in {"master", "main"}:
        stable = effective_stable_version(repo, "HEAD")
        return make_result(stable.version, "", None, branch_name, sha)

    # A stable release is only real to beta once its tag exists.  Master must
    # infer its pending version so the release workflow knows which tag to
    # create, but using that inferred version here would advance beta twice
    # when a master build is intentionally skipped.
    base = merged_stable_base(
        repo,
        main_refs,
        include_untagged_main=branch_name != "beta",
    )
    history_messages = commit_messages(repo, base.commit, "HEAD")
    next_from_stable = base.version.bump(requested_increment(history_messages))
    historical_betas = beta_tags(repo)
    highest_beta_core = max(
        (tag.version for tag in historical_betas),
        default=next_from_stable,
    )
    target = max(next_from_stable, highest_beta_core)

    if branch_name == "beta":
        previous_numbers = [
            tag.number for tag in historical_betas if tag.version == target
        ]
        number = max(previous_numbers, default=0) + 1
        return make_result(target, "beta", number, branch_name, sha)

    pull_request = PULL_REQUEST_RE.fullmatch(branch_name)
    if pull_request:
        label = f"PullRequest{pull_request.group(1)}"
    else:
        label = normalize_branch_name(branch_name)
    number = max(commit_distance(repo, base.commit), 1)
    return make_result(target, label, number, branch_name, sha)


def write_github_outputs(path: Path, result: VersionResult) -> None:
    with path.open("a", encoding="utf-8", newline="\n") as output:
        output.write(f"fullSemVer={result.fullSemVer}\n")
        output.write(f"semVer={result.semVer}\n")
        output.write(f"informationalVersion={result.informationalVersion}\n")
        output.write(f"preReleaseLabel={result.preReleaseLabel}\n")


def parse_arguments(argv: Sequence[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--repo",
        type=Path,
        default=Path.cwd(),
        help="Git repository to inspect (default: current directory)",
    )
    parser.add_argument(
        "--branch",
        help="Branch name override, required for an unrecognised detached HEAD",
    )
    parser.add_argument(
        "--main-ref",
        action="append",
        dest="main_refs",
        help="Candidate main ref; may be supplied more than once",
    )
    parser.add_argument(
        "--github-output",
        type=Path,
        help="Append GitHub Actions outputs to this file",
    )
    parser.add_argument(
        "--show-variable",
        choices=(
            "FullSemVer",
            "SemVer",
            "InformationalVersion",
            "PreReleaseLabel",
        ),
        help="Print only one GitVersion-compatible variable",
    )
    return parser.parse_args(argv)


def main(argv: Sequence[str] | None = None) -> int:
    arguments = parse_arguments(argv or sys.argv[1:])
    try:
        result = calculate_version(
            arguments.repo,
            branch=arguments.branch,
            main_refs=tuple(arguments.main_refs)
            if arguments.main_refs
            else ("origin/master", "master", "origin/main", "main"),
        )
        if arguments.github_output:
            write_github_outputs(arguments.github_output, result)
        if arguments.show_variable:
            variable = arguments.show_variable[0].lower() + arguments.show_variable[1:]
            print(getattr(result, variable))
        else:
            print(json.dumps(asdict(result), indent=2))
        return 0
    except VersionError as error:
        print(f"version.py: {error}", file=sys.stderr)
        return 2


if __name__ == "__main__":
    raise SystemExit(main())
