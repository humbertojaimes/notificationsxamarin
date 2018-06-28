using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace notificationwebinar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            await Helpers.NotificationsHelper.RegisterDevice();
        }
    }
}
