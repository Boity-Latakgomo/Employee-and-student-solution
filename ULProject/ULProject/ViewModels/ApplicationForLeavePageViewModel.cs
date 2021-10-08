using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;

namespace ULProject.ViewModels
{
    public class ApplicationForLeavePageViewModel : ViewModelBase
    {
        // This is binded as ItemsSource in a picker
        public string NumberOfDays { get; set; }
        public string Description { get; set; }
        public IList<LeaveType> Leave { get; set; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        // this deals with the selected item from the picker, binded as SelectedItem
        LeaveType selectedLeave;
        public LeaveType SelectedLeave
        {
            get { return selectedLeave; }
            set
            {
                if (selectedLeave != value)
                {
                    selectedLeave = value;
                    //OnPropertyChanged();
                }
            }
        }

        private INavigationService _navigationService;
        public ApplicationForLeavePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveCommand = new DelegateCommand(ValidateData);
            CancelCommand = new DelegateCommand(CancelLeaveApplication);
            _navigationService = navigationService;
            AddLeavePickerValues();
        }

        private async void CancelLeaveApplication()
        {
            await _navigationService.GoBackAsync();
        }
        // Populating LeaveType values
        private void AddLeavePickerValues()
        {
            Leave = new List<LeaveType>()
            {
                new LeaveType{Type = "Sick Leave"},
                new LeaveType{Type = "Maternity Leave"},
                new LeaveType{Type = "Random Leave"}
            };
        }

        private async void ValidateData()
        {
            //if (SelectedLeave == null)
            //{
            //    UserDialogs.Instance.Alert("Nothing to display");
            //    return;
            //}
            //UserDialogs.Instance.Alert(SelectedLeave.Type + " selected");
            string Leave = SelectedLeave.Type;
            if (SelectedLeave != null && !string.IsNullOrEmpty(NumberOfDays) && !string.IsNullOrEmpty(Description))
            {
                Save(Leave);
            }
            else
            {
                UserDialogs.Instance.Toast("Fill all the fields");
            }
        }
        private async Task Save(string Leave)
        {
            UserDialogs.Instance.Loading("Saving...");
            IDatabaseServices databaseservice = new DatabaseServices();
            bool isSuccessful = await  databaseservice.AddLeaveApplicationDetails(Leave, NumberOfDays, Description);
            if (isSuccessful) 
            {
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Alert("Your leave application is successfully submitted", "Success", "OK");
                await _navigationService.GoBackAsync();
            }
            else
            {
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Alert("Please try again", "Failed", "OK");
            }

        }

    }
}
