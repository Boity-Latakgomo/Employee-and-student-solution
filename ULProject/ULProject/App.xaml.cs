using Prism;
using Prism.Ioc;
using ULProject.ViewModels;
using ULProject.Views;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ULProject
{
    // TODO:Update user token frequently

    // This app uses firebase realtime database and firebase Auth: email -> WilliamDeveloper , project -> Practice
    // Mockup, in boxfusion account -> New Project 2 
    public partial class App
    {
        
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            GlobalResources.Initialise();
            InitializeComponent();

            //await NavigationService.NavigateAsync("NavigationPage/ApplicationForLeavePage");

            if (string.IsNullOrEmpty(Preferences.Get("FirebaseRefreshToken", string.Empty)))
            {
                await NavigationService.NavigateAsync("LoginPage");
            }
            else
            {
                await NavigationService.NavigateAsync("MainFlyoutPage/NavigationPage/MainPage");
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ApplicationForLeavePage, ApplicationForLeavePageViewModel>();
            containerRegistry.RegisterForNavigation<MainFlyoutPage, MainFlyoutPageViewModel>();

            containerRegistry.RegisterDialog<ResetPasswordDialog, ResetPasswordDialogViewModel>();
        }
    }
}