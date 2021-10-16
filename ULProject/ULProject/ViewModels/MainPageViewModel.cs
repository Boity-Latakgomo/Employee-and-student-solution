using Acr.UserDialogs.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; }
        //public DelegateCommand LogoutCommand { get; }
        public DelegateCommand LeaveApplicationOptionCommand { get; }
        public DelegateCommand ProfileCommand { get; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Home";
            _navigationService = navigationService;
            //LogoutCommand = new DelegateCommand(logout);
            LeaveApplicationOptionCommand = new DelegateCommand(navigateToLeaveApplication);
            ProfileCommand = new DelegateCommand(navigateToProfile);
        }

        private async void navigateToProfile()
        {
            await _navigationService.NavigateAsync("ProfilePage");
        }
        private async void navigateToLeaveApplication()
        {
            await _navigationService.NavigateAsync("ApplicationForLeavePage");
        }

        //private async void logout()
        //{
        //    Preferences.Remove("FirebaseRefreshToken");
        //    await _navigationService.NavigateAsync("/LoginPage");
        //}

    }
}
