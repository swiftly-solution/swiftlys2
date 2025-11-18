using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;

namespace PluginId;

/// <summary>
/// This is an example that shows how to hook and call native functions.
/// </summary>
public partial class PluginId
{

  // Your delegate type of the function.
  delegate int TestFunctionDelegate(int a, int b);
  public void InitializeUnmanagedFunctions()
  {
    // Get the address of the function.
    nint? address = Core.GameData.GetSignature("YourFunctionSignature");

    // Or you can use the signature to get the address.
    address = Core.Memory.GetAddressBySignature(Library.Server, "48 89 5C XXX");
    if (address is null)
    {
      Core.Logger.LogError("Failed to get the address of the function.");
      return;
    }

    // Get the function.
    var function = Core.Memory.GetUnmanagedFunctionByAddress<TestFunctionDelegate>(address!.Value);

    // Call the function.
    function.Call(1, 2);

    // Call the original function if hooked, this can bypass all the hooks that created by SwiftlyS2.
    function.CallOriginal(1, 2);

    var guid = function.AddHook(next => {
      return (a, b) =>
      {
        Console.WriteLine("Hooked!");

        Console.WriteLine("Pre hook");

        // here is the code for pre hook.

        // you can modify the param in this way.
        var modifiedA = a + 1;

        // Call the original function.
        // If you want to stop the call, you can return 0 and don't call next().
        var result = next()(modifiedA, b);

        // here is the code for post hook.
        Console.WriteLine("Post hook");

        // you can modify the result in this way.
        var modifiedResult = result + 1;

        return modifiedResult;
      };
    });

    // Remove the hook.
    function.RemoveHook(guid);


  }
}