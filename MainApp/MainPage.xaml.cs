using MainApp.ViewModels;
using MainApp.Views;

namespace MainApp
{
    public partial class MainPage : ContentPage
    {
   

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(); 
        }

        private async void GoToSettings(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as MainViewModel;
            if (vm != null && vm.Devices.Count == 0)
            {
                await vm.LoadDevicesAsync();
            }
        }


    }

}
