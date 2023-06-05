using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SwitchData
{
    public class DAConnection
    {
        public string ImportSwitchData(DataTable dtMRGLdata)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["switchdata"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("SpUploadswitchdata", con);
                    cmd1.CommandTimeout = 1000000;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Connection = con;
                    cmd1.Parameters.AddWithValue("@dtswitchdata", dtMRGLdata);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    return "Successful";
                }
            }

            catch (Exception ex)
            {
                return "Error";
                //Console.WriteLine(ex.Message);
            }
        }
    }
}

