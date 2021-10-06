using Acr.UserDialogs;
using Firebase.Auth;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class MainFlyoutPageViewModel : ViewModelBase
    {
        public DelegateCommand LogoutCommand { get; }
        INavigationService _navigation;

        // Delete this
        public DelegateCommand ID { get; }

        public MainFlyoutPageViewModel(INavigationService navigation) : base(navigation)
        {
            LogoutCommand = new DelegateCommand(logout);

            _navigation = navigation;

            // Delete this
            ID = new DelegateCommand(() =>  GetID());
        }

        // Delete the following function
        private void GetID()
        {
            UserDialogs.Instance.Alert(TokenService.GetUserID(), "User ID", "OK");
        }

        private async void logout()
        {
            TokenService.RemoveTokenPreference();
            await _navigation.NavigateAsync("/LoginPage");
        }

    }
}
