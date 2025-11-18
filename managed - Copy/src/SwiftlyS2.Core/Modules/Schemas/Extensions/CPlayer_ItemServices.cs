using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CPlayer_ItemServices {

  /// <summary>
  /// Give an item to the player.
  /// </summary>
  /// <typeparam name="T">The type of the item to give.</typeparam>
  /// <returns>The item that was given.</returns>
  public T GiveItem<T>() where T : ISchemaClass<T>;

  /// <summary>
  /// Give an item to the player.
  /// </summary>
  /// <param name="itemDesignerName">The designer name of the item to give.</param>
  /// <returns>The item that was given.</returns>
  public T GiveItem<T>(string itemDesignerName) where T : ISchemaClass<T>;

  /// <summary>
  /// Give an item to the player.
  /// </summary>
  /// <param name="itemDesignerName">The designer name of the item to give.</param>
  public void GiveItem(string itemDesignerName);
  

  /// <summary>
  /// Drop the item that player is holding.
  /// </summary>
  public void DropActiveItem();

  /// <summary>
  /// Remove all items from the player.
  /// </summary>
  public void RemoveItems();
}