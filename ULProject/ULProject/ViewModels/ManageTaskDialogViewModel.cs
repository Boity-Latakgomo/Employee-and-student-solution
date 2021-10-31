using Acr.UserDialogs;
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
    public class ManageTaskDialogViewModel : BindableBase, IDialogAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TaskType TaskDetails { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public ManageTaskDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            SubmitCommand = new DelegateCommand(() => OnSubmit());
        }


        private async Task OnSubmit()
        {
            UserDialogs.Instance.Loading("Loading...");

            DatabaseServices databaseServices = new DatabaseServices();
            await databaseServices.RemoveTask(TaskDetails.TaskId);

            UserDialogs.Instance.Loading().Dispose();
            UserDialogs.Instance.Toast("Task was removed successful");

            RequestClose(null);

        }
        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            TaskDetails = parameters.GetValue<TaskType>("TaskDetails");
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
