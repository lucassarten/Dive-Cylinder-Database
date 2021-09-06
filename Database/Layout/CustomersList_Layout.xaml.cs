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
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Collections;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using CG.Web.MegaApiClient;
using System.Globalization;

namespace Database
{
    /// <summary>
    /// Interaction logic for CustomersList_Layout.xaml
    /// </summary>
    public partial class CustomersList_Layout : Page
    {
        CultureInfo MyCultureInfo = new CultureInfo("en-NZ");
        public static DataTable dtoverdue = new DataTable();
        public static string PUBLIC_ID = "";
        public CustomersList_Layout()
        {
            InitializeComponent();
        }

        public void RefreshDatagrid()
        {
            DataTable dt = new DataTable(); // open connection
            dt = SQLUtils.queryDT(dt, "SELECT * FROM Customers");
            customersDataGrid.ItemsSource = dt.AsDataView();
            dt.Dispose();
        }

        //Buttons for Navigation between layouts
        #region Navigation
        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            CustomersList.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CustomersList.Content = new DocumentsList_Layout();
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            CustomersList.Content = new CoursesList_Layout();
        }
        #endregion

        private void CustomersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDatagrid();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_Loaded(object sender, EventArgs e)
        {

        }

        private void CustomersDataGrid_Initialized(object sender, EventArgs e)
        {

        }

        private void CustomersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonDetails_Click(object sender, RoutedEventArgs e)
        {
            object item = customersDataGrid.SelectedItem;
            PUBLIC_ID = (customersDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            CustomersList.Content = new CustomerDetails_Layout();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchname = TextBoxSearch.Text;
            dt = SQLUtils.queryDT(dt, "SELECT * FROM Customers WHERE Customer_Name LIKE '%" + (searchname) + "%'");
            customersDataGrid.ItemsSource = dt.AsDataView();
            dt.Dispose();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            RefreshDatagrid();
            TextBoxSearch.Clear();
        }

        private void ButtonAdd_Record_Click(object sender, RoutedEventArgs e)
        {
            PUBLIC_ID = "";
            CustomersList.Content = new CustomerDetails_Layout();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            object item = customersDataGrid.SelectedItem;
            try
            {
                string ID = (customersDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                SQLUtils.execute("DELETE FROM Customers WHERE CustomersID = '" + (ID) + "'");
                RefreshDatagrid();
            }
            catch
            {}
        }

        // update our database to the cloud provider mega
        private void Backup()
        {
            var options = new Options(applicationKey: "");
            var client = new MegaApiClient(options);
            client.Login("", "");

            IEnumerable<INode> nodes = client.GetNodes();

            INode root = nodes.Single(x => x.Type == NodeType.Root);
            INode myFolder = client.CreateFolder("Backups", root);

            INode myFile = client.UploadFile("photo.jpg", myFolder);
            Uri downloadLink = client.GetDownloadLink(myFile);
            Console.WriteLine(downloadLink);

            client.Logout();
        }

        private void CustomersDataGrid_Overdue_Loaded(object sender, RoutedEventArgs e)
        {
            string TestPeriodDate = DateTime.Now.AddMonths(-6).ToString("dd/MM/yyyy");
            if (TestPeriodDate.StartsWith("0"))
            {
                TestPeriodDate = TestPeriodDate.Substring(1);
            }

            try
            {
                dtoverdue.Columns.Add("DocumentID", typeof(string));
                dtoverdue.Columns.Add("Document_Customer_Name", typeof(string));
                dtoverdue.Columns.Add("Document_Test_Date", typeof(string));
                dtoverdue.Columns.Add("Document_Customer_Email", typeof(string));
                dtoverdue.Columns.Add("Document_Cylinder_Serial_Num", typeof(string));
                dtoverdue.Columns.Add("Document_Cylinder_Manufacturer", typeof(string));
            }
            catch
            {}
            dtoverdue.Clear();
            DataTable dtc = new DataTable();
            dtc = SQLUtils.queryDT(dt, "SELECT * FROM Documents");
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Documents", con);
                SqlDataReader dr = cmd.ExecuteReader();
                try
                {
                    while (dr.Read())
                    {
                        try
                        { // check expired dates
                            string datez = DateTime.Parse(dr["Document_Test_Date"].ToString()).ToShortDateString();
                            if (dr["Document_Test_Date"].ToString().Substring(0, dr["Document_Test_Date"].ToString().IndexOf("/")).Length == 1)
                            {
                                datez = ("0" + DateTime.Parse(dr["Document_Test_Date"].ToString()).ToShortDateString());
                            }
                            if (DateTime.Parse(dr["Document_Test_Date"].ToString()) <= DateTime.Parse(TestPeriodDate) && dr["Document_Notify"].ToString() != "True")
                            {
                                dtoverdue.Rows.Add(dr["DocumentID"].ToString(), dr["Document_Customer_Name"].ToString(), datez, dr["Document_Customer_Email"].ToString(), dr["Document_Cylinder_Serial_Num"].ToString(), dr["Document_Cylinder_Manufacturer"].ToString());
                            }
                        }
                        catch
                        {}
                    }
                }
                catch
                {}
                dr.Close();
            }
            customersDataGrid_Overdue.ItemsSource = dtoverdue.AsDataView();
        }

        private void ButtonSendMail_Click(object sender, RoutedEventArgs e)
        {
            EmailDialogWindow EmailDialogWin = new EmailDialogWindow();
            EmailDialogWin.Show();
        }
    }
}