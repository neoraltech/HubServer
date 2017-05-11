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

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatterHub"/> class.
        /// </summary>
        public ChatterHub() : this(Chatter.Instance)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatterHub"/> class.
        /// </summary>
        /// <param name="chatter">The chatter.</param>
        public ChatterHub(Chatter chatter)
        {
            _chatter = chatter;
        }

        /// <summary>
        /// Access for Users
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public void UserAccess(string userDataJson)
        {
            Chatter.Instance.AddUserToList(userDataJson);
        }

        /// <summary>
        /// Access for Admins
        /// </summary>
        /// <param name="connectionId">The connection identifier.</param>
        public void AdminAccess(string connectionId)
        {
            Chatter.Instance.AddAdminToList(connectionId);
        }

        /// <summary>
        /// Sends the message to admin.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessageToAdmin(string connectionId, string message)
        {
            Chatter.Instance.SendAdminMessage(connectionId, message);
        }
    }
}