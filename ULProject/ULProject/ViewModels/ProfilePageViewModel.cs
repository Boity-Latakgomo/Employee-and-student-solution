using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULProject.Models;
using ULProject.Services;
using Xamarin.Essentials;

namespace ULProject.ViewModels
{
    public class ProfilePageViewModel : BindableBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public ProfilePageViewModel()
        {
            PopulateUserDetails();

        }

        private void PopulateUserDetails()
        {

            UserDetails user = JsonConvert.DeserializeObject<UserDetails>(Preferences.Get(Constants.UserDetails, string.Empty));

            Name = user.FullName;
            Surname = user.Surname;
            PhoneNumber = user.PhoneNumber;
            EmailAddress = user.EmailAddress;
        }
    }
}
