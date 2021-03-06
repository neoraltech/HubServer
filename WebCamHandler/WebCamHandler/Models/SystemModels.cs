﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCamHandler.Models
{
    public class AccessControl
    {
        public string Name { get; set; }
        public bool IsServer { get; set; }
        public string ConnectionId { get; set; }
        public string IpAddress { get; set; }
        public bool IsHost { get; set; }
    }

    public class UserData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ConnectionId { get; set; }
    }
}
