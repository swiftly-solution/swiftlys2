    private static nint? _$NAME$Offset;

    public $INTERFACE_TYPE$ $NAME$ {
        get {
            _$NAME$Offset = _$NAME$Offset ?? Schema.GetOffset($HASH$);
            return new $IMPL_TYPE$(_Handle + _$NAME$Offset!.Value);
        }
    }