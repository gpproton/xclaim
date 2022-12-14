using Microsoft.Maui.Controls.Shapes;
using XClaim.Mobile.Views.Profile;
using XClaim.Mobile.Views.Claim;

namespace XClaim.Mobile.Views.Home;

internal enum PageRow { First, Second, Third, Fourth }
internal enum HeaderRows { First, Second }
internal enum HeaderColumns { First, Second, Third }
internal enum ListTitleColumn { First, Second }

public class HomeView : BaseView<HomeViewModel> {
    public HomeView(HomeViewModel vm) : base(vm) => Build();

    protected override void Build() => Content = new Grid() {
        RowDefinitions = Rows.Define(
            (PageRow.First, 85),
            (PageRow.Second, 115),
            (PageRow.Third, Star),
            (PageRow.Fourth, Auto)
        ),
        Children = {
            new Grid() {
                ColumnDefinitions = Columns.Define(
                    (HeaderColumns.First, Auto),
                    (HeaderColumns.Second, Star),
                    (HeaderColumns.Third, Auto)
                ),
                Children = {
                    new AvatarView() {
                        Content = new Image().Source(new FontImageSource() {
                            FontFamily = "FASolid",
                            Glyph = FA.Solid.User,
                            Color = Colors.White
                        }).Size(28)
                    }.Size(54, 54)
                    .CenterVertical()
                    .DynamicResource(BackgroundColorProperty, "Gray300")
                    .TapGesture(async () => await Shell.Current.GoToAsync(nameof(ProfileView)))
                    .Column(HeaderColumns.First),
                    new StackLayout {
                        Children = {
                            new Label().Text("Hello")
                            .DynamicResource(Label.TextColorProperty, "Secondary"),
                            new Label().Text("Saurav")
                            .Font(size: 20, family: "RobotoBold")
                            .Margins(0, -5, 0, 0)
                            .DynamicResource(Label.TextColorProperty, "Gray500")
                        }
                    }.Margins(8, 16, 0, 0)
                    .Column(HeaderColumns.Second),
                    new Image().Source(new FontImageSource() {
                        FontFamily = "FASolid",
                        Glyph = FA.Solid.Bell
                    }.DynamicResource(FontImageSource.ColorProperty, "Primary"))
                    .Size(28)
                    .CenterVertical()
                    .TapGesture(async () => await Shell.Current.GoToAsync($"///{nameof(HomeView)}/{nameof(NotificationView)}"))
                    .Column(HeaderColumns.Third)
                }
            }
            .Paddings(24, 8, 24, 8)
            .Row(PageRow.First),
            new Border() {
                Padding = 16,
                StrokeThickness = 0F,
                StrokeShape = new RoundRectangle {
                    CornerRadius = new CornerRadius(10)
                },
                Background = Gradients.AppGradient,
                Shadow = new Shadow {
                    Offset = new Point(4, 8),
                    Opacity = 0.5F,
                    Radius = 5,
                    Brush = new LinearGradientBrush {
                        GradientStops = {
                            new GradientStop {
                                Offset = 0.1F,
                                Color = Colors.Grey
                            },
                            new GradientStop {
                                Offset = 1.0F,
                                Color = Colors.LightGray
                            }
                        }
                    }
                },
                Content = new StackLayout() {
                    Children = {
                        new Label()
                        .Text("Pending Claims")
                        .Font(size: 14)
                        .TextColor(Colors.White)
                        .Margins(0, 0, 0, 4),
                        new Label()
                        .Text("\u20A6" + "10,000")
                        .TextColor(Colors.White)
                        .Font(size: 32, family: "RobotoBold")
                    }
                }
            }
            .Margins(24, 6, 24, 6)
            .Row(PageRow.Second),
            new VerticalStackLayout() {
                new Grid() {
                    ColumnDefinitions = Columns.Define(
                        (ListTitleColumn.First, Star),
                        (ListTitleColumn.Second, Auto)
                    ),
                    Padding = 4,
                    Children = {
                        new Label()
                        .Text("Recents")
                        .Font(size: 16)
                        .Column(ListTitleColumn.First)
                        .DynamicResource(Label.TextColorProperty, "Gray300"),
                        new Label()
                        .Text("See all")
                        .Font(size: 16)
                        .TapGesture(async () => await Shell.Current.GoToAsync($"//{nameof(ClaimView)}"))
                        .Column(ListTitleColumn.Second)
                        .DynamicResource(Label.TextColorProperty, "Primary")
                    }
                }
            }
            .Margins(16, 16, 16, 8)
            .Row(PageRow.Third),
            new Button().Text("Create Request")
            .DynamicResource(StyleProperty, "ButtonLargePrimary")
            .CenterVertical()
            .Margins(24, 16, 24, 24)
            .TapGesture(async () => await Shell.Current.GoToAsync($"///{nameof(HomeView)}/{nameof(ClaimFormView)}"))
            .Row(PageRow.Fourth)
        }
    };
    protected override void OnAppearing() {
        base.OnAppearing();
    }
}

public partial class HomeViewModel : BaseViewModel { }
