using System;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Firebase.Messaging;

namespace notificationwebinar.Droid.NotificationServices
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class AppFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "NHubFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            var messageBody = message.Data["message"];
            var bigmessage = string.Empty;//;message.Data["bigmessage"];
            var subtitle = string.Empty;//message.Data["subtitle"];
            if (string.IsNullOrWhiteSpace(messageBody))
                return;

            if (string.IsNullOrWhiteSpace(bigmessage))
                SendNotification(messageBody);
            else
                SendNotification(messageBody,subtitle,bigmessage);
        }



        void SendNotification(string messageBody)
        {
            //Display notification however necessary
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetSmallIcon(Resource.Drawable.Icon)
                .SetContentTitle("FCM Message")
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }

        void SendNotification(string messageBody, string subtitle, string message)
        {
            //Display notification however necessary
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Android.Support.V4.App.NotificationCompat.Builder(this)
                .SetSmallIcon(Resource.Drawable.Icon)
                 .SetStyle(new NotificationCompat.BigTextStyle().BigText(message))
                 .SetContentTitle("title")
                  .SetContentText(subtitle)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }

    }
}
