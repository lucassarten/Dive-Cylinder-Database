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
    /// Interaction logic for CylindersDetails_Layout.xaml
    /// </summary>
    public partial class CylindersDetails_Layout : Page
    {
        public CylindersDetails_Layout()
        {
            InitializeComponent();
            if (CylindersList_Layout.CYLINDER_PUBLIC_ID != "")
            {
                SqlCommand cmd = new SqlCommand("select * from Cylinders where CylinderID = @ID", SQLUtils.con);
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = CylindersList_Layout.CYLINDER_PUBLIC_ID.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read(); // set fields
                TextBoxCylinder_LABNum.Text = dr["Cylinder_LAB_Num"].ToString();
                TextBoxCylinder_Specifications.Text = dr["Cylinder_Specifications"].ToString();
                TextBoxCylinder_Material.Text = dr["Cylinder_Material"].ToString();
                TextBoxCylinder_SerialNumber.Text = dr["Cylinder_Serial_Num"].ToString();
                TextBoxCylinder_Manufacturer.Text = dr["Cylinder_Manufacturer"].ToString();
                TextBoxCylinder_Manufacture_Date.Text = dr["Cylinder_Manufacture_Date"].ToString();
                TextBoxCylinder_Colour.Text = dr["Cylinder_Colour"].ToString();
                TextBoxCylinder_Water_Capacity.Text = dr["Cylinder_Water_Capacity"].ToString();
                dr.Close();
            }
            else
            {
                TextBoxCylinder_LABNum.Clear();
                TextBoxCylinder_Specifications.Clear();
                TextBoxCylinder_Material.Clear();
                TextBoxCylinder_SerialNumber.Clear();
                TextBoxCylinder_Manufacturer.Clear();
                TextBoxCylinder_Manufacture_Date.Clear();
                TextBoxCylinder_Colour.Clear();
                TextBoxCylinder_Water_Capacity.Clear();
            }
        }

        //Buttons for Navigation between layouts
        #region Navigation
        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            CylindersDetails.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            CylindersDetails.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CylindersDetails.Content = new DocumentsList_Layout();
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            CylindersDetails.Content = new CoursesList_Layout();
        }
        #endregion

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCylinder_SerialNumber.Text != "" && TextBoxCylinder_Manufacture_Date.Text != "" && TextBoxCylinder_LABNum.Text != "")
            {
                if (CylindersList_Layout.CYLINDER_PUBLIC_ID == "")
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt = SQLUtils.queryDT(dt, "SELECT * FROM Cylinders");
                        dt.Rows.Add(null, null, TextBoxCylinder_Specifications.Text, TextBoxCylinder_Material.Text, TextBoxCylinder_SerialNumber.Text, TextBoxCylinder_LABNum.Text, TextBoxCylinder_Manufacturer.Text, TextBoxCylinder_Manufacture_Date.Text, TextBoxCylinder_Colour.Text, TextBoxCylinder_Water_Capacity.Text);
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
                    try
                    {
                        DataTable dt = new DataTable();
                        dt = SQLUtils.queryDT(dt, "SELECT * FROM Cylinders");
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["CylinderID"].ToString() == CylindersList_Layout.CYLINDER_PUBLIC_ID)
                            {
                                dr["Cylinder_Specifications"] = TextBoxCylinder_Specifications.Text;
                                dr["Cylinder_Material"] = TextBoxCylinder_Material.Text;
                                dr["Cylinder_Serial_Num"] = TextBoxCylinder_SerialNumber.Text;
                                dr["Cylinder_Manufacturer"] = TextBoxCylinder_Manufacturer.Text;
                                dr["Cylinder_Manufacture_Date"] = TextBoxCylinder_Manufacture_Date.Text;
                                dr["Cylinder_Colour"] = TextBoxCylinder_Colour.Text;
                                dr["Cylinder_Water_Capacity"] = TextBoxCylinder_Water_Capacity.Text;
                            }
                        }
                        SQLUtils.Update(dt);
                        dt.Dispose();
                        MessageBox.Show("All changes saved!", "Notice");
                    }
                    catch
                    {
                        MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
            }
        }

        private void ButtonDiscard_Click(object sender, RoutedEventArgs e)
        {
            CylindersDetails.Content = new CylindersList_Layout();
        }
    }
}
