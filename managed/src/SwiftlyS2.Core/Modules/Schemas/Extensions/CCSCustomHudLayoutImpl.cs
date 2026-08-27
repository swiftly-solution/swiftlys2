using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CCSCustomHudLayoutImpl : CCSCustomHudLayout
{

    private bool TryFindPanelId( string panelId, out int index )
    {
        for (var i = 0; i < PanelIds.Count; i++)
        {
            if (PanelIds[i] == panelId)
            {
                index = i;
                return true;
            }
        }

        index = -1;
        return false;
    }

    private bool TryFindDialogVariableName( string variableName, out int index )
    {
        for (var i = 0; i < DialogVariableNames.Count; i++)
        {
            if (DialogVariableNames[i] == variableName)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    private bool TryFindClassName( string className, out int index )
    {
        for (var i = 0; i < ClassNames.Count; i++)
        {
            if (ClassNames[i] == className)
            {
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }

    public void SetDialogVariableStringForPlayer( int playerId, string panelId, string variableName, string value )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetDialogVariableStringForPlayer(
            Address,
            playerId,
            panelId,
            variableName,
            value
        );
    }

    public Task SetDialogVariableStringForPlayerAsync( int playerId, string panelId, string variableName, string value )
    {
        return SchedulerManager.QueueOrNow(() => SetDialogVariableStringForPlayer(playerId, panelId, variableName, value));
    }

    public void RemoveDialogVariableStringForPlayer( int playerId, string panelId, string variableName )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_RemoveDialogVariableStringForPlayer(
            Address,
            playerId,
            panelId,
            variableName
        );
    }

    public Task RemoveDialogVariableStringForPlayerAsync( int playerId, string panelId, string variableName )
    {
        return SchedulerManager.QueueOrNow(() => RemoveDialogVariableStringForPlayer(playerId, panelId, variableName));
    }

    public string? GetDialogVariableStringForPlayer( int playerId, string panelId, string variableName )
    {
        if (!TryFindPanelId(panelId, out var panelIndex))
            return null;

        if (!TryFindDialogVariableName(variableName, out var variableIndex))
            return null;

        foreach (var str in PlayerLayoutStates[playerId].DialogVariableStrings)
        {
            if (str.PanelIdIndex == panelIndex && str.DialogVariableIndex == variableIndex && str.IsSet)
                return str.Value;
        }
        return null;
    }

    public void SetDialogVariableString( string panelId, string variableName, string value )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetDialogVariableString(
            Address,
            panelId,
            variableName,
            value
        );
    }

    public Task SetDialogVariableStringAsync( string panelId, string variableName, string value )
    {
        return SchedulerManager.QueueOrNow(() => SetDialogVariableString(panelId, variableName, value));
    }

    public string? GetDialogVariableString( string panelId, string variableName )
    {
        if (!TryFindPanelId(panelId, out var panelIndex))
            return null;

        if (!TryFindDialogVariableName(variableName, out var variableIndex))
            return null;

        foreach (var str in GlobalLayoutState.DialogVariableStrings)
        {
            if (str.PanelIdIndex == panelIndex && str.DialogVariableIndex == variableIndex && str.IsSet)
                return str.Value;
        }
        return null;
    }

    public void SetHasClassForPlayer(
        int playerId,
        string panelId,
        string className,
        EHudPanelClassStatus_t classStatus
    )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetHasClassForPlayer(
            Address,
            playerId,
            panelId,
            className,
            classStatus
        );
    }

    public Task SetHasClassForPlayerAsync(
        int playerId,
        string panelId,
        string className,
        EHudPanelClassStatus_t classStatus
    )
    {
        return SchedulerManager.QueueOrNow(() => SetHasClassForPlayer(playerId, panelId, className, classStatus));
    }

    public EHudPanelClassStatus_t GetHasClassForPlayer( int playerId, string panelId, string className )
    {
        if (!TryFindPanelId(panelId, out var panelIndex))
            return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;

        if (!TryFindClassName(className, out var classIndex))
            return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;

        foreach (var cls in PlayerLayoutStates[playerId].HasClasses)
        {
            if (cls.PanelIdIndex == panelIndex && cls.ClassNameIndex == classIndex)
                return cls.ClassStatus;
        }
        return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;
    }

    public void SetHasClass( string panelId, string className, EHudPanelClassStatus_t classStatus )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetHasClass(
            Address,
            panelId,
            className,
            classStatus
        );
    }

    public Task SetHasClassAsync( string panelId, string className, EHudPanelClassStatus_t classStatus )
    {
        return SchedulerManager.QueueOrNow(() => SetHasClass(panelId, className, classStatus));
    }

    public EHudPanelClassStatus_t GetHasClass( string panelId, string className )
    {
        if (!TryFindPanelId(panelId, out var panelIndex))
            return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;

        if (!TryFindClassName(className, out var classIndex))
            return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;

        foreach (var cls in GlobalLayoutState.HasClasses)
        {
            if (cls.PanelIdIndex == panelIndex && cls.ClassNameIndex == classIndex)
                return cls.ClassStatus;
        }
        return EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined;
    }

    public void SetInputCaptureEnabledForPlayer( int playerId, bool enabled )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSCustomHudLayout_SetInputCaptureEnabled(
            Address,
            playerId,
            enabled
        );
    }

    public Task SetInputCaptureEnabledForPlayerAsync( int playerId, bool enabled )
    {
        return SchedulerManager.QueueOrNow(() => SetInputCaptureEnabledForPlayer(playerId, enabled));
    }

    public void SetInputCaptureEnabled( bool enabled )
    {
        NativeBinding.ThrowIfNonMainThread();
        GlobalLayoutState.InputCaptureEnabled = enabled;
        GlobalLayoutState.InputCaptureEnabledUpdated();
    }

    public Task SetInputCaptureEnabledAsync( bool enabled )
    {
        return SchedulerManager.QueueOrNow(() => SetInputCaptureEnabled(enabled));
    }

    public bool IsInputCaptureEnabledForPlayer( int playerId )
    {
        return PlayerLayoutStates[playerId].InputCaptureEnabled;
    }

    public bool IsInputCaptureEnabled()
    {
        return GlobalLayoutState.InputCaptureEnabled;
    }
}
