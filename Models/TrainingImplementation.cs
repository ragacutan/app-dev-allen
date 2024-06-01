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
    public class TrainingImplementation
    {
        public List<TrainingProperty> GetTraining()
        {
            List<TrainingProperty> training_list = new List<TrainingProperty>();
            string mainconn = ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from training_list";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();

            foreach (DataRow dr in dt.Rows)
            {
                training_list.Add(new TrainingProperty
                {
                    TrainingId = Convert.ToInt32(dr["TrainingId"]),
                    TrainingTitle = Convert.ToString(dr["TrainingTitle"]),
                    TrainingDate = Convert.ToString(dr["TrainingDate"]),
                    TrainingLocation = Convert.ToString(dr["TrainingLocation"]),
                    TrainingDescription = Convert.ToString(dr["TrainingDescription"]),
                });
            }
            return training_list;
        }
    }
}