using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebCamHandler.Models;

namespace WebCamHandler.Hubs
{
    public class ChatterHub : Hub
    {
        private readonly Chatter _chatter;

        public ChatterHub() : this(Chatter.Instance)
        {

        }

        public ChatterHub(Chatter chatter)
        {
            _chatter = chatter;
        }

        public void AdminAccess(string connectionId)
        {
            Chatter.Instance.AddAdminToList(connectionId);
        }

        public void SendMessageToAdmin(string message)
        {
            Chatter.Instance.SendAdminMessage(message);
        }
    }
}