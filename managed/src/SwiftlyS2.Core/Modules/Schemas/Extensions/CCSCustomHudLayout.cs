using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CCSCustomHudLayout
{
    /// <summary>
    /// Set the value of a dialog variable string override for a player.
    /// </summary>
    /// <param name="playerId">Player index.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    [ThreadUnsafe]
    public void SetDialogVariableStringForPlayer( int playerId, string panelId, string variableName, string value );

    /// <summary>
    /// Remove the value of a dialog variable string override for a player. The dialog variable string will follow global settings.
    /// </summary>
    /// <param name="playerId">Player index.</param>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    [ThreadUnsafe]
    public void RemoveDialogVariableStringForPlayer( int playerId, string panelId, string variableName );

    /// <summary>
    /// Set the value of a dialog variable string globally.
    /// </summary>
    /// <param name="panelId">The id attribute of the target hud element.</param>
    /// <param name="variableName">The dialog variable string name. Example: the variable name of "{s:dynamic}" is "dynamic".</param>
    /// <param name="value">The value to set.</param>
    [ThreadUnsafe]
    public void SetDialogVariableString( string panelId, string variableName, string value );

    /// <summary>
    /// Set whether the hud element has a class or not for a player.
    /// 
    /// </summary>
    /// <param name="playerId">Player index.</param>
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
    /// Set whether the hud element has a class or not globally.
    /// 
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
}