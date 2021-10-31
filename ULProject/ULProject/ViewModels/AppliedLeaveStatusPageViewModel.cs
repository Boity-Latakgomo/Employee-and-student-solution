using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;

namespace ULProject.ViewModels
{
    public class AppliedLeaveStatusPageViewModel : BindableBase
    {
        public IList<LeaveApplication> Leaves { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public AppliedLeaveStatusPageViewModel(IDialogService dialogService)
        {
            RefreshCommand = new DelegateCommand(() => LoadLeaves());
            LoadLeaves();
            //Leaves = new List<LeaveApplication>
            //{
            //    new LeaveApplication{Status = "Accepted", Email = "eeeadwdwdwdwdwd", Leave = "Test", Description = "tttwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwd", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy"), Comment = "commnet1"},
            //    new LeaveApplication{Status = "Accepted", Email = "eeeadwdwdwdwdwd", Leave = "Test", Description = "tttwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwdwd", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy"), Comment = "commnet2"},
            //    new LeaveApplication{Status = "Accepted", Email = "aaa", Leave = "trr", Description = "Test", StartDate = new DateTime(2020, 10, 10).ToString("dd/MM/yyyy"), EndDate = new DateTime(2021, 11, 11).ToString("dd/MM/yyyy"), Comment = "commnet3"},
            //    new LeaveApplication{Status = "Accepted", Email = "sdf", Leave = "taa", Description = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy"), Comment = "commnet4"},
            //    new LeaveApplication{Status = "Accepted", Email = "sdg", Leave = "tee", Description = "Test", StartDate = DateTime.Now.ToString("dd/MM/yyyy"), EndDate = DateTime.Now.ToString("dd/MM/yyyy"), Comment = "commnet5"}
            //};
        }
        
        private async Task LoadLeaves()
        {
            UserDialogs.Instance.Loading("Loading...");
            DatabaseServices databaseServices = new DatabaseServices();
            string userID = TokenService.GetUserID();
            Leaves = await databaseServices.GetAllAppliedLeaves(userID);

            UserDialogs.Instance.Loading().Dispose();
        }
    }
}
