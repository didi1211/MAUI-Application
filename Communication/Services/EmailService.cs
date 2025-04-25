using Azure.Communication.Email;
using Azure;

namespace Communication.Services;

public class EmailService
{
    private readonly EmailClient _client;

    public EmailService(string connectionString)
    {
        _client = new EmailClient(connectionString);
    }

    public async Task SendEmailAsync(string to, string deviceId)
    {
        var sender = "DoNotReply@98934873-9745-4ef3-9e91-090eb3a3da0c.azurecomm.net";

        await _client.SendAsync(
            WaitUntil.Completed,
            senderAddress: sender,
            recipientAddress: to,
            subject: "IoT-device removed",
            plainTextContent: $"Device '{deviceId}' has been removed from you Iot Hub.",
            htmlContent: "",
            cancellationToken: CancellationToken.None);
    }

}
