using Xamarin.Forms;

namespace ULProject.Views
{
    public partial class AppliedLeaveStatusPage : ContentPage
    {
        public AppliedLeaveStatusPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (((ListView)sender).SelectedItem == null)
                return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}
