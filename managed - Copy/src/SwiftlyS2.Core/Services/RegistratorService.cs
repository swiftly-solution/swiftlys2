using Microsoft.Extensions.DependencyInjection;
using SwiftlyS2.Core.AttributeParsers;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Services;

namespace SwiftlyS2.Core.Services;

internal class RegistratorService : IRegistratorService {

  public RegistratorService(SwiftlyCore core) {
    _Core = core;
  }

  private SwiftlyCore _Core { get; }

  public void Register(object instance) {

    _Core.CommandService.ParseFromObject(instance);
    _Core.EventSubscriber.ParseFromObject(instance);
    _Core.GameEventService.ParseFromObject(instance);
    _Core.NetMessageService.ParseFromObject(instance);

  }
}