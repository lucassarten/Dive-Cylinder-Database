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
    /// <summary>
    /// Interaction logic for CustomerDetails_Layout.xaml
    /// </summary>
    public partial class CustomerDetails_Layout : Page
    {
        public CustomerDetails_Layout()
        {
            InitializeComponent();
            if (CustomersList_Layout.PUBLIC_ID != "")
            { // open connection
                using (SQLUtils.con))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from Customers where CustomersID = @ID", SQLUtils.con); // get matching ID's
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = CustomersList_Layout.PUBLIC_ID.ToString();
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    TextBoxCustomer_Name.Text = dr["Customer_Name"].ToString(); // set fields 
                    TextBoxCustomer_Address.Text = dr["Customer_Address"].ToString();
                    TextBoxCustomer_Email.Text = dr["Customer_Email"].ToString();
                    TextBoxCustomer_Phone.Text = dr["Customer_Phone"].ToString();
                    dr.Close();
                    con.Close();
                }
            }
            else
            {
                TextBoxCustomer_Address.Clear();
                TextBoxCustomer_Email.Clear();
                TextBoxCustomer_Phone.Clear();
                TextBoxCustomer_Phone.Clear();
            }

        }

        //Buttons for Navigation between layouts
        #region Navigation
        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            CustomersDetails.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            CustomersDetails.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CustomersDetails.Content = new DocumentsList_Layout();
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            CustomersDetails.Content = new CoursesList_Layout();
        }
        #endregion

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCustomer_Name.Text != "")
            {
                if (CustomersList_Layout.PUBLIC_ID == "")
                {
                    try
                    {
                        DataTable dt = new DataTable(); // open connection
                        dt = SQLUtils.queryDT(dt, "SELECT * FROM Customers");
                        dt.Rows.Add(null, TextBoxCustomer_Name.Text, TextBoxCustomer_Email.Text, TextBoxCustomer_Phone.Text, TextBoxCustomer_Address.Text);
                        SQLUtils.Update(dt);
                        dt.Dispose();
                        MessageBox.Show("Record created successfully!", "Notice");
                    }
                    catch
                    {
                        MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
                    }
                }
                else
                {
                    DataTable dt = new DataTable(); // open connection
                    dt = SQLUtils.queryDT(dt, "SELECT * FROM Customers");
                    foreach (DataRow dr in dt.Rows) // update fields
                    {
                        if (dr["CustomersID"].ToString() == CustomersList_Layout.PUBLIC_ID.ToString())
                        {
                            dr["Customer_Name"] = TextBoxCustomer_Name.Text;
                            dr["Customer_Email"] = TextBoxCustomer_Email.Text;
                            dr["Customer_Phone"] = TextBoxCustomer_Phone.Text;
                            dr["Customer_Address"] = TextBoxCustomer_Address.Text;
                        }
                    }
                    dt.Dispose();
                    MessageBox.Show("All changes saved!", "Notice");
                }
            }
            else
            {
                MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
            }
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void CylindersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); // open connection
                dt = SQLUtils.queryDT(dt, "SELECT * FROM Cylinders WHERE Cylinder_FKID_Customers = '" + (CustomersList_Layout.PUBLIC_ID) + "'");
                cylindersDataGrid.ItemsSource = dt.AsDataView();
                dt.Dispose();
            }
            catch
            {
                
            }
        }

        private void CylindersDataGrid_Initialized(object sender, EventArgs e)
        {

        }

        private void ButtonCylinderDetails_Click(object sender, EventArgs e)
        {

        }

        private void Grid_Loaded(object sender, EventArgs e)
        {

        }

        private void CoursesDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCourseDataGrid();
        }

        public void LoadCourseDataGrid()
        {
            try
            {
                DataTable dt = new DataTable(); // open connection
                dt = SQLUtils.queryDT(dt, "SELECT * FROM CoursesRelated WHERE Course_Customer_FKID = '" + (CustomersList_Layout.PUBLIC_ID) + "'");
                coursesDataGrid.ItemsSource = dt.AsDataView();
                dt.Dispose();
            }
            catch
            {}
        }

        private void ButtonAddCourse_Click(object sender, RoutedEventArgs e)
        {
            AddCourseWindow AddCourseWin = new AddCourseWindow();
            AddCourseWin.Closed += SetContentHandler;
            AddCourseWin.Show();
        }

        private void SetContentHandler(object sender, EventArgs e)
        {
            LoadCourseDataGrid();
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Page_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_GotMouseCapture(object sender, MouseEventArgs e)
        {
            
        }

        private void CustomersDetails_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonCylinderDetails_Click(object sender, RoutedEventArgs e)
        {
            object item = cylindersDataGrid.SelectedItem;
            CylindersList_Layout.CYLINDER_PUBLIC_ID = (cylindersDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            CustomersDetails.Content = new CylindersDetails_Layout();
        }

        private void ButtonDiscard_Click(object sender, RoutedEventArgs e)
        {
            CustomersDetails.Content = new CustomersList_Layout();
        }
    }
}
