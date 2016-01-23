using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebCamHandler.Models;

namespace WebCamHandler
{
    public class HandlerHub : Hub
    {
        private List<AccessControl> _masterList = new List<AccessControl>();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void AllowAccess(string connectionId, string ipAddress)
        {

        }

        public void AddComponent(string data)
        {
            try
            {
                if(!string.IsNullOrEmpty(data))
                {
                    AccessControl accessdata = JsonConvert.DeserializeObject<AccessControl>(data);

                    if (accessdata != null)
                    {
                        _masterList.Add(accessdata);
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