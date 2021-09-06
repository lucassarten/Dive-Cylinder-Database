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
using System.IO;
using System.Globalization;

namespace Database
{
    /// <summary>
    /// Interaction logic for CylinderForrm_Layout.xaml
    /// </summary>
    public partial class CylinderForrm_Layout : Page
    {
        public CylinderForrm_Layout()
        {
            InitializeComponent();
            if (DocumentsList_Layout.DOCUMENT_PUBLIC_ID != "")
            {
                dt = SQLUtils.queryDT(dt, "select * from Documents where DocumentID = @ID");
                SqlCommand cmd = new SqlCommand("select * from Documents where DocumentID = @ID", SQLUtils.con);
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = DocumentsList_Layout.DOCUMENT_PUBLIC_ID.ToString();
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read(); // set fields
                TextBoxDocument_Test_Date.Text = DateTime.Parse(dr["Document_Test_Date"].ToString()).ToShortDateString();
                TextBoxDocument_Periodic_Certificate_Num.Text = dr["Document_Periodic_Certificate_Num"].ToString();
                TextBoxDocument_Customer_Name.Text = dr["Document_Customer_Name"].ToString();
                TextBoxDocument_Customer_Email.Text = dr["Document_Customer_Email"].ToString();
                TextBoxDocument_Customer_Phone.Text = dr["Document_Customer_Phone"].ToString();
                TextBoxDocument_Customer_Address.Text = dr["Document_Customer_Address"].ToString();
                TextBoxDocument_Cylinder_Specifications.Text = dr["Document_Cylinder_Specifications"].ToString();
                TextBoxDocument_Cylinder_Material.Text = dr["Document_Cylinder_Material"].ToString();
                TextBoxDocument_Cylinder_Serial_Num.Text = dr["Document_Cylinder_Serial_Num"].ToString();
                TextBoxDocument_Cylinder_LAB_Num.Text = dr["Document_Cylinder_LAB_Num"].ToString();
                TextBoxDocument_Cylinder_Manufacturer.Text = dr["Document_Cylinder_Manufacturer"].ToString();
                TextBoxDocument_Cylinder_Manufacture_Date.Text = dr["Document_Cylinder_Manufacture_Date"].ToString();
                TextBoxDocument_Cylinder_Colour.Text = dr["Document_Cylinder_Colour"].ToString();
                TextBoxDocument_Cylinder_Water_Capacity.Text = dr["Document_Cylinder_Water_Capacity"].ToString();
                TextBoxDocument_Cylinder_Operating_Pressure.Text = dr["Document_Cylinder_Operating_Pressure"].ToString();
                CheckBoxDocument_External_Examination.IsChecked = bool.Parse(dr["Document_External_Examination"].ToString());
                CheckBoxDocument_Hydrostatic_Test.IsChecked = bool.Parse(dr["Document_Hydrostatic_Test"].ToString());
                CheckBoxDocument_ROC_Fitted.IsChecked = bool.Parse(dr["Document_ROC_Fitted"].ToString());
                CheckBoxDocument_Visual_Examination.IsChecked = bool.Parse(dr["Document_Visual_Examination"].ToString());
                TextBoxDocument_ROC_Colour.Text = dr["Document_ROC_Colour"].ToString();
                dr.Close();
            }
            else
            {
                TextBoxDocument_Test_Date.Clear();
                TextBoxDocument_Periodic_Certificate_Num.Clear();
                TextBoxDocument_Customer_Name.Clear();
                TextBoxDocument_Customer_Email.Clear();
                TextBoxDocument_Customer_Phone.Clear();
                TextBoxDocument_Customer_Address.Clear();
                TextBoxDocument_Cylinder_Specifications.Clear();
                TextBoxDocument_Cylinder_Material.Clear();
                TextBoxDocument_Cylinder_Serial_Num.Clear();
                TextBoxDocument_Cylinder_LAB_Num.Clear(); ;
                TextBoxDocument_Cylinder_Manufacturer.Clear();
                TextBoxDocument_Cylinder_Manufacture_Date.Clear();
                TextBoxDocument_Cylinder_Colour.Clear();
                TextBoxDocument_Cylinder_Water_Capacity.Clear();
                TextBoxDocument_Cylinder_Operating_Pressure.Clear();
                TextBoxDocument_ROC_Colour.Clear();
                CheckBoxDocument_External_Examination.IsChecked = false;
                CheckBoxDocument_Hydrostatic_Test.IsChecked = false;
                CheckBoxDocument_ROC_Fitted.IsChecked = false;
                CheckBoxDocument_Visual_Examination.IsChecked = false;
            }
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            CylinderForm.Content = new CoursesList_Layout();
        }

        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            CylinderForm.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            CylinderForm.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CylinderForm.Content = new DocumentsList_Layout();
        }


        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxDocument_Test_Date.Text != "" && TextBoxDocument_Customer_Name.Text != "")
            {
                if (DocumentsList_Layout.DOCUMENT_PUBLIC_ID == "")
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        dt = SQLUtils.queryDT(dt, "select * from Documents");
                        DateTime datet = DateTime.ParseExact(TextBoxDocument_Test_Date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dt.Rows.Add(null, datet.ToShortDateString(), TextBoxDocument_Periodic_Certificate_Num.Text, TextBoxDocument_Customer_Name.Text, TextBoxDocument_Customer_Email.Text, TextBoxDocument_Customer_Phone.Text, TextBoxDocument_Customer_Address.Text, TextBoxDocument_Cylinder_Specifications.Text, TextBoxDocument_Cylinder_Material.Text, TextBoxDocument_Cylinder_Serial_Num.Text, TextBoxDocument_Cylinder_LAB_Num.Text, TextBoxDocument_Cylinder_Manufacturer.Text, TextBoxDocument_Cylinder_Manufacture_Date.Text, TextBoxDocument_Cylinder_Colour.Text, TextBoxDocument_Cylinder_Water_Capacity.Text, TextBoxDocument_Cylinder_Operating_Pressure.Text, CheckBoxDocument_External_Examination.IsChecked, CheckBoxDocument_Visual_Examination.IsChecked, CheckBoxDocument_Hydrostatic_Test.IsChecked, CheckBoxDocument_ROC_Fitted.IsChecked, TextBoxDocument_ROC_Colour.Text);
                        SQLUtils.update(dt);
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
                        dt = SQLUtils.queryDT(dt, "select * from Documents");
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["DocumentID"].ToString() == DocumentsList_Layout.DOCUMENT_PUBLIC_ID)
                            {
                                dr["Document_Test_Date"] = TextBoxDocument_Test_Date.Text;
                                dr["Document_Periodic_Certificate_Num"] = TextBoxDocument_Periodic_Certificate_Num.Text;
                                dr["Document_Customer_Name"] = TextBoxDocument_Customer_Name.Text;
                                dr["Document_Customer_Email"] = TextBoxDocument_Customer_Email.Text;
                                dr["Document_Customer_Phone"] = TextBoxDocument_Customer_Phone.Text;
                                dr["Document_Customer_Address"] = TextBoxDocument_Customer_Address.Text;
                                dr["Document_Cylinder_Specifications"] = TextBoxDocument_Cylinder_Specifications.Text;
                                dr["Document_Cylinder_Material"] = TextBoxDocument_Cylinder_Material.Text;
                                dr["Document_Cylinder_Serial_Num"] = TextBoxDocument_Cylinder_Serial_Num.Text;
                                dr["Document_Cylinder_LAB_Num"] = TextBoxDocument_Cylinder_LAB_Num.Text;
                                dr["Document_Cylinder_Manufacturer"] = TextBoxDocument_Cylinder_Manufacturer.Text;
                                dr["Document_Cylinder_Manufacture_Date"] = TextBoxDocument_Cylinder_Manufacture_Date.Text;
                                dr["Document_Cylinder_Colour"] = TextBoxDocument_Cylinder_Colour.Text;
                                dr["Document_Cylinder_Water_Capacity"] = TextBoxDocument_Cylinder_Water_Capacity.Text;
                                dr["Document_Cylinder_Operating_Pressure"] = TextBoxDocument_Cylinder_Operating_Pressure.Text;
                                dr["Document_External_Examination"] = CheckBoxDocument_External_Examination.IsChecked;
                                dr["Document_Visual_Examination"] = CheckBoxDocument_Visual_Examination.IsChecked;
                                dr["Document_Hydrostatic_Test"] = CheckBoxDocument_Hydrostatic_Test.IsChecked;
                                dr["Document_ROC_Fitted"] = CheckBoxDocument_ROC_Fitted.IsChecked;
                                dr["Document_ROC_Colour"] = TextBoxDocument_ROC_Colour.Text;
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
            CylinderForm.Content = new DocumentsList_Layout();
        }
    }
}
