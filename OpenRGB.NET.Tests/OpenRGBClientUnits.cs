using System;
using Xunit;
using OpenRGB.NET;
using System.Collections.Generic;
using System.Drawing;

namespace OpenRGB.NET.Test
{
    public class OpenRGBClientUnits
    {
        [Fact]
        public void ClientConnectToServer()
        {
            OpenRGBClient client = new OpenRGBClient(port: 1337);
            client.Connect();
            client.Disconnect();
        }

        [Fact]
        public void ClientListController()
        {
            OpenRGBClient client = new OpenRGBClient(port: 1337);
            client.Connect();
            int nbController = client.GetControllerCount();
            for (int i = 0; i < nbController; i++)
            {
                OpenRGBDevice controller = client.GetControllerData(i);
                Assert.True(!string.IsNullOrWhiteSpace(controller.Name));
            }
            client.Disconnect();
        }

        [Fact]
        public void CheckLedChange()
        {
            OpenRGBClient client = new OpenRGBClient(port: 1337, name: "OpenRGB.NET Test Application");
            client.Connect();
            var controllerCount = client.GetControllerCount();
            var devices = new List<OpenRGBDevice>();

            for (int i = 0; i < controllerCount; i++)
                devices.Add(client.GetControllerData(i));

            for (int i = 0; i < devices.Count; i++)
            {
                var data = devices[i];

                var list = new OpenRGBColor[data.Leds.Length];
                Color clr = Color.Lime;
                for (int j = 0; j < data.Leds.Length; j++)
                {
                    list[j] = new OpenRGBColor(clr.R, clr.G, clr.B);
                }
                client.UpdateLeds(i, list);
            }
            client.Disconnect();
        }
    }
}
