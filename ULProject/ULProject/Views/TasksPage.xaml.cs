using Xamarin.Forms;

namespace ULProject.Views
{
    public partial class TasksPage : ContentPage
    {
        public TasksPage()
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
