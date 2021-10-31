using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;

namespace ULProject.ViewModels
{
    public class StudentsPageViewModel : BindableBase
    {
        public DelegateCommand SubmitCommand { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public StudentsPageViewModel()
        {
            SubmitCommand = new DelegateCommand(() => Submit());
        }

        private async Task Submit()
        {
            if(string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Details))
            {
                UserDialogs.Instance.Toast("Make sure all fields are filled");
                return;
            }

            UserDialogs.Instance.Loading("Loading...");

            DatabaseServices databaseServices = new DatabaseServices();
            await databaseServices.SubmitOpportunity(new Opportunity() { Title = Title, Details = Details});



            UserDialogs.Instance.Loading().Dispose();
            UserDialogs.Instance.Toast("Submitted successfully");
        }
    }
}
