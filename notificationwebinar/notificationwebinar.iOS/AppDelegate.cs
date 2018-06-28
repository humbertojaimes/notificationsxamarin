using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using notificationwebinar.iOS.NotificationServices;
using UIKit;
using UserNotifications;

namespace notificationwebinar.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            var settings = UIUserNotificationSettings.GetSettingsForTypes(
               UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
               new NSSet());

            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            UIApplication.SharedApplication.RegisterForRemoteNotifications();


            //Create accept action
            var replyId = "reply";
            var replyTitle = "Reply";
            var replyAction = UNTextInputNotificationAction.FromIdentifier(replyId, replyTitle, UNNotificationActionOptions.None, "Reply", "Message");




            //Create category
            var categoryID = "general";
            var actions = new UNNotificationAction[] { replyAction };
            var intentIDs = new string[] { };
            var categoryOptions = new UNNotificationCategoryOptions[] { };
            var category = UNNotificationCategory.FromIdentifier(categoryID, actions, intentIDs, UNNotificationCategoryOptions.None);

            //Register category
            var categories = new UNNotificationCategory[] { category };
            UNUserNotificationCenter.Current.SetNotificationCategories(new NSSet<UNNotificationCategory>(categories));



            UNUserNotificationCenter.Current.Delegate = new CustomUNUserNotificationCenterDelegate();


            if (options != null && options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
            {
                appIsStarting = true;
            }

            return base.FinishedLaunching(app, options);
        }

        #region Notification

        bool appIsStarting = false;
        public static NSData InstallationId;

        [Export("applicationDidEnterBackground:")]
        public void DidEnterBackground(UIApplication application)
        {
            appIsStarting = false;
        }


        public override void WillEnterForeground(UIApplication uiApplication)
        {
            appIsStarting = true;
        }


        [Export("applicationWillResignActive:")]
        public void OnResignActivation(UIApplication application)
        {
            appIsStarting = false;
        }


        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            AppDelegate.InstallationId = deviceToken;
        }

        public override void FailedToRegisterForRemoteNotifications(
            UIApplication application,
            NSError error)
        {
            // TODO: Show error
        }

        public override void DidReceiveRemoteNotification(
            UIApplication application,
            NSDictionary userInfo,
            Action<UIBackgroundFetchResult> completionHandler)
        {
            UIApplicationState state = application.ApplicationState;
            if (state == UIApplicationState.Background ||
                (state == UIApplicationState.Inactive &&
                !appIsStarting))
            {
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                var messageKey = new NSString("alert");
                string message = null;
                if (aps.ContainsKey(messageKey))
                    message = (aps[messageKey] as NSString).ToString();

                ShowAlert(message);

            }
            else if (state == UIApplicationState.Inactive && appIsStarting)
            {
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                var messageKey = new NSString("alert");
                string message = null;
                if (aps.ContainsKey(messageKey))
                    message = (aps[messageKey] as NSString).ToString();

                ShowAlert(message);
                // user tapped notification
                //completionHandler(UIBackgroundFetchResult.NewData);

            }
            else
            {
                NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

                var messageKey = new NSString("alert");
                string message = null;
                if (aps.ContainsKey(messageKey))
                    message = (aps[messageKey] as NSString).ToString();

                ShowAlert(message);
                // app is active             
                //completionHandler(UIBackgroundFetchResult.NoData);
            }



            void ShowAlert(string message)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    UIApplication.SharedApplication.InvokeOnMainThread(() =>
                    {
                        var alert = UIAlertController.Create(
                            "Notificación",
                            $"{message} {appIsStarting}",
                            UIAlertControllerStyle.Alert);

                        alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));

                        var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
                        while (vc.PresentedViewController != null)
                        {
                            vc = vc.PresentedViewController;
                        }

                        vc.ShowDetailViewController(alert, vc);
                    });
                }
            }



        }
        #endregion
    }
}
