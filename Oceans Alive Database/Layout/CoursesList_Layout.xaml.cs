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
    /// Interaction logic for CoursesList_Layout.xaml
    /// </summary>
    public partial class CoursesList_Layout : Page
    {
        public CoursesList_Layout()
        {
            InitializeComponent();
        }

        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            CoursesList.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            CoursesList.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CoursesList.Content = new DocumentsList_Layout();
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CoursesDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable(); // open connection
            dt = SQLUtils.queryDT(dt, "SELECT * FROM Courses");
            coursesDataGrid.ItemsSource = dt.AsDataView(); // update view source
            dt.Dispose();
        }

        private void ButtonAddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxAddCourse.Text != "")
            {
                try
                {
                    DataTable dt = new DataTable(); // open connection
                    dt = SQLUtils.queryDT(dt, "SELECT * FROM Courses");
                    dt.Rows.Add(null, TextBoxAddCourse.Text); // get matching rows
                    SQLUtils.update(dt);
                    coursesDataGrid.ItemsSource = dt.AsDataView(); // update view
                    dt.Dispose();
                }
                catch
                {
                    MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
                }
            }
        }
    }
}
