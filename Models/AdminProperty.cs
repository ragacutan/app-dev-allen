using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class AdminProperty
    {
        // GET: adminModel
        public int adminId { get; set; }
        public string adminEmail { get; set; }
        public string adminPassword { get; set; }
        public string adminName { get; set; }
    }
}