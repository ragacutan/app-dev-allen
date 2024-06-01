using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using SEE_TURTLES_WEB_APP_FINAL.Models;

namespace SEE_TURTLES_WEB_APP_FINAL.Models
{
    public class VolunteerImplementation
    {
        // GET: VolunteerImplementation
        public List<VolunteerProperty> GetVolunteer()
        {
            List<VolunteerProperty> volunteer_list = new List<VolunteerProperty>();
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from volunteer_list";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                volunteer_list.Add(new VolunteerProperty
                {
                    VolunteerId = Convert.ToInt32(dr["VolunteerId"]),
                    FirstName = Convert.ToString(dr["Firstname"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    MiddleInitial = Convert.ToString(dr["MiddleInitial"]),
                    Age = Convert.ToString(dr["Age"]),
                    Sex = Convert.ToString(dr["Sex"]),
                    HomeAddress = Convert.ToString(dr["HomeAddress"]),
                    Organization = Convert.ToString(dr["Organization"]),
                    ContactNumber = Convert.ToString(dr["ContactNumber"]),
                    Email = Convert.ToString(dr["Email"]),
                    Password = Convert.ToString(dr["Password"])
                });
            }
            return volunteer_list;
        }

        public bool insertVolunteer(VolunteerProperty volunteer_insert)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "insert into volunteer_list (LastName,FirstName,MiddleInitial,Age,Sex,HomeAddress,Organization,ContactNumber,Email,Password) values ('" + volunteer_insert.LastName + "','" + volunteer_insert.FirstName + "','" + volunteer_insert.MiddleInitial + "','" + volunteer_insert.Age + "','" + volunteer_insert.Sex + "','" + volunteer_insert.HomeAddress + "', '" + volunteer_insert.Organization + "', '" + volunteer_insert.ContactNumber + "', '" + volunteer_insert.Email + "', '" + volunteer_insert.Password + "')";
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


        public bool deleteemp(int id)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "delete from volunteer_list where VolunteerId =" + id;
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

        public List<VolunteerProperty> GetArchive()
        {
            List<VolunteerProperty> volunteer_archive = new List<VolunteerProperty>();
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from archive_list";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                volunteer_archive.Add(new VolunteerProperty
                {
                    VolunteerId = Convert.ToInt32(dr["VolunteerId"]),
                    FirstName = Convert.ToString(dr["Firstname"]),
                    LastName = Convert.ToString(dr["LastName"]),
                    MiddleInitial = Convert.ToString(dr["MiddleInitial"]),
                    Age = Convert.ToString(dr["Age"]),
                    Sex = Convert.ToString(dr["Sex"]),
                    HomeAddress = Convert.ToString(dr["HomeAddress"]),
                    Organization = Convert.ToString(dr["Organization"]),
                    ContactNumber = Convert.ToString(dr["ContactNumber"]),
                    Email = Convert.ToString(dr["Email"]),
                    Password = Convert.ToString(dr["Password"])
                });
            }
            return volunteer_archive;
        }

    }
}