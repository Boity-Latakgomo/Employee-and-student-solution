using Acr.UserDialogs.Infrastructure;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ULProject.Models;

namespace ULProject.Services
{
    public class DatabaseServices : IDatabaseServices
    {
        #region firebase constants
        static string auth = "xCWrwXdGYLhPoU8eAKG2D1DG2TQp7xuGTcRvfQGV"; //  app secret

        //FirebaseClient firebase = new FirebaseClient("https://practice-ce7fe-default-rtdb.firebaseio.com/");
        FirebaseClient firebase = new FirebaseClient("https://practice-ce7fe-default-rtdb.firebaseio.com/", 
            new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(auth) });
        #endregion

        public async Task<bool> AddLeaveApplicationDetails(string inputLeave, string inputNumberOfDays, string inputDescription)
        {
              FirebaseObject<LeaveApplication> response = await firebase
              .Child("EmployeeSolution")
              .Child("LeaveApplication")
              .Child(TokenService.GetUserID())
              //.PostAsync( new LeaveApplication() { EmailUserID = userID, Leave = "Sick leave", NumberOfDays = "7", Description = "Flue"});
              .PostAsync<LeaveApplication>( new LeaveApplication() { EmailUserID = TokenService.GetUserID(), Leave = inputLeave, NumberOfDays = inputNumberOfDays, Description = inputDescription });
            
            if(!string.IsNullOrEmpty(response.Key) && response.Object != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AddUser(UserRegister userDetails, string userId)
        {
            FirebaseObject<UserDetails> response = await firebase
            .Child("EmployeeSolution")
            .Child("Users")
            .Child(userId)
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
                return true;
            }
            return false;
        }

        //public async Task<bool> NoteExist()
        //{
        //    // In here we gonna perform a read based on a userId provided, just to check if there is any data before we
        //    // perform post to avoid duplicates
        //}
    }
}
