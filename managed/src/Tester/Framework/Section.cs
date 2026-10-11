using SwiftlyS2.Shared;

namespace Tester.Framework;

public abstract class Section( ISwiftlyCore core )
{
    protected ISwiftlyCore Core { get; } = core;

    public abstract string Name { get; }

    public virtual Task Test( TestContext t ) => Task.CompletedTask;

    public virtual bool ProfileInBackground => false;

    public virtual Task Profile( ProfileContext p ) => Task.CompletedTask;
}
