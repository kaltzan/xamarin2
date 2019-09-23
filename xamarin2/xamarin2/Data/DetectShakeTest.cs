using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
namespace xamarin2.Data
{
    public class DetectShakeTest
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.Game;
        Boolean light = false;

        public DetectShakeTest()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }

        private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            try
            {


                if (!light)
                {
                    // Turn On
                    await Flashlight.TurnOnAsync();
                    light = true;
                    Vibration.Vibrate();
                }
                else
                {
                    // Turn Off
                    await Flashlight.TurnOffAsync();
                    light = false;
                    Vibration.Vibrate();
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
    

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
