using System;
using Firebase.Iid;
using notificationwebinar.Droid.NotificationServices;
using notificationwebinar.Services;
using static Android.Provider.Settings;

[assembly: Xamarin.Forms.Dependency(typeof(DeviceInstallation))]
namespace notificationwebinar.Droid.NotificationServices
{
    public class DeviceInstallation : IDeviceInstallationService
    {
        public string PushChannel => FirebaseInstanceId.Instance.Token;
        public string Platform => "gcm";
        public string InstallationId => Secure.GetString(Android.App.Application.Context.ContentResolver, Secure.AndroidId);
    }
}
