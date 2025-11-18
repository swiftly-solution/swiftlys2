using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Shared.Commands;

public interface ICommandService
{

  /// <summary>
  /// The listener for the command.
  /// </summary>
  /// <param name="context">The command context.</param>
  delegate void CommandListener( ICommandContext context );

  /// <summary>
  /// The handler for the client command hook.
  /// </summary>
  /// <param name="playerId">The player id.</param>
  /// <param name="commandLine">The command line.</param>
  /// <returns>Whether the command should continue to be sent.</returns>
  delegate HookResult ClientCommandHandler( int playerId, string commandLine );


  /// <summary>
  /// The handler for the client chat hook.
  /// </summary>
  /// <param name="playerId">The player id.</param>
  /// <param name="text">The text.</param>
  /// <param name="teamonly">Whether the text is for team only.</param>
  /// <returns>Whether the text should continue to be sent.</returns>

  delegate HookResult ClientChatHandler( int playerId, string text, bool teamonly );

  /// <summary>
  /// Registers a command.
  /// </summary>
  /// <param name="commandName">The command name.</param>
  /// <param name="handler">The handler callback for the command.</param>
  /// <param name="registerRaw">If set to false, the command will not starts with a `sw_` prefix.</param>
  /// <param name="permission">The permission required to use the command.</param>
  /// <returns>The guid of the command.</returns>
  Guid RegisterCommand( string commandName, CommandListener handler, bool registerRaw = false, string permission = "" );

  /// <summary>
  /// Registers a command alias.
  /// </summary>
  /// <param name="commandName">The command name.</param>
  /// <param name="alias">The alias.</param>
  /// <param name="registerRaw">If set to false, the alias will not starts with a `sw_` prefix.</param>
  void RegisterCommandAlias( string commandName, string alias, bool registerRaw = false );

  /// <summary>
  /// Unregisters a command.
  /// </summary>
  /// <param name="guid">The guid of the command.</param>
  void UnregisterCommand( Guid guid );

  /// <summary>
  /// Unregisters all command listeners with the specified command name.
  /// </summary>
  /// <param name="commandName">The command name.</param>
  void UnregisterCommand( string commandName );

  /// <summary>
  /// Hooks client commands, will be fired when a player sends any command.
  /// </summary>
  /// <param name="handler">The handler callback for the client command.</param>
  Guid HookClientCommand( ClientCommandHandler handler );

  /// <summary>
  /// Unhooks a client command.
  /// </summary>
  /// <param name="guid">The guid of the client command.</param>
  void UnhookClientCommand( Guid guid );

  /// <summary>
  /// Hooks client chat, will be fired when a player sends any chat message.
  /// </summary>
  /// <param name="handler">The handler callback for the client chat.</param>
  Guid HookClientChat( ClientChatHandler handler );

  /// <summary>
  /// Unhooks a client chat.
  /// </summary>
  /// <param name="guid">The guid of the client chat.</param>
  void UnhookClientChat( Guid guid );

}