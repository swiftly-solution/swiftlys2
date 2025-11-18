
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessage_NotifyResponseFound : ITypedProtobuf<CUserMessage_NotifyResponseFound>, INetMessage<CUserMessage_NotifyResponseFound>, IDisposable
{
  static int INetMessage<CUserMessage_NotifyResponseFound>.MessageId => 165;
  
  static string INetMessage<CUserMessage_NotifyResponseFound>.MessageName => "CUserMessage_NotifyResponseFound";

  static CUserMessage_NotifyResponseFound ITypedProtobuf<CUserMessage_NotifyResponseFound>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_NotifyResponseFoundImpl(handle, isManuallyAllocated);


  public int EntIndex { get; set; }


  public string RuleName { get; set; }


  public string ResponseValue { get; set; }


  public string ResponseConcept { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_NotifyResponseFound_Criteria> Criteria { get; }


  public IProtobufRepeatedFieldValueType<uint> IntCriteriaNames { get; }


  public IProtobufRepeatedFieldValueType<int> IntCriteriaValues { get; }


  public IProtobufRepeatedFieldValueType<uint> FloatCriteriaNames { get; }


  public IProtobufRepeatedFieldValueType<float> FloatCriteriaValues { get; }


  public IProtobufRepeatedFieldValueType<uint> SymbolCriteriaNames { get; }


  public IProtobufRepeatedFieldValueType<uint> SymbolCriteriaValues { get; }


  public int SpeakResult { get; set; }

}
