////////////////////////////////////////////////////////
// Copyright (c) 2021 Sameer Khandekar                //
// License: MIT License.                              //
////////////////////////////////////////////////////////
using System;
using System.Diagnostics;

using Microsoft.Azure.Devices;

using Xamarin.Forms;

namespace CatToy
{
    public partial class MainPage : ContentPage
    {
        // Commands to start and stop game
        const string StartCommand = "startGame";
        const string StopCommand = "stopGame";

        // connection string for your IoTHub
        string _connectionString = "Your connection string";
        // id of your device as defined in the IoTHub
        readonly string _deviceId = "Your Device Id";

        // client to communicate with the hub
        ServiceClient _svcClient;

        public MainPage()
        {
            InitializeComponent();

            // Humidity and temperature is sent via telemetry
            // In the future, it will be displayed here.
            // TBD - lack of time
            txtHumidity.Text = "TBD";
            txtTemp.Text = "TBD";

            try
            {
                // create the service client
                _svcClient = ServiceClient.CreateFromConnectionString(_connectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
        }

        private async void btnStart_Clicked(object sender, EventArgs e)
        {
            // send the start command
            CloudToDeviceMethod cloudToDeviceMethod = new CloudToDeviceMethod(StartCommand);
            await _svcClient.InvokeDeviceMethodAsync(_deviceId, cloudToDeviceMethod).ConfigureAwait(false);
        }

        private async void btnStop_Clicked(object sender, EventArgs e)
        {
            // send the stop command
            CloudToDeviceMethod cloudToDeviceMethod = new CloudToDeviceMethod(StopCommand);
            await _svcClient.InvokeDeviceMethodAsync(_deviceId, cloudToDeviceMethod).ConfigureAwait(false);
        }
    }
}
