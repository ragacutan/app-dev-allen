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
    public class JoinImplementation
    {
        // GET: VolunteerImplementation
        public List<JoinProperty> GetJoin()
        {
            List<JoinProperty> join_list = new List<JoinProperty>();
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from volunteer_event";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
               join_list.Add(new JoinProperty
               {
                    Email = Convert.ToString(dr["Email"]),
                    Address = Convert.ToString(dr["Address"]),
                    volCondition = Convert.ToString(dr["volCondition"]),
                    Motivation = Convert.ToString(dr["Motivation"]),
                    Expectation = Convert.ToString(dr["Expectation"]),
                    EmergencyName = Convert.ToString(dr["EmergencyName"]),
                    EmergencyAddress = Convert.ToString(dr["EmergencyAddress"]),
                    EmergencyMobile = Convert.ToString(dr["EmergencyMobile"]),
                    EmergencyRelation = Convert.ToString(dr["EmergencyRelation"]),
               });
            }
            return join_list;
        }

        public bool insertJoin(JoinProperty join_insert)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "insert into volunteer_event (Email,Address,volCondition,Motivation,Expectation,EmergencyName,EmergencyAddress,EmergencyMobile,EmergencyRelation) values ('" + join_insert.Email + "', '" + join_insert.Address + "', '" + join_insert.Motivation + "', '" + join_insert.volCondition + "', '" + join_insert.Expectation + "', '" + join_insert.EmergencyName + "', '" + join_insert.EmergencyAddress + "', '" + join_insert.EmergencyMobile + "', '" + join_insert.EmergencyRelation + "')";
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