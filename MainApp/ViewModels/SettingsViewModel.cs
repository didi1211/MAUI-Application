using System.Windows.Input;

namespace MainApp.ViewModels;

public class SettingsViewModel
{
    public string ConnectionString { get; set; }
    public string EmailServiceConnection { get; set; }
    public string Email { get; set; }

    public ICommand SaveCommand { get; }

    public SettingsViewModel()
    {
        ConnectionString = Preferences.Default.Get("ConnectionString", "");
        EmailServiceConnection = Preferences.Default.Get("EmailServiceConnection", "");
        Email = Preferences.Default.Get("Email", "");

        SaveCommand = new Command(SaveSettings);
    }

    private void SaveSettings()
    {
        Preferences.Default.Set("ConnectionString", ConnectionString);
        Preferences.Default.Set("EmailServiceConnection", EmailServiceConnection);
        Preferences.Default.Set("Email", Email);
    }
}
