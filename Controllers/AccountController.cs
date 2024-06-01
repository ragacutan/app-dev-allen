using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Configuration;
using SEE_TURTLES_WEB_APP_FINAL.Models;
using System.Web.Security;

namespace SEE_TURTLES_WEB_APP_FINAL.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account

        VolunteerImplementation vol = new VolunteerImplementation();
        EventImplementation vent = new EventImplementation();
        TrainingImplementation train = new TrainingImplementation();
        JoinImplementation join = new JoinImplementation();

        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(VolunteerProperty insertVol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vol.insertVolunteer(insertVol))
                    {
                        ViewBag.message = "Record Saved Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("Login");   
            }
            catch
            {
                return View();
            }
        }

        public ActionResult profile()
        {
            string displayLastname = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select LastName, FirstName, HomeAddress, Age, Sex, Organization, Email, ContactNumber from volunteer_list where email ='" + displayLastname + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("Email", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {
                string l = sdr["LastName"].ToString();
                ViewData["Lastname"] = l;

                string f = sdr["FirstName"].ToString();
                ViewData["Firstname"] = f;

                string h = sdr["HomeAddress"].ToString();
                ViewData["Homeaddress"] = h;

                string a = sdr["Age"].ToString();
                ViewData["Age"] = a;

                string s = sdr["Sex"].ToString();
                ViewData["Sex"] = s;

                string o = sdr["Organization"].ToString();
                ViewData["Organization"] = o;

                string e = sdr["Email"].ToString();
                ViewData["Email"] = e;

                string c = sdr["ContactNumber"].ToString();
                ViewData["Contact"] = c;
            }
            mysqlconn.Close();
            return View();
        }

        public ActionResult Verify(VolunteerProperty acc)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from volunteer_list where email ='" + acc.Email + "' and password ='" + acc.Password + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();

            MySqlDataReader rdr = sqlcomm.ExecuteReader();

            if (rdr.Read())
            {
                string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection mysqlconn2 = new MySqlConnection(mainconn2);
                MySqlCommand sqlcomm2 = new MySqlCommand(sqlquery, mysqlconn2);
                MySqlDataReader MyReader2;
                mysqlconn2.Open();
                MyReader2 = sqlcomm2.ExecuteReader();
                while (MyReader2.Read())
                {

                }
                mysqlconn2.Close();
                FormsAuthentication.SetAuthCookie(acc.Email, true);
                Session["emailid"] = acc.Email.ToString();
                return RedirectToAction("HomePage");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return RedirectToAction("Login");
            }

        }

        public ActionResult HomePage()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select FirstName from volunteer_list where Email ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {
                string a = sdr["FirstName"].ToString();
                ViewData["Firstname"] = a;
            }
            mysqlconn.Close();
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public ActionResult UpdateProfile()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select LastName, FirstName, MiddleInitial, HomeAddress, Age, Sex, Organization, Email, ContactNumber from volunteer_list where email ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("Email", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {
                string l = sdr["LastName"].ToString();
                ViewData["Lastname"] = l;

                string f = sdr["FirstName"].ToString();
                ViewData["Firstname"] = f;

                string m = sdr["MiddleInitial"].ToString();
                ViewData["Middleinitial"] = m;

                string h = sdr["HomeAddress"].ToString();
                ViewData["Homeaddress"] = h;

                string a = sdr["Age"].ToString();
                ViewData["Age"] = a;

                string s = sdr["Sex"].ToString();
                ViewData["Sex"] = s;

                string o = sdr["Organization"].ToString();
                ViewData["Organization"] = o;

                string e = sdr["Email"].ToString();
                ViewData["Email"] = e;

                string c = sdr["ContactNumber"].ToString();
                ViewData["Contact"] = c;
            }
            mysqlconn.Close();
            return View();
        }

        [HttpPost]
        public ActionResult UpdateProfile(VolunteerProperty empupdate)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (volunteerUpdate(empupdate))
                    {
                        ViewBag.message = "Record Successfully Updated!";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("Profile");
            }
            catch
            {
                return View();
            }
        }

        public bool volunteerUpdate(VolunteerProperty empupdate)
        {
            var username = Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "update volunteer_list set LastName='" + empupdate.LastName + "', FirstName='" + empupdate.FirstName + "', MiddleInitial='" + empupdate.MiddleInitial + "', Age='" + empupdate.Age + "', Sex='" + empupdate.Sex + "', HomeAddress='" + empupdate.HomeAddress + "', Organization='" + empupdate.Organization + "', ContactNumber='" + empupdate.ContactNumber + "', Email='" + empupdate.Email + "' where Email ='" + username + "'";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            int i = sqlcomm.ExecuteNonQuery();
            mysqlconn.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult programs()
        {
            return View();
        }

        public ActionResult EventList()
        {
            ModelState.Clear();
            return View(vent.GetEvent());
        }

        public ActionResult Training()
        {

            String displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select FirstName from volunteer_list where Email ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {
                string a = sdr["FirstName"].ToString();
                ViewData["Firstname"] = a;

                ModelState.Clear();
                return View(train.GetTraining());
            }
            mysqlconn.Close();
            return View();
        }

        public ActionResult joinList()
        {
            ModelState.Clear();
            return View(join.GetJoin());
        }

        [HttpGet]
        public ActionResult Join()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Join(JoinProperty eventJoin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (join.insertJoin(eventJoin))
                    {
                        ViewBag.message = "Record Saved Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("HomePage");
            }
            catch
            {
                return View();
            }
        }
    }
}
