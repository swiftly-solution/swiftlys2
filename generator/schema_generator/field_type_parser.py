from class_name_convertor import get_interface_name, get_impl_name

unmanaged_type_maps = {
  "int8": "byte",
  "int16": "short",
  "int32": "int",
  "int64": "long",
  "uint8": "byte",
  "uint16": "ushort",
  "uint32": "uint",
  "uint64": "ulong",
  "float32": "float",
  "float64": "double", # fuck you valve
  "double": "double",
  "bool": "bool",
  "char": "char",
  "VectorWS": "Vector",
  "VectorAligned": "Vector",
  "Vector": "Vector",
  "QAngle": "QAngle",
  "CUtlVector": "CUtlVector",
  "CUtlLeanVector": "CUtlLeanVector",
  "Quaternion": "Quaternion",
  "Vector2D": "Vector2D",
  "Vector4D": "Vector4D",
  "CStrongHandle": "CStrongHandle",
  "CUtlSymbolLarge": "CUtlSymbolLarge",
  "CUtlString": "CUtlString",
  "Color": "Color",
  "CHandle": "CHandle",
  "CBufferString": "CBufferString",
  "CGlobalSymbolCaseSensitive": "CGlobalSymbol",
  "CGlobalSymbol": "CGlobalSymbol",
  "CTransformWS": "CTransform",
  "CTransform": "CTransform",
  "CNetworkedQuantizedFloat": "CNetworkedQuantizedFloat",
  "CUtlBinaryBlock": "CUtlBinaryBlock",
  "fltx4": "fltx4",
  "FourVectors": "FourVectors",
  "CEntityIndex": "uint",
  "CSplitScreenSlot": "uint",
  "CPlayerSlot": "uint",
  "WorldGroupId_t": "uint",
  "matrix3x4_t": "matrix3x4_t",
  "matrix3x4a_t": "matrix3x4_t", # should works?
  "RadianEuler": "RadianEuler",
  "CNetworkUtlVectorBase": "CUtlVector",
  "CEntityHandle": "CHandle<CEntityInstance>",
  "CTakeDamageInfo": "CTakeDamageInfo",
  "CTakeDamageResult": "CTakeDamageResult",
  "ChangeAccessorFieldPathIndex_t": "ChangeAccessorFieldPathIndex_t",
  "CNetworkVarChainer": "CNetworkVarChainer",
  "HSCRIPT": "HSCRIPTHandler",
  "QuaternionStorage": "QuaternionStorage",
  "CEntityIOOutput": "CEntityIOOutput",
  "CVariantBase<CVariantDefaultAllocator>": "CVariant<CVariantDefaultAllocator>",
}

blacklisted_types = [
  "CUtlStringTokenWithStorage",
  "FourVectors2D",
  "FeSimdTri_t",
  "CStrongHandleVoid",
  "CUtlVectorFixedGrowable",
  "CUtlLeanVectorFixedGrowable",
  "CWeakHandle",
  "DegreeEuler",
  "CTypedBitVec",
  "CUtlSymbol",
  "CUtlOrderedMap",
  "CUtlMap",
  "CSmartPtr",
  "CUtlHashtable",
  "CPulseValueFullType",
  "PulseSymbol_t",
  "CColorGradient",
  "CPiecewiseCurve",
  "CAnimGraph2ParamOptionalRef",
  "Range_t",
  "CAnimGraphParamRef",
  "bitfield",
  "KeyValues3",
  "KeyValues",
  "CResourceName",
  "CParticleNamedValueRef",
  "CKV3MemberNameSet",
  "CAnimGraphTagRef",
  "CResourceNameTyped",
  "CResourceArray",
  "CAnimGraphParamOptionalRef",
  "CAnimVariant",
  "RotationVector",
  "CAnimScriptParam",
  "CKV3MemberNameWithStorage",
  "CModelAnimNameWithDeltas",
  "CAnimValue",
  "CEntityOutputTemplate",
  "SphereBase_t",
  "CAttachmentNameSymbolWithStorage",
  "std::pair",
  "CCompressor",
  "CUtlVectorSIMDPaddedVector",
]

def convert_handle_type(type, interface=False):
  if type.startswith("CWeakHandle"):
    name = "CWeakHandle"
  elif type.startswith("CStrongHandleCopyable"):
    length = len("CStrongHandleCopyable")
    name = "CStrongHandle"
  elif type.startswith("CStrongHandle"):
    length = len("CStrongHandle")
    name = "CStrongHandle"
  else:
    length = len("CHandle")
    name = "CHandle"


  generic_t1 = type[length+1:]
  generic_t1 = generic_t1[:-1]

  generic_t1 = get_impl_name(generic_t1) if not interface else get_interface_name(generic_t1)

  # print(f"{name}<{generic_t1}>")
  return (f"{name}<{generic_t1}>", True)

def convert_utlvector_type(type, all_class_names, all_enum_names, interface = False):
  if type.startswith("CUtlVectorFixedGrowable"):
    name = "CUtlVectorFixedGrowable"
    length = len("CUtlVectorFixedGrowable")
  elif type.startswith("CUtlLeanVector"):
    name = "CUtlLeanVector"
    length = len("CUtlLeanVector")
  elif type.startswith("CUtlVectorEmbeddedNetworkVar"):
    name = "CUtlVector"
    length = len("CUtlVectorEmbeddedNetworkVar")
  elif type.startswith("CNetworkUtlVectorBase"):
    name = "CUtlVector"
    length = len("CNetworkUtlVectorBase")
  else:
    name = "CUtlVector"
    length = len("CUtlVector")

  generic_t1 = type[length+1:]
  generic_t1 = generic_t1[:-1]

  if "," in generic_t1:
    generic_t1 = generic_t1.split(",")[0]

  is_ptr = generic_t1.endswith("*")

  if is_ptr:
    generic_t1 = generic_t1[:-1]
  
  generic_t1_type, is_value_type = convert_field_type(generic_t1, "ref", all_class_names, all_enum_names, interface)

  if name == "CUtlLeanVector":
    if is_ptr and generic_t1_type == "char":
      return (f"{name}<CString, int>", True)
    if is_ptr:
      # print(f"{name}<PointerTo<{generic_t1_type}>>")
      return (f"{name}<PointerTo<{generic_t1_type}>, int>", True)
    
    return (f"{name}<{generic_t1_type}, int>", True)
  else:
    if is_ptr and generic_t1_type == "char":
      return (f"{name}<CString>", True)
    if is_ptr:
      # print(f"{name}<PointerTo<{generic_t1_type}>>")
      return (f"{name}<PointerTo<{generic_t1_type}>>", True)
    
    for blacklisted_type in blacklisted_types:
      if blacklisted_type in generic_t1_type:
        return (f"{name}<SchemaUntypedField>", True)

    return (f"{name}<{generic_t1_type}>", True)

def convert_field_type(type, kind, all_class_names, all_enum_names, interface = False):

  type = type.replace(' ', '');
  prefix = "I" if interface else ""

  for blacklisted_type in blacklisted_types:
    if type.startswith(blacklisted_type) and type != "CUtlSymbolLarge": # bypass CUtlSymbol
      return (f"SchemaUntypedField", False)

  if kind == "ptr" and type == "char": # char*
    return (f"CString", True)
  
  for key, value in unmanaged_type_maps.items():
    if type.startswith(key):

      if type.startswith("CWeakHandle") or type.startswith("CStrongHandle") or type.startswith("CHandle"):
        name, is_value_type = convert_handle_type(type, True)
        if kind == "fixed_array":
          return (f"{prefix}SchemaFixedArray<{name}>", False)
        return (name, is_value_type)
      
      if type.startswith("CUtlVector") or type.startswith("CNetworkUtlVector") or type.startswith("CUtlLeanVector"):
        name, is_value_type = convert_utlvector_type(type, all_class_names, all_enum_names, True)
        if kind == "fixed_array":
          return (f"{prefix}SchemaFixedArray<{name}>", False)
        return (name, is_value_type)
      
      if kind == "fixed_array":
        if type == "char":
          return (f"{prefix}SchemaFixedString", False)
        if "[" not in type:
          return (f"{prefix}SchemaFixedArray<{type.replace(key, value)}>", False)
        else:
          return (f"SchemaUntypedField", False)
      return (f"{type.replace(key, value)}", True)
  

      # print(generic_t1_type)

  if type in all_enum_names:
    if kind == "fixed_array":
      return (f"{prefix}SchemaFixedArray<{type}>", False)
    return (type, True)

  if type in all_class_names:
    if kind == "fixed_array":
      return (f"{prefix}SchemaClassFixedArray<{type}>", False)
    complex_type = get_impl_name(type) if not interface else get_interface_name(type)
    return (complex_type, False)
  
  print(f"Unknown type: {type}")
  return ("UNKNOWN", False)