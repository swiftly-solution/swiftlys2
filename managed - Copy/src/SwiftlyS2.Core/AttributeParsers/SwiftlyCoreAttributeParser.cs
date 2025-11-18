using System.Reflection;
using SwiftlyS2.Core.Services;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace SwiftlyS2.Core.AttributeParsers;

internal static class SwiftlyCoreAttributeParser {
  public static void Parse(this ISwiftlyCore self, Type t) {

    var assembly = t.Assembly;
    var types = assembly.GetTypes();
    foreach (var type in types) {
      var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
      foreach (var field in fields) {
        var attribute = field.GetCustomAttribute<SwiftlyInject>();

        if (attribute != null) {
          field.SetValue(null, self);
        }
      }
      var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
      foreach (var property in properties) {
        var attribute = property.GetCustomAttribute<SwiftlyInject>();

        if (attribute != null) {
          property.SetValue(null, self);
        }
      }
    }
  }
}
