using System;
using System.Collections.Generic;
using WebCamHandler.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;

namespace WebCamHandler.Hubs
{
    public class Chatter
    {
        private readonly static Lazy<Chatter> _instance = new Lazy<Chatter>(() => new Chatter(GlobalHost.ConnectionManager.GetHubContext<ChatterHub>().Clients));

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private Chatter(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static Chatter Instance
        {
            get { return _instance.Value; }
        }

        private List<string> _adminConnectionIds = new List<string>();

        public void AddAdminToList(string connectionId)
        {
            if(!_adminConnectionIds.Exists(x => x == connectionId))
            {
                _adminConnectionIds.Add(connectionId);
            }
        }

        public void SendAdminMessage(string message)
        {
            foreach (string connectionId in _adminConnectionIds)
            {
                Clients.Client(connectionId).sendAdminMessage(message);
            }
        }
    }
}