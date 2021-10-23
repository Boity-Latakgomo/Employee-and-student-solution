using Acr.UserDialogs;
using Firebase.Auth;
using Newtonsoft.Json;
using Prism.AppModel;
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
    public class MainFlyoutPageViewModel : ViewModelBase, IPageLifecycleAware
    {
        public DelegateCommand LogoutCommand { get; }
        public string NameAndSurname { get; set; }

        INavigationService _navigation;
        public MainFlyoutPageViewModel(INavigationService navigation) : base(navigation)
        {
            LogoutCommand = new DelegateCommand(logout);

            _navigation = navigation;
        }

        private async void logout()
        {
            TokenService.RemoveTokenPreference();
            Preferences.Clear();
            //Preferences.Remove(Constants.UserDetails);
            await _navigation.NavigateAsync("/LoginPage");
        }

        public void OnAppearing()
        {
            UserDetails user = JsonConvert.DeserializeObject<UserDetails>(Preferences.Get(Constants.UserDetails, string.Empty));

            NameAndSurname = user.FullName + " " + user.Surname;
        }

        public void OnDisappearing()
        {
           
        }
    }
}
