using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using notificationwebinar.Functions.NotificationsServices;

namespace notificationwebinar.Functions
{
    public static class Function1
    {
        [FunctionName("RegisterDevice")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            try
            {
                var content = await req.Content.ReadAsStringAsync();
                DeviceInstallation deviceUpdate = await req.Content.ReadAsAsync<DeviceInstallation>();
                await NotificationManager.RegisterDevice(deviceUpdate);
                await NotificationManager.SendBroadcastNotification("Notificación en el webinar");

            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
            }
            return req.CreateResponse(HttpStatusCode.OK);

        }
    }
}
