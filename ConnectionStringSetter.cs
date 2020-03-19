using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDatabase
{
    public class ConnectionStringSetter
    {
        Configuration config;
        public ConnectionStringSetter()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public string Connected()
        {   try
                {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = config.ConnectionStrings.ConnectionStrings["ApplicationContext"].ConnectionString;
            if (con.State == System.Data.ConnectionState.Closed)
            {
             
                    con.Open();
                    con.Close();
                    return "Connected";
                }
            
            else if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
                return "Connected";
            }
            }catch(Exception ex)
                {
                    return "Connection Failed\n" + ex;
                }
                
            return "Connection Failed";
        }
        public string saveConnectionString(string value)
        {
            config.ConnectionStrings.ConnectionStrings["ApplicationContext"].ConnectionString = value;
            config.ConnectionStrings.ConnectionStrings["ApplicationContext"].ProviderName = "System.Data.SqlClient";
            config.Save(ConfigurationSaveMode.Modified);
            return Connected();
        }
    }
}
