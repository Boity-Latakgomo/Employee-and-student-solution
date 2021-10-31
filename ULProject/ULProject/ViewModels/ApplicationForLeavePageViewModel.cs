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
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
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
            if (SelectedLeave != null && !string.IsNullOrEmpty(Description))
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
            string stringStartDate = StartDate.ToString("dd/MM/yyyy");
            string stringEndDate = EndDate.ToString("dd/MM/yyyy");
            string userEmail = TokenService.GetUserEmail();
            
            Random random = new Random();
            int randomNumber = random.Next(10, 100000);
            string leaveID = randomNumber.ToString();

            UserDialogs.Instance.Loading("Saving...");
            DatabaseServices databaseService = new DatabaseServices();
            bool isSuccessful = await databaseService.AddLeaveApplicationDetails(userEmail, Leave, Description, stringStartDate, stringEndDate, leaveID);
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
