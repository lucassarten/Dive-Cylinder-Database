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
    /// Interaction logic for AddCourseWindow.xaml
    /// </summary>
    public partial class AddCourseWindow : Window
    {
        public AddCourseWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = SQLUtils.queryDT(dt, "SELECT * FROM CoursesRelated");
                dt.Rows.Add(null, ListBoxCourses.SelectedItem, CustomersList_Layout.PUBLIC_ID); // add the new row
                SQLUtils.update(dt);
                dt.Dispose();
                MessageBox.Show("Course added successfully!", "Notice");
                this.Close();
            }
            catch
            {
                MessageBox.Show("An error has occurred. Please check all fields and try again.", "Error");
            }
        }

        private void ListBoxCourses_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = SQLUtils.queryDT(dt, "SELECT * FROM Courses");
                for (int i = 0; i <= dt.Rows.Count; i++) // add to the display list
                {
                    ListBoxCourses.Items.Add(dt.Rows[i][1].ToString());
                }
                dt.Dispose();
            }
            catch
            {

            }
        }
    }
}
