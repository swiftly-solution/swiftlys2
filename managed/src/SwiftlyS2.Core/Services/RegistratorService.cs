using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.AttributeParsers;

namespace SwiftlyS2.Core.Services;

internal class RegistratorService : IRegistratorService
{
    private readonly SwiftlyCore core;

    public RegistratorService( SwiftlyCore core )
    {
        this.core = core;
    }

    public void Register( object instance )
    {
        core.CommandService.ParseFromObject(instance);
        core.EventSubscriber.ParseFromObject(instance);
        core.GameEventService.ParseFromObject(instance);
        core.NetMessageService.ParseFromObject(instance);
        core.EntitySystemService.ParseFromObject(instance);
    }
}