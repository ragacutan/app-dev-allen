using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class TrainingProperty
    {
        public int TrainingId { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDate { get; set; }
        public string TrainingLocation { get; set; }
        public string TrainingDescription { get; set; }
    }
}