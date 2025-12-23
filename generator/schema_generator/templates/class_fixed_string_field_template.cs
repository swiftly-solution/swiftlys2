    private static nint? _$NAME$Offset;

    public string $NAME$ {
        get {
            _$NAME$Offset = _$NAME$Offset ?? Schema.GetOffset($HASH$);
            return Schema.GetString(_Handle + _$NAME$Offset!.Value);
        }
        set {
            _$NAME$Offset = _$NAME$Offset ?? Schema.GetOffset($HASH$);
            Schema.SetFixedString(_Handle, _$NAME$Offset!.Value, value, $ELEMENT_COUNT$);
        }
    } 