using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Sounds;

public class SoundEvent : IDisposable
{

  private SoundEventSafeHandle _handle;


  /// <summary>
  /// The sound event name.
  /// </summary>
  public string Name
  {
    get => NativeSounds.GetName(Address);
    set => NativeSounds.SetName(Address, value);
  }

  /// <summary>
  /// The index of the entity that this sound event is emitted from.
  /// Setting to -1 (default) will emit the sound from the recipient location.
  /// </summary>
  public int SourceEntityIndex
  {
    get => NativeSounds.GetSourceEntityIndex(Address);
    set => NativeSounds.SetSourceEntityIndex(Address, value);
  }

  /// <summary>
  /// The volume of the sound event.
  /// </summary>
  public float Volume
  {
    get => NativeSounds.GetFloat(Address, "public.volume");
    set => NativeSounds.SetFloat(Address, "public.volume", value);
  }

  /// <summary>
  /// The pitch of the sound event.
  /// </summary>
  public float Pitch
  {
    get => NativeSounds.GetFloat(Address, "public.pitch");
    set => NativeSounds.SetFloat(Address, "public.pitch", value);
  }

  private CRecipientFilter _recipients = new();

  /// <summary>
  /// The recipients of the sound event.
  /// </summary>
  public ref CRecipientFilter Recipients { get => ref _recipients; }

  public SoundEvent()
  {
    _handle = new SoundEventSafeHandle(NativeSounds.CreateSoundEvent());
    Volume = 1.0f;
    Pitch = 1.0f;
    SourceEntityIndex = -1;
  }


  public SoundEvent(string name, float volume = 1.0f, float pitch = 1.0f)
  {
    _handle = new SoundEventSafeHandle(NativeSounds.CreateSoundEvent());
    Name = name;
    Volume = volume;
    Pitch = pitch;
    SourceEntityIndex = -1;
  }

  private nint Address => _handle.Address;

  public void SetSourceEntity(CEntityInstance entity)
  {
    SourceEntityIndex = (int)entity.Index;
  }

  public void SetBool(string fieldName, bool value)
  {
    NativeSounds.SetBool(Address, fieldName, value);
  }

  public bool GetBool(string fieldName)
  {
    return NativeSounds.GetBool(Address, fieldName);
  }

  public void SetInt32(string fieldName, int value)
  {
    NativeSounds.SetInt32(Address, fieldName, value);
  }

  public int GetInt32(string fieldName)
  {
    return NativeSounds.GetInt32(Address, fieldName);
  }

  public void SetUInt32(string fieldName, uint value)
  {
    NativeSounds.SetUInt32(Address, fieldName, value);
  }

  public uint GetUInt32(string fieldName)
  {
    return NativeSounds.GetUInt32(Address, fieldName);
  }

  public void SetFloat(string fieldName, float value)
  {
    NativeSounds.SetFloat(Address, fieldName, value);
  }

  public float GetFloat(string fieldName)
  {
    return NativeSounds.GetFloat(Address, fieldName);
  }

  public void SetFloat3(string fieldName, float x, float y, float z)
  {
    Vector vec = new(x, y, z);
    NativeSounds.SetFloat3(Address, fieldName, vec);
  }

  public void SetFloat3(string fieldName, Vector vec)
  {
    NativeSounds.SetFloat3(Address, fieldName, vec);
  }

  public Vector GetFloat3(string fieldName)
  {
    return NativeSounds.GetFloat3(Address, fieldName);
  }

  /// <summary>
  /// Emit the sound event.
  /// 
  /// <returns>The emitted sound event guid.</returns>
  /// </summary>
  public uint Emit()
  {
    NativeSounds.SetClients(Address, Recipients.ToMask());
    return NativeSounds.Emit(Address);
  }

  public void Dispose()
  {
    _handle.Dispose();
  }
}