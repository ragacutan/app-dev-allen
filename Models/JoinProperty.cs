using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class JoinProperty
    {
        public int VolunteerId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string volCondition { get; set; }
        public string Motivation { get; set; }
        public string Expectation { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyAddress{ get; set; }
        public string EmergencyMobile { get; set; }
        public string EmergencyRelation { get; set; }
    }
}