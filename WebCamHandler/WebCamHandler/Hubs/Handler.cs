using System;
using System.Collections.Generic;
using WebCamHandler.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebCamHandler
{
    public class Handler
    {
        private readonly static Lazy<Handler> _instance = new Lazy<Handler>(() => new Handler(GlobalHost.ConnectionManager.GetHubContext<HandlerHub>().Clients));

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private Handler(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static Handler Instance
        {
            get { return _instance.Value; }
        }

        private List<AccessControl> _masterList = new List<AccessControl>();

        public void ProcessAccess(string data)
        {
            Dictionary<string, string> pushData = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            string connectionId = pushData["ConnectionId"];
            string ipAddress = pushData["IpAddress"];

            if(_masterList.Exists(x => x.ConnectionId == connectionId))
            {
                Clients.Client(connectionId).processAccess(ipAddress);
            }
        }

        public void AddComponent(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    AccessControl accessdata = JsonConvert.DeserializeObject<AccessControl>(data);

                    if (accessdata != null)
                    {
                        if (_masterList.Exists(x => x.Name == accessdata.Name))
                        {
                            //Some Logic Here Soon.
                        }
                        else
                        {
                            _masterList.Add(accessdata);
                        }
                    }

                    string masterData = JsonConvert.SerializeObject(_masterList);

                    Clients.All.broadCastData(masterData);
                }
            }
            catch (Exception)
            {

            }
        }

        public void RemoveComponent(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    AccessControl accessdata = JsonConvert.DeserializeObject<AccessControl>(data);

                    if (accessdata != null)
                    {
                        _masterList.Remove(accessdata);
                    }

                    string masterData = JsonConvert.SerializeObject(_masterList);

                    Clients.All.broadCastData(masterData);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
