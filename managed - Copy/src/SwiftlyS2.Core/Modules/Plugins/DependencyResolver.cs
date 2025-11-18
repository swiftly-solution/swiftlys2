using Mono.Cecil;
using Microsoft.Extensions.Logging;

namespace SwiftlyS2.Core.Modules.Plugins;

internal class DependencyResolver
{
    private readonly ILogger _logger;
    private readonly Dictionary<string, List<string>> _dependencyGraph = new();
    private readonly Dictionary<string, string> _pluginPaths = new();

    public DependencyResolver( ILogger logger )
    {
        _logger = logger;
    }

    private void ReadDependencies( string pluginDir, ref Dictionary<string, string> exportAssemblies )
    {
        var exportDir = Path.Combine(pluginDir, "resources", "exports");
        if (Directory.Exists(exportDir))
        {
            var exportFiles = Directory.GetFiles(exportDir, "*.dll");
            foreach (var exportFile in exportFiles)
            {
                try
                {
                    using var assembly = AssemblyDefinition.ReadAssembly(exportFile);
                    var assemblyName = assembly.Name.Name;
                    exportAssemblies[assemblyName] = exportFile;
                    _pluginPaths[assemblyName] = exportFile;

                    if (!_dependencyGraph.ContainsKey(assemblyName))
                    {
                        _dependencyGraph[assemblyName] = new List<string>();
                    }

                    _logger.LogDebug($"Found export assembly: {assemblyName} at {exportFile}");
                }
                catch (Exception ex)
                {
                    if (!GlobalExceptionHandler.Handle(ex)) return;
                    _logger.LogWarning(ex, $"Failed to read assembly {exportFile}");
                }
            }
        }
    }

    private void PopulateAssemblies( string startDirectory, ref Dictionary<string, string> exportAssemblies )
    {
        var pluginDirs = Directory.GetDirectories(startDirectory);
        foreach (var pluginDir in pluginDirs)
        {
            var dirName = Path.GetFileName(pluginDir);
            if (dirName.StartsWith("[") && dirName.EndsWith("]")) PopulateAssemblies(pluginDir, ref exportAssemblies);
            else ReadDependencies(pluginDir, ref exportAssemblies);
        }
    }

    public void AnalyzeDependencies( string startDirectory )
    {
        _dependencyGraph.Clear();
        _pluginPaths.Clear();

        var exportAssemblies = new Dictionary<string, string>(); // assemblyName -> path

        PopulateAssemblies(startDirectory, ref exportAssemblies);

        foreach (var (assemblyName, assemblyPath) in exportAssemblies)
        {
            try
            {
                using var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
                var dependencies = new List<string>();

                foreach (var reference in assembly.MainModule.AssemblyReferences)
                {
                    var refName = reference.Name;

                    if (exportAssemblies.ContainsKey(refName))
                    {
                        dependencies.Add(refName);
                        _logger.LogDebug($"{assemblyName} depends on {refName}");
                    }
                }

                _dependencyGraph[assemblyName] = dependencies;
            }
            catch (Exception ex)
            {
                if (!GlobalExceptionHandler.Handle(ex)) return;
                _logger.LogWarning(ex, $"Failed to analyze dependencies for {assemblyName}");
            }
        }
    }

    public List<string> GetLoadOrder()
    {
        var result = new List<string>();
        var visited = new HashSet<string>();
        var visiting = new HashSet<string>();

        foreach (var assembly in _dependencyGraph.Keys)
        {
            if (!visited.Contains(assembly))
            {
                TopologicalSort(assembly, visited, visiting, result);
            }
        }

        return result.Select(name => _pluginPaths[name]).ToList();
    }

    private void TopologicalSort(
        string assembly,
        HashSet<string> visited,
        HashSet<string> visiting,
        List<string> result )
    {
        if (visiting.Contains(assembly))
        {
            var cycle = BuildCyclePath(assembly, visiting);
            throw new InvalidOperationException(
                $"Circular dependency detected: {cycle}");
        }

        if (visited.Contains(assembly))
        {
            return;
        }

        visiting.Add(assembly);

        if (_dependencyGraph.TryGetValue(assembly, out var dependencies))
        {
            foreach (var dependency in dependencies)
            {
                TopologicalSort(dependency, visited, visiting, result);
            }
        }

        visiting.Remove(assembly);
        visited.Add(assembly);
        result.Add(assembly);
    }
    private string BuildCyclePath( string start, HashSet<string> visiting )
    {
        var path = new List<string> { start };
        var current = start;

        if (_dependencyGraph.TryGetValue(current, out var deps))
        {
            foreach (var dep in deps)
            {
                if (visiting.Contains(dep))
                {
                    path.Add(dep);
                    if (dep == start)
                    {
                        break;
                    }
                    current = dep;
                }
            }
        }

        return string.Join(" -> ", path);
    }

    public string GetDependencyGraphVisualization()
    {
        var lines = new List<string> { "Dependency Graph:" };

        foreach (var (assembly, dependencies) in _dependencyGraph.OrderBy(x => x.Key))
        {
            if (dependencies.Any())
            {
                lines.Add($"  {assembly} -> [{string.Join(", ", dependencies)}]");
            }
            else
            {
                lines.Add($"  {assembly} (no dependencies)");
            }
        }

        return string.Join(Environment.NewLine, lines);
    }
}

