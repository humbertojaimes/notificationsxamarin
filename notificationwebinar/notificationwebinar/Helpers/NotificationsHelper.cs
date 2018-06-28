using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using notificationwebinar.Services;
using Xamarin.Forms;

namespace notificationwebinar.Helpers
{
    public class NotificationsHelper
    {
        static HttpClient client = new HttpClient();
        const string url = "";


        public static async Task<bool> RegisterDevice()
        {
            bool registrationResult = false;
            var deviceInstallation =
                DependencyService.Get<IDeviceInstallationService>();

            var jsonDeviceInfo =
                JsonConvert.SerializeObject(deviceInstallation);

            using (var response =
                   await client.PostAsync
                  (url, new StringContent(jsonDeviceInfo, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    registrationResult = true;
                }

            }

            return registrationResult;
        }
    }
}
