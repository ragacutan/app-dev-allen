using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class VolunteerProperty
    {
        public int VolunteerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string HomeAddress { get; set; }
        public string Organization { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}