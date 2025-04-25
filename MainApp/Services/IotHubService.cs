using MainApp.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;


namespace MainApp.Services;

public class IotHubService
{
    private RegistryManager? _registryManager;

    public void Init(string connectionString)
    {
        _registryManager = RegistryManager.CreateFromConnectionString(connectionString);
    }


    public async Task<List<IotDevice>> GetDevicesAsync()
    {
        try
        {
            var devices = await _registryManager!.GetDevicesAsync(100);
            return devices.Select(d => new IotDevice
            {
                DeviceId = d.Id,
                Status = d.ConnectionState.ToString()
            }).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning av devices: {ex.Message}");
            return new List<IotDevice>(); 
        }
    }



    public async Task RemoveDeviceAsync(string deviceId)
    {
        try
        {
            await _registryManager!.RemoveDeviceAsync(deviceId);
        }
        catch (DeviceNotFoundException ex)
        {
            Console.WriteLine($"Enheten '{deviceId}' kunde inte tas bort: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid borttagning: {ex.Message}");
        }
    }

}
