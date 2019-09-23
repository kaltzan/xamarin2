using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using xamarin2.Data;

namespace xamarin2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {  
        // Used for flashlight
        Boolean light = false;
       
        DetectShakeTest ds = new DetectShakeTest();


        public MainPage()
        {
            InitializeComponent();
            ds.ToggleAccelerometer();


        }

        public void switchClicked(object sender, EventArgs args)
        {   flashlight();
        }

        public async void sendClicked(object sender, EventArgs args)
        {
           await sendSms(sms.Text, receiver.Text);
        }

        public void geoClicked(object sender, EventArgs args)
        {
            getGeo();
        }


        public async void getGeo()
        {
            

            try
            {
                await DisplayAlert("1", "1", "1");
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                await DisplayAlert("2", "2", "2");
                var location = await Geolocation.GetLocationAsync(request);
                await DisplayAlert("3", "3", "3");

                if (location != null)
                {

                    await DisplayAlert("Your Geolocation", $"Latitude: {location.Latitude}\nLongitude: {location.Longitude}\nAltitude: {location.Altitude}", "done");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                await DisplayAlert("ERROR", "ERROR", "ERROR");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                await DisplayAlert("ERROR", "ERROR", "ERROR");

            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                await DisplayAlert("ERROR", "ERROR", "ERROR");

            }
            catch (Exception ex)
            {
                // Unable to get location
                await DisplayAlert("ERROR", "ERROR", "ERROR");

            }


        }

        public async Task sendSms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, new[] { recipient });
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }

        private async void flashlight()
        {
            

            try
            {
            

                if (!light)
                {
                    // Turn On
                    await Flashlight.TurnOnAsync();
                    light = true;
                }
                else
                {
                    // Turn Off
                    await Flashlight.TurnOffAsync();
                    light = false;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to turn on/off flashlight
            }
        }


    }
}
