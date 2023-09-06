using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnterPage : ContentPage
    {
        public EnterPage()
        {
            InitializeComponent();
            GetButtons();
        }

        void GetButtons()
        {
            var registerButton = new Button
            {
                Text = "Register",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 22
            };

            registerButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new RegisterPage());
            };

            var singinButton = new Button
            {
                Text = "SingIn",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                CornerRadius = 22
            };

            stackLayout.Children.Add(registerButton);
            stackLayout.Children.Add(singinButton);
        }
    }
}