using SwiftlyS2.Shared.Menus;

namespace SwiftlyS2.Core.Menus;

internal sealed class MenuDesignAPI : IMenuDesignAPI
{
    private readonly MenuConfiguration configuration;
    private readonly IMenuBuilderAPI builder;
    private readonly Action<MenuOptionScrollStyle> setScrollStyle;
    // private readonly Action<MenuOptionTextStyle> setTextStyle;

    public MenuDesignAPI( MenuConfiguration configuration, IMenuBuilderAPI builder, Action<MenuOptionScrollStyle> setScrollStyle/*, Action<MenuOptionTextStyle> setTextStyle*/ )
    {
        this.configuration = configuration;
        this.builder = builder;
        this.setScrollStyle = setScrollStyle;
        // this.setTextStyle = setTextStyle;
    }

    public IMenuBuilderAPI SetMenuTitle( string? title = null )
    {
        configuration.Title = title ?? "Menu";
        return builder;
    }

    public IMenuBuilderAPI SetMenuTitleVisible( bool visible = true )
    {
        configuration.HideTitle = !visible;
        return builder;
    }

    public IMenuBuilderAPI SetMenuTitleItemCountVisible( bool visible = true )
    {
        configuration.HideTitleItemCount = !visible;
        return builder;
    }

    public IMenuBuilderAPI SetMenuFooterVisible( bool visible = true )
    {
        configuration.HideFooter = !visible;
        return builder;
    }

    public IMenuBuilderAPI SetMaxVisibleItems( int count = 5 )
    {
        configuration.MaxVisibleItems = count;
        return builder;
    }

    public IMenuBuilderAPI EnableAutoAdjustVisibleItems()
    {
        configuration.AutoIncreaseVisibleItems = true;
        return builder;
    }

    public IMenuBuilderAPI DisableAutoAdjustVisibleItems()
    {
        configuration.AutoIncreaseVisibleItems = false;
        return builder;
    }

    public IMenuBuilderAPI SetGlobalScrollStyle( MenuOptionScrollStyle style )
    {
        setScrollStyle(style);
        return builder;
    }

    public IMenuBuilderAPI SetNavigationMarkerColor( string? hexColor = null )
    {
        configuration.NavigationMarkerColor = hexColor;
        return builder;
    }

    public IMenuBuilderAPI SetNavigationMarkerColor( Shared.Natives.Color color )
    {
        configuration.NavigationMarkerColor = color.ToHex();
        return builder;
    }

    public IMenuBuilderAPI SetNavigationMarkerColor( System.Drawing.Color color )
    {
        configuration.NavigationMarkerColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        return builder;
    }

    public IMenuBuilderAPI SetMenuFooterColor( string? hexColor = null )
    {
        configuration.FooterColor = hexColor;
        return builder;
    }

    public IMenuBuilderAPI SetMenuFooterColor( Shared.Natives.Color color )
    {
        configuration.FooterColor = color.ToHex();
        return builder;
    }

    public IMenuBuilderAPI SetMenuFooterColor( System.Drawing.Color color )
    {
        configuration.FooterColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        return builder;
    }

    public IMenuBuilderAPI SetVisualGuideLineColor( string? hexColor = null )
    {
        configuration.VisualGuideLineColor = hexColor;
        return builder;
    }

    public IMenuBuilderAPI SetVisualGuideLineColor( Shared.Natives.Color color )
    {
        configuration.VisualGuideLineColor = color.ToHex();
        return builder;
    }

    public IMenuBuilderAPI SetVisualGuideLineColor( System.Drawing.Color color )
    {
        configuration.VisualGuideLineColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        return builder;
    }

    public IMenuBuilderAPI SetDisabledColor( string? hexColor = null )
    {
        configuration.DisabledColor = hexColor;
        return builder;
    }

    public IMenuBuilderAPI SetDisabledColor( Shared.Natives.Color color )
    {
        configuration.DisabledColor = color.ToHex();
        return builder;
    }

    public IMenuBuilderAPI SetDisabledColor( System.Drawing.Color color )
    {
        configuration.DisabledColor = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        return builder;
    }

    // public IMenuBuilderAPI SetGlobalOptionTextStyle( MenuOptionTextStyle style )
    // {
    //     setTextStyle(style);
    //     return builder;
    // }
}