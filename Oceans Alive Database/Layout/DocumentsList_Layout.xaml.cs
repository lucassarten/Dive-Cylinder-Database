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
using System.Globalization;

namespace Database
{
    /// <summary>
    /// Interaction logic for DocumentsList_Layout.xaml
    /// </summary>
    public partial class DocumentsList_Layout : Page
    {
        public static string DOCUMENT_PUBLIC_ID = "";
        public DocumentsList_Layout()
        {
            InitializeComponent();
        }

        private void Button_Cylinders_Click(object sender, RoutedEventArgs e)
        {
            DocumentsList.Content = new CylindersList_Layout();
        }

        private void Button_Customers_Click(object sender, RoutedEventArgs e)
        {
            DocumentsList.Content = new CustomersList_Layout();
        }

        private void Button_Documents_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Courses_Click(object sender, RoutedEventArgs e)
        {
            DocumentsList.Content = new CoursesList_Layout();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DocumentsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt = SQLUtils.queryDT(dt, "SELECT & FROM Documents");
            documentsDataGrid.ItemsSource = dt.AsDataView();
            dt.Dispose();
        }           

        private void ButtonDocumentDetails_Click(object sender, RoutedEventArgs e)
        {
            object item = documentsDataGrid.SelectedItem;
            DOCUMENT_PUBLIC_ID = (documentsDataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            DocumentsList.Content = new CylinderForrm_Layout();
        }

        private void ButtonAddDocuent_Click(object sender, RoutedEventArgs e)
        {
            DOCUMENT_PUBLIC_ID = "";
            DocumentsList.Content = new CylinderForrm_Layout();
        }
    }
}
