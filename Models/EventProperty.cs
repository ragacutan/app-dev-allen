using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class EventProperty
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventPax { get; set; }
        public string EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventDescription { get; set; }
    }
}