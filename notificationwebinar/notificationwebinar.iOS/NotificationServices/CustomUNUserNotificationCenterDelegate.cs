using System;
using UserNotifications;

namespace notificationwebinar.iOS.NotificationServices
{
    public class CustomUNUserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }


        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var res = response.Notification;
            completionHandler();
        }

    }
}
