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
    public class EventImplementation
    {
        // GET: VolunteerImplementation
        public List<EventProperty> GetEvent()
        {
            List<EventProperty> event_list = new List<EventProperty>();
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from event_list";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
               event_list.Add(new EventProperty
                {
                    EventId = Convert.ToInt32(dr["EventId"]),
                    EventTitle = Convert.ToString(dr["EventTitle"]),
                    EventPax = Convert.ToString(dr["EventPax"]),
                    EventDate = Convert.ToString(dr["EventDate"]),
                    EventLocation = Convert.ToString(dr["EventLocation"]),
                    EventDescription = Convert.ToString(dr["EventDescription"]),
                });
            }
            return event_list;
        }

        public bool insertEvent(EventProperty event_insert)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "insert into event_list (EventTitle,EventPax,EventDate,EventLocation,EventDescription) values ('" + event_insert.EventTitle + "', '" + event_insert.EventPax + "', '" + event_insert.EventDate + "', '" + event_insert.EventLocation+ "', '" + event_insert.EventDescription + "')";
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

    }
}