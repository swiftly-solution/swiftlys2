from pathlib import Path
import os
import shlex
import subprocess
import tempfile


test_root = Path(__file__).resolve().parent
source_root = test_root.parent.parent


def extract(relative_path, signature):
    source = (source_root / relative_path).read_text()
    start = source.index(signature)
    opening = source.index("{", start)
    while 0 <= source.find(";", start) < opening:
        start = source.index(signature, start + len(signature))
        opening = source.index("{", start)
    depth = 0
    for position in range(opening, len(source)):
        if source[position] == "{":
            depth += 1
        elif source[position] == "}":
            depth -= 1
            if depth == 0:
                return source[start:position + 1]
    raise RuntimeError(f"Unterminated method: {signature}")


console_path = "src/engine/consoleoutput/consoleoutput.cpp"
console_methods = [
    "bool CConsoleOutput::ProcessLogMessage(",
    "bool CConsoleOutput::FilterOrQueueMessage(",
    "void CConsoleOutput::DispatchWorker()",
    "void CConsoleOutput::Initialize()",
    "void CConsoleOutput::Shutdown()",
    "void CConsoleOutput::ToggleFilter()",
    "bool CConsoleOutput::IsEnabled()",
    "uint64_t CConsoleOutput::AddConsoleListener(",
    "void CConsoleOutput::RemoveConsoleListener(",
    "void CConsoleOutput::DispatchQueuedListeners()",
]
replacements = {
    "CONSOLE_SCOPE": extract(console_path, "struct ConsoleDispatchScope") + ";",
    "CONSOLE_METHODS": "\n\n".join(extract(console_path, signature) for signature in console_methods),
    "MAP_LOAD": extract("src/core/entrypoint.cpp", "bool LoopInitHook("),
    "COMMAND_METHODS": "\n\n".join(
        extract("src/server/commands/manager.cpp", signature)
        for signature in [
            "bool CServerCommands::ResolveCommandName(",
            "uint64_t CServerCommands::RegisterCommand(",
            "void CServerCommands::UnregisterCommand(",
            "bool CServerCommands::IsCommandRegistered(",
        ]
    ),
    "EVENT_METHODS": "\n\n".join(
        extract("src/engine/gameevents/gameevents.cpp", signature)
        for signature in [
            "void CEventManager::OnServerStartup()",
            "void CEventManager::QueueListener(",
            "void CEventManager::DrainEngineListeners()",
        ]
    ),
}

fixture = (test_root / "callbacks.cpp.in").read_text()
for name, body in replacements.items():
    fixture = fixture.replace("@" + name + "@", body)

compiler = shlex.split(os.environ.get("CXX", "g++"))
with tempfile.TemporaryDirectory(prefix="swiftlys2-native-regressions-") as temporary:
    build_root = Path(temporary)
    callbacks = build_root / "callbacks.cpp"
    callbacks.write_text(fixture)
    for name, source in [("queue_mutex", test_root / "queue_mutex.cpp"), ("callbacks", callbacks)]:
        binary = build_root / name
        subprocess.run(
            compiler + ["-std=c++20", "-O2", "-pthread", "-Wall", "-Wextra", "-Werror", "-I", str(source_root / "src"), str(source), "-o", str(binary)],
            check=True,
        )
        subprocess.run([str(binary)], check=True, timeout=15)
