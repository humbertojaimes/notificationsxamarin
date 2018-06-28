using System;
using notificationwebinar.Services;
using UIKit;
using notificationwebinar.iOS.NotificationServices;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInstallation))]
namespace notificationwebinar.iOS.NotificationServices
{
    public class DeviceInstallation : IDeviceInstallationService
    {
            public string InstallationId => UIDevice.CurrentDevice.IdentifierForVendor.ToString();

            public string PushChannel => AppDelegate.InstallationId.Description
                    .Trim('<', '>').Replace(" ", string.Empty).ToUpperInvariant();

            public string Template => "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

            public string Platform => "apns";
    }
}
