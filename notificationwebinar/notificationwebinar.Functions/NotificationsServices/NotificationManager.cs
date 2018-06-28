using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notificationwebinar.Functions.NotificationsServices
{
    public class NotificationManager
    {
        public const string ConnectionString = "";
        public const string NotificationHubPath = "";

        static NotificationHubClient notitifcationHubClient = NotificationHubClient.CreateClientFromConnectionString(ConnectionString, NotificationHubPath);

        public static async Task RegisterDevice(DeviceInstallation deviceUpdate)
        {

            Installation installation = new Installation();
            installation.InstallationId = deviceUpdate.InstallationId;
            installation.PushChannel = deviceUpdate.PushChannel;
            switch (deviceUpdate.Platform)
            {
                case "apns":
                    installation.Platform = NotificationPlatform.Apns;
                    break;
                case "gcm":
                    installation.Platform = NotificationPlatform.Gcm;
                    break;
                default:
                    throw new Exception("Invalid Channel");
            }
            installation.Tags = new List<string>();
            await notitifcationHubClient.CreateOrUpdateInstallationAsync(installation);

        }

        public static async Task AddTag(string installationId, string newTag)
        {
            Installation installation = await notitifcationHubClient.GetInstallationAsync(installationId);
            if (installation.Tags == null)
                installation.Tags = new List<string>();
            installation.Tags.Add($"groupid:{newTag}");
            await notitifcationHubClient.CreateOrUpdateInstallationAsync(installation);
        }

        public static async Task RemoveDevice(string installationId)
        {
            await notitifcationHubClient.DeleteInstallationAsync(installationId);
        }

        public static async Task SendBroadcastNotification(string text)
        {
            var jsonGcm = string.Format("{{\"data\":{{\"message\":\"{0}\"}}}}", text);
            await notitifcationHubClient.SendGcmNativeNotificationAsync(jsonGcm);
            var jsonApns = string.Format("{{\"aps\":{{\"alert\":\" {0}\"}}}}", text);
            await notitifcationHubClient.SendAppleNativeNotificationAsync(jsonApns);
        }

    }
}
