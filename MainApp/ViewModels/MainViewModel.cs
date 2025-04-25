using System.Collections.ObjectModel;
using Communication.Services;
using System.Windows.Input;
using MainApp.Models;
using MainApp.Services;

namespace MainApp.ViewModels;

public class MainViewModel 
{
    public ObservableCollection<IotDevice> Devices { get; set; } = new();
    public ICommand LoadDevicesCommand { get; }
    public ICommand RemoveDeviceCommand { get; }

    private readonly IotHubService? _iotHubService;
    private readonly EmailService? _emailService;

    public MainViewModel()
    {
        var cs = Preferences.Default.Get("ConnectionString", "");
        var emailCs = Preferences.Default.Get("EmailServiceConnection", "");
        var emailTo = Preferences.Default.Get("Email", "");

        if (string.IsNullOrWhiteSpace(cs) || string.IsNullOrWhiteSpace(emailCs) || string.IsNullOrWhiteSpace(emailTo))
            throw new InvalidOperationException("Missing setting, cannot continue.");

        _iotHubService = new IotHubService();
        _iotHubService.Init(cs);

        _emailService = new EmailService(emailCs);

        LoadDevicesCommand = new Command(async () => await LoadDevicesAsync());
        RemoveDeviceCommand = new Command<string>(async id => await RemoveDevice(id, emailTo));
    }


    public async Task LoadDevicesAsync()
    {
        Devices.Clear();
        var deviceList = await _iotHubService!.GetDevicesAsync();
        foreach (var d in deviceList)
        {
            Devices.Add(d);
        }
            
    }

    private async Task RemoveDevice(string deviceId, string email)
    {
        await _iotHubService!.RemoveDeviceAsync(deviceId);
        await _emailService!.SendEmailAsync(email, deviceId);
        await LoadDevicesAsync();
    }

  
}
