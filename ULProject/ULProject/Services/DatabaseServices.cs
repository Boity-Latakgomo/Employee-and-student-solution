using Acr.UserDialogs.Infrastructure;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULProject.Models;

namespace ULProject.Services
{
    public class DatabaseServices : IDatabaseServices
    {
        #region firebase constants
        private static string auth = "ySbSRzvoXD2Z26pGfePP2PsUe3BWEYDFdbjfoOCm"; //  app secret

        //FirebaseClient firebase = new FirebaseClient("https://practice-ce7fe-default-rtdb.firebaseio.com/");
        FirebaseClient firebase = new FirebaseClient("https://employee-and-student-solution-default-rtdb.firebaseio.com/", 
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(auth) });
        #endregion

        public async Task<bool> AddLeaveApplicationDetails(string userEmail, string inputLeave, string inputDescription, string startDate, string endDate, string leaveID)
        {
              FirebaseObject<LeaveApplication> response = await firebase
              .Child("EmployeeSolution")
              .Child("LeaveApplication")
              //.PostAsync( new LeaveApplication() { EmailUserID = userID, Leave = "Sick leave", NumberOfDays = "7", Description = "Flue"});
              .PostAsync<LeaveApplication>( new LeaveApplication() 
              {
                 Email = userEmail,
                 EmailUserID = TokenService.GetUserID(),
                 Leave = inputLeave,
                 Description = inputDescription,
                 StartDate = startDate,
                 EndDate = endDate,
                 Status = "Pending",
                 Comment = "",
                 LeaveID = leaveID
              });
            
            if(!string.IsNullOrEmpty(response.Key) && response.Object != null)
            {
                return true;
            }
            return false;
        }

        public async Task SubmitOpportunity(Opportunity opportunity)
        {
            FirebaseObject<Opportunity> response = await firebase
              .Child("StudentSolution")
            .Child("Opportunities")
            .PostAsync<Opportunity>(new Opportunity()
            {
                Title = opportunity.Title,
                Details = opportunity.Details
            });
        }

        public async Task<bool> AddUser(UserRegister userDetails, string userId)
        {
            FirebaseObject<UserDetails> response = await firebase
            .Child("EmployeeSolution")
            .Child("Users")
            //.PostAsync( new LeaveApplication() { EmailUserID = userID, Leave = "Sick leave", NumberOfDays = "7", Description = "Flue"});
            .PostAsync<UserDetails>(new UserDetails()
            {
                FullName = userDetails.FullName,
                Surname = userDetails.Surname,
                EmailAddress = userDetails.EmailAddress,
                PhoneNumber = userDetails.PhoneNumber,
                EmailUserID = userId
            });

            if (!string.IsNullOrEmpty(response.Key) && response.Object != null)
            {
                //await AddUserEmail(userDetails.EmailAddress);
                return true;
            }
            return false;
        }

        //private async Task AddUserEmail(string email)
        //{
        //    FirebaseObject<UserEmail> response = await firebase
        //    .Child("EmployeeSolution")
        //    .Child("UserEmails")
        //    //.PostAsync( new LeaveApplication() { EmailUserID = userID, Leave = "Sick leave", NumberOfDays = "7", Description = "Flue"});
        //    .PostAsync<UserEmail>(new UserEmail()
        //    {
        //        Email = email
        //    });
        //}


        public async Task<UserDetails> GetUser(string UserID)
        {
            var allUsers = (await firebase
             .Child("EmployeeSolution")
            .Child("Users")
              .OnceAsync<UserDetails>()).Select(item => new UserDetails
              {
                  EmailAddress = item.Object.EmailAddress,
                  EmailUserID = item.Object.EmailUserID,
                  FullName = item.Object.FullName,
                  PhoneNumber = item.Object.PhoneNumber,
                  Surname = item.Object.Surname
              }).ToList();

            return allUsers.Where(a => a.EmailUserID == UserID).FirstOrDefault();
        }

        public async Task<List<TaskType>> GetTasks(string email)
        {
            return (await firebase
             .Child("EmployeeSolution")
            .Child("EmployeeTasks")
              .OnceAsync<TaskType>()).Select(item => new TaskType
              {
                  Email = item.Object.Email,
                  Title = item.Object.Title,
                  Details = item.Object.Details,
                  Status = item.Object.Status,
                  TaskId = item.Object.TaskId
              }).Where(item => item.Email == email).ToList();
        }

        public async Task<List<LeaveApplication>> GetAllAppliedLeaves(string UserID)
        {
            var allUsers = (await firebase
             .Child("EmployeeSolution")
            .Child("LeaveApplication")
              .OnceAsync<LeaveApplication>()).Select(item => new LeaveApplication
              {
                  Leave = item.Object.Leave,
                  Description = item.Object.Description,
                  StartDate = item.Object.StartDate,
                  EndDate = item.Object.EndDate,
                  EmailUserID = item.Object.EmailUserID,
                  Comment = item.Object.Comment,
                  Email = item.Object.Email,
                  LeaveID = item.Object.LeaveID,
                  Status = item.Object.Status
                  
              }).ToList();

            return allUsers.Where(a => a.EmailUserID == UserID).ToList();
        }



        public async Task UpdateUserDetails(string EmailUserID, UserDetails userDetails)
        {
            var toUpdateUser = (await firebase
             .Child("EmployeeSolution")
            .Child("Users")
              .OnceAsync<UserDetails>()).Where(a => a.Object.EmailUserID == EmailUserID).FirstOrDefault();

            await firebase
             .Child("EmployeeSolution")
            .Child("Users")
              .Child(toUpdateUser.Key)  
              .PutAsync<UserDetails>(new UserDetails()
              {
                  FullName = userDetails.FullName,
                  PhoneNumber = userDetails.PhoneNumber,
                  Surname = userDetails.Surname,
                  EmailAddress = userDetails.EmailAddress,
                  EmailUserID = userDetails.EmailUserID
                 
              });  
        }
        

        public async Task RemoveTask(string taskID)
        {
            var toDeleteTask = (await firebase
            .Child("EmployeeSolution")
            .Child("EmployeeTasks")
            .OnceAsync<TaskType>()).Where(a => a.Object.TaskId == taskID).FirstOrDefault();
            await firebase.Child("EmployeeSolution").Child("EmployeeTasks").Child(toDeleteTask.Key).DeleteAsync();
        }

        //public async Task<bool> NoteExist()
        //{
        //    // In here we gonna perform a read based on a userId provided, just to check if there is any data before we
        //    // perform post to avoid duplicates
        //}
    }
}
