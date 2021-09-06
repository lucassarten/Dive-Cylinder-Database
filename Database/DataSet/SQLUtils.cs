using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data;

namespace Database
{
    // Functions for interacting with the SQL database.
    partial class SQLUtils
    {
        public SqlConnection;

        public SQLUtils(){
            con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\Database.mdf; Integrated Security = True");
            con.open();
        }

        // query the database and return a DataTable object of the data
        public DataTable queryDT(DataTable dt, String query){
            using (SQLUtils.con)
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Fill(dt);
                adapter.Update(dt);
                adapter.Dispose();
                return dt;
            }
        }

        // update the database with a DataTable object of the data
        public update(DataTable dt){
            using (SQLUtils.con)
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                adapter.Fill(dt);
                adapter.Update(dt);
                adapter.Dispose();
            }
        }

        // execute a query against the database
        public execute(String query){
            using (SQLUtils.con)
            {
                using (SqlCommand command = new SqlCommand(query, SQLUtils.con))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}