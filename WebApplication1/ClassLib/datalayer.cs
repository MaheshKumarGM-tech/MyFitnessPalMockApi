using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.ClassLib
{
    public class datalayer
    {
        public string getfitnesspaldata(string userid)
        {
            string connstr = ConfigurationManager.ConnectionStrings["localdbconnection"].ConnectionString;
            DataTable dt = new DataTable("fitnesspaldata");
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                SqlCommand sqlComm = new SqlCommand("uspMyFitnessPal", conn);
                sqlComm.Parameters.AddWithValue("@userName", userid);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(dt);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

    }
}