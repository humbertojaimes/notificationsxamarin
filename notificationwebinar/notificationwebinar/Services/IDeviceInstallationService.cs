using System;
namespace notificationwebinar.Services
{
    public interface IDeviceInstallationService
    {
        string InstallationId { get; }

        string Platform { get; }

        string PushChannel { get; }

    }
}
