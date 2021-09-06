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
    /// Interaction logic for CylindersList_Layout.xaml
    /// </summary>
    public partial class CylindersList_Layout : Page
    {
        public static string CYLINDER_PUBLIC_ID = "";
        public CylindersList_Layout()
        {
            InitializeComponent();
        }

        //Buttons for Navigation between layouts
        #region Navigation
        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            CylindersList.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {
            CylindersList.Content = new DocumentsList_Layout();
        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            CylindersList.Content = new CoursesList_Layout();
        }
        #endregion

        private void CylindersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = SQLUtils.queryDT(dt, "SELECT & FROM Cylinders");
            cylindersDataGrid.ItemsSource = dt.AsDataView();
            dt.Dispose();
        }

        private void CylindersDataGrid_Initialized(object sender, EventArgs e)
        {

        }

        private void ButtonCylinderDetails_Click(object sender, EventArgs e)
        {

        }

        private void ButtonCylinderDetails_Click(object sender, RoutedEventArgs e)
        {
            object item = cylindersDataGrid.SelectedItem;
            CYLINDER_PUBLIC_ID = (cylindersDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            CylindersList.Content = new CylindersDetails_Layout();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CYLINDER_PUBLIC_ID = "";
            CylindersList.Content = new CylindersDetails_Layout();
        }
    }
}
