using Newtonsoft.Json;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class ProfilePageViewModel : BindableBase, IPageLifecycleAware
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        private string emailUserId;
        public DelegateCommand OpenDialogForEditCommand { get; }
        private IDialogService _dialogService;
        public ProfilePageViewModel(IDialogService dialogService)
        {
            
            OpenDialogForEditCommand = new DelegateCommand(OpenDialogForEdit);
            _dialogService = dialogService;

        }

        private async void OpenDialogForEdit()
        {
            var parameters = new DialogParameters
            {
                { "UserDetails", new UserDetails{
                    FullName = Name,
                    EmailAddress = EmailAddress,
                    PhoneNumber = PhoneNumber,
                    Surname = Surname,
                    EmailUserID = emailUserId
                } }
            };
            _dialogService.ShowDialog("EditUserDetailsDialog", parameters);

        }

        private void PopulateUserDetails()
        {

            UserDetails user = JsonConvert.DeserializeObject<UserDetails>(Preferences.Get(Constants.UserDetails, string.Empty));

            Name = user.FullName;
            Surname = user.Surname;
            PhoneNumber = user.PhoneNumber;
            EmailAddress = user.EmailAddress;
            emailUserId = user.EmailUserID;

        }

        public void OnAppearing()
        {
            PopulateUserDetails();
        }

        public void OnDisappearing()
        {
           
        }
    }
}
