using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CCSCustomHudLayout
{
    /// <summary>
    /// Set the value of a dialog variable string override for a player.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    [ThreadUnsafe]
    public void SetDialogVariableStringForPlayer( int playerId, string panelId, string variableName, string value );

    /// <summary>
    /// Set the value of a dialog variable string override for a player asynchronously.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    public Task SetDialogVariableStringForPlayerAsync( int playerId, string panelId, string variableName, string value );

    /// <summary>
    /// Remove the value of a dialog variable string override for a player. The dialog variable string will follow global settings.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    [ThreadUnsafe]
    public void RemoveDialogVariableStringForPlayer( int playerId, string panelId, string variableName );

    /// <summary>
    /// Remove the value of a dialog variable string override for a player asynchronously. The dialog variable string will follow global settings.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    public Task RemoveDialogVariableStringForPlayerAsync( int playerId, string panelId, string variableName );

    /// <summary>
    /// Get the value of a dialog variable string override for a player.
    /// Doesn't fallback to global settings if the value is not set.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <returns>The value. Return null if the value is not set.</returns>
    public string? GetDialogVariableStringForPlayer( int playerId, string panelId, string variableName );

    /// <summary>
    /// Set the value of a dialog variable string globally.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    [ThreadUnsafe]
    public void SetDialogVariableString( string panelId, string variableName, string value );

    /// <summary>
    /// Set the value of a dialog variable string globally asynchronously.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    public Task SetDialogVariableStringAsync( string panelId, string variableName, string value );

    /// <summary>
    /// Get the value of a dialog variable string globally.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <returns>The value. Return null if the value is not set.</returns>
    public string? GetDialogVariableString( string panelId, string variableName );

    /// <summary>
    /// Set whether the hud element has a class or not for a player.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <param name="classStatus">
    /// <list type="table">
    ///     <listheader>
    ///         <term>Value</term>
    ///         <description>Behavior</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_DoesNotHaveClass"/></term>
    ///         <description>Does not override the existing class status.</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined"/></term>
    ///         <description>Explicitly specifies that the panel does not have the class.</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_HasClass"/></term>
    ///         <description>Explicitly specifies that the panel has the class.</description>
    ///     </item>
    /// </list>
    /// </param>
    [ThreadUnsafe]
    public void SetHasClassForPlayer( int playerId, string panelId, string className, EHudPanelClassStatus_t classStatus );

    /// <summary>
    /// Set whether the hud element has a class or not for a player asynchronously.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <param name="classStatus">The class status.</param>
    public Task SetHasClassForPlayerAsync( int playerId, string panelId, string className, EHudPanelClassStatus_t classStatus );

    /// <summary>
    /// Get the class status of a hud element for a player.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <returns>The class status. Return <see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined"/> if the class status is not set.</returns>
    public EHudPanelClassStatus_t GetHasClassForPlayer( int playerId, string panelId, string className );

    /// <summary>
    /// Set whether the hud element has a class or not globally.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <param name="classStatus">
    /// <list type="table">
    ///     <listheader>
    ///         <term>Value</term>
    ///         <description>Behavior</description>
    ///     </listheader>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_DoesNotHaveClass"/></term>
    ///         <description>Does not override the existing class status.</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined"/></term>
    ///         <description>Explicitly specifies that the panel does not have the class.</description>
    ///     </item>
    ///     <item>
    ///         <term><see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_HasClass"/></term>
    ///         <description>Explicitly specifies that the panel has the class.</description>
    ///     </item>
    /// </list>
    /// </param>    
    [ThreadUnsafe]
    public void SetHasClass( string panelId, string className, EHudPanelClassStatus_t classStatus );

    /// <summary>
    /// Set whether the hud element has a class or not globally asynchronously.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <param name="classStatus">The class status.</param>
    public Task SetHasClassAsync( string panelId, string className, EHudPanelClassStatus_t classStatus );

    /// <summary>
    /// Get the class status of a hud element globally.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="className">The class name.</param>
    /// <returns>The class status. Return <see cref="EHudPanelClassStatus_t.k_eHudPanelClassStatus_Undefined"/> if the class status is not set.</returns>
    public EHudPanelClassStatus_t GetHasClass( string panelId, string className );

    /// <summary>
    /// Set the input capture state of a hud element for a player.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="enabled">Whether the input capture is enabled or not.</param>
    [ThreadUnsafe]
    public void SetInputCaptureEnabledForPlayer( int playerId, bool enabled );

    /// <summary>
    /// Set the input capture state of a hud element for a player asynchronously.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <param name="enabled">Whether the input capture is enabled or not.</param>
    public Task SetInputCaptureEnabledForPlayerAsync( int playerId, bool enabled );

    /// <summary>
    /// Set the input capture state of a hud element globally.
    ///
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="enabled">Whether the input capture is enabled or not.</param>
    [ThreadUnsafe]
    public void SetInputCaptureEnabled( bool enabled );

    /// <summary>
    /// Set the input capture state of a hud element globally asynchronously.
    /// </summary>
    /// <param name="enabled">Whether the input capture is enabled or not.</param>
    public Task SetInputCaptureEnabledAsync( bool enabled );

    /// <summary>
    /// Get the input capture state of a hud element for a player.
    /// </summary>
    /// <param name="playerId">Player id.</param>
    /// <returns>Whether the input capture is enabled or not.</returns>
    public bool IsInputCaptureEnabledForPlayer( int playerId );

    /// <summary>
    /// Get the input capture state of a hud element globally.
    /// </summary>
    /// <returns>Whether the input capture is enabled or not.</returns>
    public bool IsInputCaptureEnabled();
}
