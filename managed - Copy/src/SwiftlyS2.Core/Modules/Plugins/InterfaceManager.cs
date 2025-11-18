using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Shared;

namespace SwiftlyS2.Core.Modules.Plugins;

internal class InterfaceManager : IInterfaceManager, IDisposable
{
  private ServiceCollection _ServiceCollection { get; set; } = new();
  private ServiceProvider? _ServiceProvider { get; set; }
  private List<string> _RegisteredStrings { get; init; } = new();
  public void AddSharedInterface<TInterface, TImpl>(string key, TImpl implInstance) 
      where TInterface : class
      where TImpl : class, TInterface
  {
    if (_RegisteredStrings.Contains(key))
    {
      throw new Exception($"Interface {key} is already registered.");
    }
    _RegisteredStrings.Add(key);
    _ServiceCollection.AddKeyedSingleton<TInterface>(key, implInstance);
  }

  public bool HasSharedInterface(string key)
  {
    return _RegisteredStrings.Contains(key);
  }

  public TInterface GetSharedInterface<TInterface>(string key) where TInterface : class
  {
    if (_ServiceProvider == null)
    {
      throw new Exception("InterfaceManager is not built.");
    }
    return _ServiceProvider!.GetRequiredKeyedService<TInterface>(key);
  }

  public void Build()
  {
    _ServiceProvider = _ServiceCollection.BuildServiceProvider();
  }

  public void Dispose()
  {
    _ServiceProvider?.Dispose();
    _ServiceCollection = new();
    _RegisteredStrings.Clear();
  }
}