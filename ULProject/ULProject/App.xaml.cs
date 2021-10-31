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

    //github repo
    //boitylatakgomo@gmail.com -> Employee-and-Student-Solution

    // Firebase
    // This app uses firebase realtime database and firebase Auth: email -> boitumelolatakgomo@gmail.com , project -> Employee and Student solution
    // -> New Project 2 
    public partial class App
    {
        
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            GlobalResources.Initialise(); // TODO: this
            InitializeComponent(); // link this file with the corresponding XAML file

            //await NavigationService.NavigateAsync("NavigationPage/ApplicationForLeavePage");

            if (string.IsNullOrEmpty(Preferences.Get("FirebaseRefreshToken", string.Empty))) // TODO: this
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
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterDialog<EditUserDetailsDialog, EditUserDetailsDialogViewModel>();
            containerRegistry.RegisterForNavigation<TasksPage, TasksPageViewModel>();
            containerRegistry.RegisterForNavigation<AppliedLeaveStatusPage, AppliedLeaveStatusPageViewModel>();
            containerRegistry.RegisterDialog<ManageTaskDialog, ManageTaskDialogViewModel>();
            containerRegistry.RegisterForNavigation<StudentsPage, StudentsPageViewModel>();
        }
    }
}
