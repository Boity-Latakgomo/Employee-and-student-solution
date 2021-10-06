using Acr.UserDialogs;
using Firebase.Auth;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    { 
        public DelegateCommand ToLoginCommand { get; }
        public DelegateCommand RegisterCommand { get; }
        public INavigationService _navigationService { get; }

        public RegisterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            ToLoginCommand = new DelegateCommand(LoginLinkCommand);
            RegisterCommand = new DelegateCommand(Register);
            _navigationService = navigationService;
        }

        private void Register()
        {
            UserDialogs.Instance.Loading();

            // Check for connectivity before making any connections
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Alert("You are not connected to internet", "Alert", "Ok");
                return;
            }

            if (!string.IsNullOrEmpty(RegisterFullNames) && !string.IsNullOrEmpty(RegisterSurname) &&
                !string.IsNullOrEmpty(RegisterEmail) && !string.IsNullOrEmpty(RegisterPassword) && !string.IsNullOrEmpty(RegisterPhoneNumber)) {

                // Checking for individual field validation before proceeding
                if (AllFieldsValidated())
                {
                    UserRegister create_user_details = new UserRegister()
                    {
                        FullName = RegisterFullNames, // use this to create a user in a realtime database
                        Surname = RegisterSurname,    // use this to create a user in a realtime database
                        PhoneNumber = RegisterPhoneNumber,    // use this to create a user in a realtime database
                        EmailAddress = RegisterEmail, // use this to create a user in a realtime database
                        Password = RegisterPassword
                    };


                    CreateUser(create_user_details);
                }
                else
                {
                    UserDialogs.Instance.Loading().Dispose();
                }
            }
            else
            {
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Toast("Please enter all details");
            }
        }

        private bool AllFieldsValidated()
        {
            bool FoundValidField = true;
            // Email validation 
            try
            {
                MailAddress m = new MailAddress(RegisterEmail);
            }
            catch (FormatException)
            {
                UserDialogs.Instance.Toast("Enter valid email address");
                FoundValidField = false;
            }

            // Phone number validation
            if (!RegisterPhoneNumber.StartsWith("07") && !RegisterPhoneNumber.StartsWith("06") && !RegisterPhoneNumber.StartsWith("08"))
            {
                // Please enter a valid SA phone number 
                FoundValidField = false;
            }
            else if (RegisterPhoneNumber.Length != 10) 
            {
                // The digits of your phone number must be 10
                FoundValidField = false;
            }

            if(RegisterPassword.Length < 6)
            {
                // Password must have 6 or more characters
                FoundValidField = false;
            }

            return FoundValidField;
        }

        private async void CreateUser(UserRegister user_details)
        {
            // firebaseAuth sign up
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(user_details.EmailAddress, user_details.Password);
                string userId = auth.User.LocalId;

                DatabaseServices databaseServices = new DatabaseServices();
                var UserAdded = await databaseServices.AddUser(user_details, userId);

                if(!UserAdded)
                {
                    // delete user account and prompt a user to register again
                    // return;
                }

                await _navigationService.NavigateAsync("/LoginPage");
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Toast("Sign up successful");

                //await App.Current.MainPage.DisplayAlert("Alert", gettoken, "Ok");

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Loading().Dispose();
                await UserDialogs.Instance.AlertAsync("Failed to signup, something went wrong", "Error", "OK");
            }


        }

        private async void LoginLinkCommand()
        {
            await _navigationService.NavigateAsync("/LoginPage");
            UserDialogs.Instance.Loading().Dispose();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

        }

    }
}
