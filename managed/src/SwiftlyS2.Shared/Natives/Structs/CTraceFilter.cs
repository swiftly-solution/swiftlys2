using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Explicit, Pack = 8, Size = 64)]
public struct CTraceFilter
{
    [FieldOffset(0x0)] private nint _pVTable;
    [FieldOffset(0x8)] public RnQueryShapeAttr_t QueryShapeAttributes;
    [FieldOffset(0x38)] public bool IterateEntities;

    public CTraceFilter( bool checkIgnoredEntities = true )
    {
        _pVTable = checkIgnoredEntities ? CTraceFilterVTable.pCTraceFilterShouldHitSimpleCall : CTraceFilterVTable.pCTraceFilterVTable;
    }

    public CTraceFilter( Func<CBaseEntity, bool> customFilter )
    {
        CTraceFilterVTable.CustomFilterFunc = customFilter;
        _pVTable = CTraceFilterVTable.pCTraceFilterShouldHitCustomCall;
        this.IterateEntities = true;
    }

    internal void EnsureValid()
    {
        if (this._pVTable == 0)
        {
            _pVTable = CTraceFilterVTable.pCTraceFilterShouldHitCustomCall;
        }
    }
}

internal static class CTraceFilterVTable
{
    public static nint pCTraceFilterVTable;
    public static nint pCTraceFilterShouldHitSimpleCall;
    public static nint pCTraceFilterShouldHitCustomCall;

    [UnmanagedCallersOnly]
    public unsafe static void Destructor( CTraceFilter* filter, byte unk01 )
    {
        // do nothing
    }

    [UnmanagedCallersOnly]
    public unsafe static byte ShouldHitDirect()
    {
        return 1;
    }

    public static Func<CBaseEntity, bool>? CustomFilterFunc { get; internal set; } = null;

    [UnmanagedCallersOnly]
    public unsafe static byte ShouldHitSimple( CTraceFilter* filter, nint entity )
    {
        var ent = Helper.AsSchema<CBaseEntity>(entity);
        if (ent == null || !ent.IsValid) return 0;

        return filter->QueryShapeAttributes.EntityIdsToIgnore[0] != ent.Index && filter->QueryShapeAttributes.EntityIdsToIgnore[1] != ent.Index ? (byte)1 : (byte)0;
    }

    [UnmanagedCallersOnly]
    public unsafe static byte ShouldHitCustom( CTraceFilter* filter, nint entity )
    {
        var ent = Helper.AsSchema<CBaseEntity>(entity);
        if (ent == null || !ent.IsValid) return 0;

        var hit = filter->QueryShapeAttributes.EntityIdsToIgnore[0] != ent.Index && filter->QueryShapeAttributes.EntityIdsToIgnore[1] != ent.Index;

        if (hit && CustomFilterFunc != null)
        {
            hit = CustomFilterFunc.Invoke(ent);
        }

        return hit ? (byte)1 : (byte)0;
    }

    static unsafe CTraceFilterVTable()
    {
        pCTraceFilterVTable = Marshal.AllocHGlobal(sizeof(nint) * 2);
        Span<nint> vtable = new((void*)pCTraceFilterVTable, 2);
        vtable[0] = (nint)(delegate* unmanaged< CTraceFilter*, byte, void >)(&Destructor);
        vtable[1] = (nint)(delegate* unmanaged< byte >)(&ShouldHitDirect);

        pCTraceFilterShouldHitSimpleCall = Marshal.AllocHGlobal(sizeof(nint) * 2);
        Span<nint> funcTableSimple = new((void*)pCTraceFilterShouldHitSimpleCall, 2);
        funcTableSimple[0] = (nint)(delegate* unmanaged< CTraceFilter*, byte, void >)(&Destructor);
        funcTableSimple[1] = (nint)(delegate* unmanaged< CTraceFilter*, nint, byte >)(&ShouldHitSimple);

        pCTraceFilterShouldHitCustomCall = Marshal.AllocHGlobal(sizeof(nint) * 2);
        Span<nint> funcTableCustom = new((void*)pCTraceFilterShouldHitCustomCall, 2);
        funcTableCustom[0] = (nint)(delegate* unmanaged< CTraceFilter*, byte, void >)(&Destructor);
        funcTableCustom[1] = (nint)(delegate* unmanaged< CTraceFilter*, nint, byte >)(&ShouldHitCustom);
    }
}