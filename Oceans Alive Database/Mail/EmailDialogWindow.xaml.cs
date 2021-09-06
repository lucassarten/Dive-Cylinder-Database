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
using System.Net.Mail;

namespace Database
{
    /// <summary>
    /// Interaction logic for EmailDialogWindow.xaml
    /// </summary>
    public partial class EmailDialogWindow : Window
    {
        public EmailDialogWindow()
        {
            InitializeComponent();
        }

        private void CustomersDataGrid_OverdueEmail_Loaded(object sender, RoutedEventArgs e)
        {
            customersDataGrid_OverdueEmail.ItemsSource = CustomersList_Layout.dtoverdue.AsDataView();
        }

        /// <summary>
        /// Sends a batch of emails. Configured for Gmail server.
        /// </summary>
        private async void ButtonSendMail_Click(object sender, RoutedEventArgs e)
        {
            List<string> mailaddress = new List<string>(CustomersList_Layout.dtoverdue.Rows.Count);
            foreach (DataRow row in CustomersList_Layout.dtoverdue.Rows)
                mailaddress.Add((string)row["Document_Customer_Email"]);

            SmtpClient client = new SmtpClient // create client
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = "HOST",
                Credentials = new System.Net.NetworkCredential("EMAIL", "SECURITY_KET")
            };
            for (int i = 0; i < mailaddress.Count; i++) // send mails
                {
                    try
                    {
                        MailMessage mail = new MailMessage("EMAIL", mailaddress[i])
                        {
                            Subject = TextBoxEmailTitle.Text,
                            Body = TextBoxEmailBody.Text
                        };
                        await client.SendMailAsync(mail);
                    }
                    catch
                    {
                        MessageBox.Show("Failed to send message to '" + mailaddress[i] + "'.", "Warning");
                    }
                }
        }
    }
}
