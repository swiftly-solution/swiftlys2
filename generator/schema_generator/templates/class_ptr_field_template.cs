    private static nint? _$NAME$Offset;

    public $INTERFACE_TYPE$? $NAME$ {
        get {
            _$NAME$Offset = _$NAME$Offset ?? Schema.GetOffset($HASH$);
            var ptr = _Handle.Read<nint>(_$NAME$Offset!.Value);
            return ptr.IsValidPtr() ? new $IMPL_TYPE$(ptr) : null;
        }
    }