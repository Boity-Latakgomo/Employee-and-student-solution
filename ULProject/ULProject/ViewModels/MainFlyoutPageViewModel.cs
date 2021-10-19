using Acr.UserDialogs;
using Firebase.Auth;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class MainFlyoutPageViewModel : ViewModelBase
    {
        public DelegateCommand LogoutCommand { get; }
        public string NameAndSurname { get; set; }

        INavigationService _navigation;
        public MainFlyoutPageViewModel(INavigationService navigation) : base(navigation)
        {
            LogoutCommand = new DelegateCommand(logout);

            _navigation = navigation;

            UserDetails user = JsonConvert.DeserializeObject<UserDetails>(Preferences.Get(Constants.UserDetails, string.Empty));

            NameAndSurname = user.FullName + " " + user.Surname;


        }

        private async void logout()
        {
            TokenService.RemoveTokenPreference();
            Preferences.Remove(Constants.UserDetails);
            await _navigation.NavigateAsync("/LoginPage");
        }

    }
}
