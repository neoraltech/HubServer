using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCamHandler.Models;

namespace WebCamHandler.Hubs
{
    public class Chatter
    {
        /// <summary>
        /// The instance
        /// </summary>
        private readonly static Lazy<Chatter> _instance = new Lazy<Chatter>(() => new Chatter(GlobalHost.ConnectionManager.GetHubContext<ChatterHub>().Clients));

        private IHubConnectionContext<dynamic> Clients { get; set; }

        private Chatter(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Chatter Instance
        {
            get { return _instance.Value; }
        }

        private List<string> _adminConnectionIds = new List<string>();

        private List<UserData> _userConnectionIds = new List<UserData>();

        /// <summary>
        /// Adds the user to list.
        /// </summary>
        /// <param name="userDataJson">The user data json.</param>
        public void AddUserToList(string userDataJson)
        {
            UserData userData = JsonConvert.DeserializeObject<UserData>(userDataJson);

            if (!_userConnectionIds.Exists(x => x.ConnectionId == userData.ConnectionId))
            {
                _userConnectionIds.Add(userData);
            }
        }

        /// <summary>
        /// Adds the admin to list.
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public void AddAdminToList(string connectionId)
        {
            if(!_adminConnectionIds.Exists(x => x == connectionId))
            {
                _adminConnectionIds.Add(connectionId);
            }
        }

        /// <summary>
        /// Sends the admin message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendAdminMessage(string userConnectionId, string message)
        {
            UserData data = _userConnectionIds.SingleOrDefault(x => x.ConnectionId == userConnectionId); //.Select(x => x.ConnectionId == userConnectionId).Select();
            string sendMsg = data.Name + ": " + message;

            foreach (string connectionId in _adminConnectionIds)
            {
                Clients.Client(connectionId).sendAdminMessage(sendMsg);
            }
        }

        /// <summary>
        /// Sends the user message.
        /// </summary>
        /// <param name="userDataJson">The user data json.</param>
        /// <param name="message">The message.</param>
        public void SendUserMessage(string userDataJson, string message)
        {
            UserData userData = JsonConvert.DeserializeObject<UserData>(userDataJson);

            if (!_userConnectionIds.Exists(x => x.ConnectionId == userData.ConnectionId))
            {
                int userIndex = _userConnectionIds.IndexOf(userData);
                string userConnectionId = _userConnectionIds[userIndex].ConnectionId;

                Clients.Client(userConnectionId).sendUserMessage(message);
            }
        }
    }
}