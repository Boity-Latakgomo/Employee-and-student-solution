using Acr.UserDialogs;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ULProject.Models;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class EditUserDetailsDialogViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand EditProfileCommand { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        private string email;
        private string userID;

        private UserDetails unchangedUserdetails;
        public EditUserDetailsDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            EditProfileCommand = new DelegateCommand(EditProfile);
        }

        private async void EditProfile()
        {
            if(unchangedUserdetails.FullName == Name && unchangedUserdetails.Surname == Surname &&
                unchangedUserdetails.PhoneNumber == PhoneNumber)
            {
                RequestClose(null);
                UserDialogs.Instance.Toast("No changes have been made");
            }
            else
            {
                if (PhoneNumberValid())
                {

                    UserDialogs.Instance.Loading("Updating...");
                    DatabaseServices databaseServices = new DatabaseServices();

                    string userID = TokenService.GetUserID();
                    await databaseServices.UpdateUserDetails(userID, new UserDetails
                    {
                        FullName = Name,
                        Surname = Surname,
                        PhoneNumber = PhoneNumber,
                        EmailAddress = email,
                        EmailUserID = userID
                    });
                    Preferences.Remove(Constants.UserDetails);


                    UserDetails user = await databaseServices.GetUser(userID);
                    var serializedUserDetails = JsonConvert.SerializeObject(user);
                    Preferences.Set(Constants.UserDetails, serializedUserDetails);


                    RequestClose(null);
                    UserDialogs.Instance.Loading().Dispose();
                    UserDialogs.Instance.Toast("Updated Successfully");
                }

            }

        }

        private bool PhoneNumberValid()
        {
            if (!PhoneNumber.StartsWith("07") && !PhoneNumber.StartsWith("06") && !PhoneNumber.StartsWith("08"))
            {
                // Please enter a valid SA phone number 
                UserDialogs.Instance.Toast("Enter valid phone number");
                UserDialogs.Instance.Loading().Dispose();
                return false;
            }
            else if (PhoneNumber.Length != 10)
            {
                // The digits of your phone number must be 10
                UserDialogs.Instance.Toast("Phone number must contain 10 digits");
                UserDialogs.Instance.Loading().Dispose();
                return false;
            }
            return true;
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

            unchangedUserdetails = parameters.GetValue<UserDetails>("UserDetails");
            UserDetails user = parameters.GetValue<UserDetails>("UserDetails");

            Name = user.FullName;
            Surname = user.Surname;
            PhoneNumber = user.PhoneNumber;
            email = user.EmailAddress;
            userID = user.EmailUserID;

            NotifyPropertyChanged();


        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
