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
    public class AdminController : Controller
    {
        // GET: Admin
        VolunteerImplementation vol = new VolunteerImplementation();
        EventImplementation vent = new EventImplementation();
        JoinImplementation join = new JoinImplementation();
        public ActionResult LoginPage()
        {
            return View();
        }

        public ActionResult Verify(AdminProperty acc)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from admin_table where adminEmail ='" + acc.adminEmail + "' and adminPassword ='" + acc.adminPassword+ "'";
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
                FormsAuthentication.SetAuthCookie(acc.adminEmail, true);
                Session["emailid"] = acc.adminEmail.ToString();
                return RedirectToAction("dashboard");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return RedirectToAction("LoginPage");
            }

        }

        public ActionResult dashboard()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {
                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;


                string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection MyConn = new MySqlConnection(mainconn2);
                MySqlCommand MyCommand = MyConn.CreateCommand();
                MyCommand.CommandText = "Select count(*) as myCount from volunteer_list";
                MyConn.Open();
                int returnValue = int.Parse(MyCommand.ExecuteScalar().ToString());
                string c = returnValue.ToString();
                ViewData["Count"] = c;
                MyConn.Close();
                return View();
      

            }
            mysqlconn.Close();
            return View();
        }

        [HttpGet]
        public ActionResult addEvent()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection MyConn = new MySqlConnection(mainconn2);
                MySqlCommand MyCommand = MyConn.CreateCommand();
                MyCommand.CommandText = "Select count(*) as myCount from event_list";
                MyConn.Open();
                int returnValue = int.Parse(MyCommand.ExecuteScalar().ToString());
                string c = returnValue.ToString();
                ViewData["Count"] = c;
                MyConn.Close();


                ModelState.Clear();
                return View(vent.GetEvent());
            }
            mysqlconn.Close();
            return View();
        }

   
        [HttpPost]
        public ActionResult addEvent(EventProperty addEvent)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (vent.insertEvent(addEvent))
                    {
                        ViewBag.message = "Record Saved Successfully!";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("AddEvent");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult volunteerList()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(vol.GetVolunteer());
            }
            mysqlconn.Close();
            return View();
        }
        public ActionResult ConfirmEmail()
        {
            return View();
        }

        public ActionResult Verify2(VolunteerProperty confirm)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from volunteer_list where Email ='" + confirm.Email + "'";
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
                FormsAuthentication.SetAuthCookie(confirm.Email, true);
                Session["emailid2"] = confirm.Email.ToString();
                return RedirectToAction("UpdateProfile");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return RedirectToAction("volunteerList");
            }

        }


        [HttpGet]
        public ActionResult UpdateProfile()
        {
            string displayEmail = (string)Session["emailid2"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select LastName, FirstName, HomeAddress, Age, Sex, Organization, Email, ContactNumber from volunteer_list where email ='" + displayEmail + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("Email", Session["emailid2"].ToString());
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
                return RedirectToAction("volunteerList");
            }
            catch
            {
                return View();
            }
        }

        public bool volunteerUpdate(VolunteerProperty empupdate)
        {
            var username = Session["emailid2"];
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

        public ActionResult volunteerArchive()
        {
            return View();
        }
        public ActionResult Verify3(VolunteerProperty archive)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from volunteer_list where Email ='" + archive.Email + "'";
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
                FormsAuthentication.SetAuthCookie(archive.Email, true);
                Session["emailid3"] = archive.Email.ToString();
                return RedirectToAction("archiveProfile");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return RedirectToAction("volunteerList");
            }
        }

        [HttpGet]
        public ActionResult archiveProfile()
        {
            string displayEmail = (string)Session["emailid3"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select LastName, FirstName, HomeAddress, Age, Sex, Organization, Email, ContactNumber, Password from volunteer_list where email ='" + displayEmail + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("Email", Session["emailid3"].ToString());
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

                string p = sdr["Password"].ToString();
                ViewData["Password"] = p;

                string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                string sqlquery2 = "DELETE FROM volunteer_list WHERE email ='" + displayEmail+"'";
                MySqlConnection mysqlconn2 = new MySqlConnection(mainconn2);
                MySqlCommand sqlcomm2 = new MySqlCommand(sqlquery2, mysqlconn2);
                mysqlconn2.Open();
                int i = sqlcomm2.ExecuteNonQuery();
                mysqlconn2.Close();
            }
            mysqlconn.Close();
            return View();
        }

       [HttpPost]
        public ActionResult archiveProfile(VolunteerProperty archiveVol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (insertVolunteer(archiveVol))
                    {
                        ViewBag.message = "Record Saved Successfully";
                        ModelState.Clear();
                    }

                }
                return RedirectToAction("archiveList");
            }
            catch
            {
                return View();
            }
        }

        public bool insertVolunteer(VolunteerProperty archiveVol)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "insert into archive_list (LastName,FirstName,MiddleInitial,Age,Sex,HomeAddress,Organization,ContactNumber,Email,Password) values ('" + archiveVol.LastName + "','" + archiveVol.FirstName + "','" + archiveVol.MiddleInitial + "','" + archiveVol.Age + "','" + archiveVol.Sex + "','" + archiveVol.HomeAddress + "', '" + archiveVol.Organization + "', '" + archiveVol.ContactNumber + "', '" + archiveVol.Email + "', '" + archiveVol.Password + "')";
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

        public ActionResult archiveList()
        {
            string displayprofile2 = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile2 + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(vol.GetArchive());
            }
            mysqlconn.Close();
            return View();
        }


        public ActionResult volunteerUnarchive()
        {
            return View();
        }

        public ActionResult Verify4(VolunteerProperty archive)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from archive_list where Email ='" + archive.Email + "'";
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
                FormsAuthentication.SetAuthCookie(archive.Email, true);
                Session["emailid4"] = archive.Email.ToString();
                return RedirectToAction("UnarchiveProfile");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return RedirectToAction("volunteerList");
            }
        }

        [HttpGet]
        public ActionResult UnarchiveProfile()
        {
            string displayEmail = (string)Session["emailid4"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select LastName, FirstName, HomeAddress, Age, Sex, Organization, Email, ContactNumber, Password from archive_list where email ='" + displayEmail + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("Email", Session["emailid4"].ToString());
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

                string p = sdr["Password"].ToString();
                ViewData["Password"] = p;

                string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                string sqlquery2 = "DELETE FROM archive_list WHERE email ='" + displayEmail + "'";
                MySqlConnection mysqlconn2 = new MySqlConnection(mainconn2);
                MySqlCommand sqlcomm2 = new MySqlCommand(sqlquery2, mysqlconn2);
                mysqlconn2.Open();
                int i = sqlcomm2.ExecuteNonQuery();
                mysqlconn2.Close();
            }
            mysqlconn.Close();
            return View();
        }

        [HttpPost]
        public ActionResult UnarchiveProfile(VolunteerProperty archiveVol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (insertVolunteer2(archiveVol))
                    {
                        ViewBag.message = "Record Saved Successfully";
                        ModelState.Clear();
                    }

                }
                return RedirectToAction("volunteerList");
            }
            catch
            {
                return View();
            }
        }

        public bool insertVolunteer2(VolunteerProperty archiveVol)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "insert into volunteer_list (LastName,FirstName,MiddleInitial,Age,Sex,HomeAddress,Organization,ContactNumber,Email,Password) values ('" + archiveVol.LastName + "','" + archiveVol.FirstName + "','" + archiveVol.MiddleInitial + "','" + archiveVol.Age + "','" + archiveVol.Sex + "','" + archiveVol.HomeAddress + "', '" + archiveVol.Organization + "', '" + archiveVol.ContactNumber + "', '" + archiveVol.Email + "', '" + archiveVol.Password + "')";
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

        public ActionResult Verify5(VolunteerProperty delete)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select * from archive_list where Email ='" + delete.Email + "'";
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
                FormsAuthentication.SetAuthCookie(delete.Email, true);
                Session["emailid5"] = delete.Email.ToString();
                return RedirectToAction("DeleteProfile");
            }
            else
            {
                mysqlconn.Close();
                ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                return View();
            }
        }

        public ActionResult DeleteProfile()
        {
            string displayEmail = (string)Session["emailid5"];
            string mainconn2 = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery2 = "DELETE FROM archive_list WHERE email ='" + displayEmail + "'";
            MySqlConnection mysqlconn2 = new MySqlConnection(mainconn2);
            MySqlCommand sqlcomm2 = new MySqlCommand(sqlquery2, mysqlconn2);
            mysqlconn2.Open();
            int i = sqlcomm2.ExecuteNonQuery();
            mysqlconn2.Close();
            return View();
        }

        public ActionResult Nesting()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(vol.GetVolunteer());
            }
            mysqlconn.Close();
            return View();
        }

        public ActionResult Hatchling()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(vol.GetVolunteer());
            }
            mysqlconn.Close();
            return View();
        }

        public ActionResult dashboard2()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(vol.GetVolunteer());
            }
            mysqlconn.Close();
            return View();
        }

        public ActionResult joinedVolunteer()
        {
            ModelState.Clear();
            return View(join.GetJoin());

        }

        public ActionResult joinedVol()
        {
            string displayprofile = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string sqlquery = "select adminName from admin_table where adminEmail ='" + displayprofile + "'";
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            sqlcomm.Parameters.AddWithValue("adminEmail", Session["emailid"].ToString());
            MySqlDataReader sdr = sqlcomm.ExecuteReader();
            //String Lastname
            if (sdr.Read())
            {

                string a = sdr["adminName"].ToString();
                ViewData["AdminName"] = a;

                ModelState.Clear();
                return View(join.GetJoin());
            }
            mysqlconn.Close();
            return View();

        }
    }
}