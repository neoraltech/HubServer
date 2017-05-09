using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebCamHandler.Models;

namespace WebCamHandler
{
    public class HandlerHub : Hub
    {
        private readonly Handler _handler;

        public HandlerHub() : this(Handler.Instance)
        { }

        public HandlerHub(Handler handler)
        {
            _handler = handler;
        }

        private List<AccessControl> _masterList = new List<AccessControl>();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AllowAccess(string data)
        {
            Handler.Instance.ProcessAccess(data);
        }

        public void AddComponent(string data)
        {
            Handler.Instance.AddComponent(data);
        }

        public void RemoveComponent(string data)
        {
            Handler.Instance.RemoveComponent(data);
        }
    }
}