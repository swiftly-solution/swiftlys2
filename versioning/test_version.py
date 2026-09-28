from __future__ import annotations

import subprocess
import sys
import tempfile
import unittest
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "versioning"))

import version  # noqa: E402


class GitRepository:
    def __init__(self, path: Path) -> None:
        self.path = path
        self.run("init", "--initial-branch=master")
        self.run("config", "user.name", "Version Test")
        self.run("config", "user.email", "version-test@example.invalid")
        self.run("config", "commit.gpgsign", "false")
        self.run("config", "tag.gpgsign", "false")

    def run(self, *arguments: str) -> str:
        process = subprocess.run(
            ["git", *arguments],
            cwd=self.path,
            check=True,
            capture_output=True,
            text=True,
            encoding="utf-8",
        )
        return process.stdout.strip()

    def commit(self, name: str, content: str, message: str) -> str:
        target = self.path / name
        target.parent.mkdir(parents=True, exist_ok=True)
        target.write_text(content, encoding="utf-8")
        self.run("add", name)
        self.run("commit", "--quiet", "-m", message)
        return self.run("rev-parse", "HEAD")

    def tag(self, name: str) -> None:
        self.run("tag", "-a", name, "-m", f"Release {name}")


class VersionTests(unittest.TestCase):
    def setUp(self) -> None:
        self.temporary_directory = tempfile.TemporaryDirectory()
        self.repo = GitRepository(Path(self.temporary_directory.name))
        self.repo.commit("base.txt", "base\n", "Initial release")
        self.repo.tag("v1.4.5")

    def tearDown(self) -> None:
        self.temporary_directory.cleanup()

    def calculate(self, branch: str) -> version.VersionResult:
        return version.calculate_version(
            self.repo.path,
            branch=branch,
            main_refs=("master",),
        )

    def test_master_cherry_pick_increments_stable_patch(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "beta")
        fix = self.repo.commit("fix.txt", "fixed\n", "fix: urgent beta fix")
        self.repo.tag("v1.4.6-beta.1")

        self.repo.run("switch", "--quiet", "master")
        self.repo.run("cherry-pick", "--quiet", fix)

        result = self.calculate("master")
        self.assertEqual("1.4.6", result.semVer)
        self.assertEqual("", result.preReleaseLabel)

    def test_beta_resets_after_cherry_picked_stable_is_merged(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "beta")
        self.repo.commit("feature-a.txt", "a\n", "feat: beta work")
        self.repo.tag("v1.4.6-beta.1")
        self.repo.commit("feature-b.txt", "b\n", "feat: more beta work")
        self.repo.tag("v1.4.6-beta.2")
        fix = self.repo.commit("fix.txt", "fixed\n", "fix: urgent beta fix")
        self.repo.tag("v1.4.6-beta.3")
        self.repo.commit("feature-c.txt", "c\n", "feat: long-running work")
        self.repo.tag("v1.4.6-beta.19")

        self.repo.run("switch", "--quiet", "master")
        self.repo.run("cherry-pick", "--quiet", fix)
        self.assertEqual("1.4.6", self.calculate("master").semVer)
        self.repo.tag("v1.4.6")

        self.repo.run("switch", "--quiet", "beta")
        self.repo.run("merge", "--quiet", "--no-ff", "master", "-m", "Merge master")

        first = self.calculate("beta")
        self.assertEqual("1.4.7-beta.1", first.semVer)
        self.repo.tag("v1.4.7-beta.1")
        self.assertEqual("1.4.7-beta.1", self.calculate("beta").semVer)

        self.repo.commit("after-merge.txt", "next\n", "feat: next beta change")
        self.assertEqual("1.4.7-beta.2", self.calculate("beta").semVer)

    def test_beta_sequence_uses_highest_matching_tag_not_commit_count(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "beta")
        self.repo.commit("one.txt", "one\n", "feat: one")
        self.repo.tag("v1.4.6-beta.2")
        for number in range(5):
            self.repo.commit(
                "work.txt",
                f"work {number}\n",
                f"feat: untagged work {number}",
            )
        self.repo.tag("v1.4.6-beta.19")
        self.repo.commit("next.txt", "next\n", "feat: next")

        self.assertEqual("1.4.6-beta.20", self.calculate("beta").semVer)

    def test_higher_long_running_beta_series_is_not_downgraded(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "beta")
        self.repo.commit("feature.txt", "future\n", "+semver: minor")
        self.repo.tag("v1.5.0-beta.7")

        self.repo.run("switch", "--quiet", "master")
        self.repo.commit("hotfix.txt", "hotfix\n", "fix: stable hotfix")
        self.repo.tag("v1.4.6")

        self.repo.run("switch", "--quiet", "beta")
        self.repo.run("merge", "--quiet", "--no-ff", "master", "-m", "Merge master")

        self.assertEqual("1.5.0-beta.8", self.calculate("beta").semVer)

    def test_master_advances_beta_only_after_stable_tag_exists(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "beta")
        self.repo.commit("beta.txt", "beta\n", "feat: beta work")
        self.repo.tag("v1.4.6-beta.9")

        self.repo.run("switch", "--quiet", "master")
        self.repo.commit("hotfix.txt", "hotfix\n", "fix: stable hotfix")
        self.assertEqual("1.4.6", self.calculate("master").semVer)

        self.repo.run("switch", "--quiet", "beta")
        self.repo.run("merge", "--quiet", "--no-ff", "master", "-m", "Merge master")

        self.assertEqual("1.4.6-beta.10", self.calculate("beta").semVer)

        self.repo.run("switch", "--quiet", "master")
        self.repo.tag("v1.4.6")
        self.repo.run("switch", "--quiet", "beta")

        self.assertEqual("1.4.7-beta.1", self.calculate("beta").semVer)

    def test_other_branch_keeps_gitversion_style_label_and_distance(self) -> None:
        self.repo.run("switch", "--quiet", "-c", "feature/menu-system")
        self.repo.commit("one.txt", "one\n", "feat: first change")
        self.repo.commit("two.txt", "two\n", "feat: second change")

        result = self.calculate("feature/menu-system")
        self.assertEqual("1.4.6-feature-menu-system.2", result.semVer)
        self.assertEqual("feature-menu-system", result.preReleaseLabel)

    def test_semver_commit_message_can_select_minor_release(self) -> None:
        self.repo.commit("feature.txt", "feature\n", "feat: API +semver: minor")
        self.assertEqual("1.5.0", self.calculate("master").semVer)


if __name__ == "__main__":
    unittest.main()
