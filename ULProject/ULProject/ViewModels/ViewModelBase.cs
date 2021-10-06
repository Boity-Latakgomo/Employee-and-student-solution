using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ULProject.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        // For LoginPage
        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        // For register
        private string _registerFullNames;
        public string RegisterFullNames
        {
            get { return _registerFullNames; }
            set { SetProperty(ref _registerFullNames, value); }
        }

        private string _registerSurname;
        public string RegisterSurname
        {
            get { return _registerSurname; }
            set { SetProperty(ref _registerSurname, value); }
        }

        private string _registerPhoneNumber;
        public string RegisterPhoneNumber
        {
            get { return _registerPhoneNumber; }
            set { SetProperty(ref _registerPhoneNumber, value); }
        }

        
        private string _registerEmail;
        public string RegisterEmail
        {
            get { return _registerEmail; }
            set { SetProperty(ref _registerEmail, value); }
        }

        private string _registerUsername;
        public string RegisterUsername
        {
            get { return _registerUsername; }
            set { SetProperty(ref _registerUsername, value); }
        }

        private string _registerPassword;
        public string RegisterPassword
        {
            get { return _registerPassword; }
            set { SetProperty(ref _registerPassword, value); }
        }

        private string _registerMobileNumber;
        public string RegisterMobileNumber
        {
            get { return _registerMobileNumber; }
            set { SetProperty(ref _registerMobileNumber, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
