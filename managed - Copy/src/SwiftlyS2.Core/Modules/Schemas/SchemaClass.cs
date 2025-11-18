using System.Security.Cryptography.X509Certificates;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.Schemas;

internal abstract class SchemaClass : SchemaField {
  public SchemaClass(nint handle) : base(handle, 0) {
  }

  public SchemaClass(nint handle, ulong hash) : base(handle, hash) {
  }

}