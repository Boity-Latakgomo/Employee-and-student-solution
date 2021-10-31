using Acr.UserDialogs;
using Prism.AppModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;

namespace ULProject.ViewModels
{
    public class TasksPageViewModel : BindableBase, IPageLifecycleAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DelegateCommand RefreshCommand { get; set; }
        IDialogService _dialogService;
        public List<TaskType> Tasks { get; set; }
        public TasksPageViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            RefreshCommand = new DelegateCommand(() => LoadTasks());
            //Tasks = new List<TaskType> {
            //    new TaskType { Title = "Bursaries", Task = "Avail bursary to students view until 30 November"},
            //    new TaskType { Title = "October Documention", Task = "Finish with your 1 month documention and send it to Boity for review, please make sure you do that by the end on working hours and do notify Boity if she doesn't come back to you"},
            //    new TaskType { Title = "Archievements", Task = "Update your weekly archeivements for October"}, 
            //    new TaskType { Title = "Alert", Task = "Message Brian to drop me the file"}, 
            //    new TaskType { Title = "Alert", Task = "Ask Celine to check final designs"}, 
            //    new TaskType { Title = "Approval", Task = "Approve Boitumelo's request"}, 
            //    new TaskType { Title = "Grand access", Task = "Grand Boitumelo access to perfomance document"}, 
            //    new TaskType { Title = "Alert", Task = "Meet with Antony at 2.30."}, 
            //    new TaskType { Title = "Clearance", Task = "Clear all the approved tasks for this month after working hours"}

            //};
        }

        private async Task LoadTasks()
        {
            UserDialogs.Instance.Loading("Loading...");
            string userEmail = TokenService.GetUserEmail();
            DatabaseServices databaseServices = new DatabaseServices();
            Tasks = await databaseServices.GetTasks(userEmail);

            UserDialogs.Instance.Loading().Dispose();
            NotifyPropertyChanged();
        }
        public void TaskEdit(TaskType task)
        {
            var parameters = new DialogParameters
            {
                { "TaskDetails", task }
            };
            _dialogService.ShowDialog("ManageTaskDialog", parameters);
        }

        public void OnAppearing()
        {
            LoadTasks();
        }

        public void OnDisappearing()
        {
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
