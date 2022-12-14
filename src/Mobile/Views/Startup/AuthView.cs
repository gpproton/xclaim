using XClaim.Mobile.Views.Home;
using static Microsoft.Maui.Controls.Button;

namespace XClaim.Mobile.Views.Startup;

public class AuthView : BaseView<AuthViewModel> {
    enum PageRow { First, Second }
    public AuthView(AuthViewModel vm) : base(vm) => Build();

    protected override void Build() {
        Background = Gradients.AppGradient;
        Content = new Grid {
            RowDefinitions = Rows.Define(
                (PageRow.First, Stars(2)),
                (PageRow.Second, Star)
            ),
            Children = {
                new Image() {
                    HeightRequest = 320,
                    MaximumHeightRequest = 768,
                    Aspect = Aspect.AspectFill
                }
                .Source(Icons.AuthBanner)
                .CenterHorizontal()
                .Row(PageRow.First),
                new VerticalStackLayout() {
                    Children = {
                        new Button() {
                            ImageSource = Icons.Google,
                            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Left, 32)
                        }
                        .Text("Sign in with Google")
                        .DynamicResource(StyleProperty, "ButtonAuth")
                        .CenterVertical()
                        .BindCommand(nameof(AuthViewModel.NavigateToHomeCommand))
                    }
                }
                .Paddings(16, 16, 16, 16)
                .Row(PageRow.Second)
            }
        };
    }

    protected override void OnAppearing() {
        base.OnAppearing();
    }
}

public partial class AuthViewModel : BaseViewModel {
    [RelayCommand]
    async void NavigateToHome() => await Shell.Current.GoToAsync($"//{nameof(HomeView)}");
}
