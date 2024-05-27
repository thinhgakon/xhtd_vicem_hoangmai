using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HMXHTD.SignalRNotification
{
    public class ConnectSignalR
    {
        private static IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://192.168.0.10:8091/signalr"; // service ở con 0.10
       // const string ServerURI = "http://192.168.158.55:8091/signalr"; // service ở con 55
        //"http://localhost:8091/signalr";
        private static HubConnection Connection { get; set; }
        public static IHubProxy CreateConnectSignalR()
        {
            Connection = new HubConnection(ConfigurationManager.AppSettings.Get("singalRHost").ToString());
            HubProxy = Connection.CreateHubProxy("MyHub");
            try
            {
                Connection.Start().GetAwaiter().GetResult();
                return HubProxy;
            }
            catch (HttpRequestException ex)
            {

            }
            return CreateConnectSignalR();
        }
    }
}
