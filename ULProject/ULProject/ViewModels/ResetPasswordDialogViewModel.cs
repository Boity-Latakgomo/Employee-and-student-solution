using Acr.UserDialogs;
using Firebase.Auth;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ULProject.ViewModels
{
    class ResetPasswordDialogViewModel : BindableBase, IDialogAware
    {
        public string ResetPasswordSubmittedEmail { get; set; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SendEmailResetPasswordCommand { get; }
        public ResetPasswordDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
            SendEmailResetPasswordCommand = new DelegateCommand(() => SendEmailResetPassword());
        }


        private async Task SendEmailResetPassword()
           {
            UserDialogs.Instance.Loading();
            RequestClose(null);


            try
            {
                MailAddress m = new MailAddress(ResetPasswordSubmittedEmail);
            }
            catch (FormatException)
            {
                UserDialogs.Instance.Loading().Dispose();
                UserDialogs.Instance.Toast("Enter valid email address");
            }



    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.WebAPIkey));
            // TODO: Check with a user if they did enter the correct email if the app loads more than 25 seconds
            await authProvider.SendPasswordResetEmailAsync(ResetPasswordSubmittedEmail);

            UserDialogs.Instance.Loading().Dispose();
            UserDialogs.Instance.Alert("Check your email to reset your password", "Email submitted", "Ok");
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
