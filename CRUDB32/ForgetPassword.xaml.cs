using CRUDB32.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for ForgetPassword.xaml
    /// </summary>
    public partial class ForgetPassword : Window
    {
        MyContext myContext = new MyContext();
        public ForgetPassword()
        {
            InitializeComponent();
        }

        

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var checkemail = myContext.Users.FirstOrDefault(v => v.Email == TxtForgetPass.Text);
                if(checkemail != null)
                {
                    var email = checkemail.Email;
                    if (TxtForgetPass.Text == email)
                    {
                        string newpass = Guid.NewGuid().ToString();
                        var emailcheck = myContext.Users.Where(s => s.Email == TxtForgetPass.Text).FirstOrDefault();
                        myContext.SaveChanges();
                        MessageBox.Show("Password has been sent");
                        Outlook._Application _app = new Outlook.Application();
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        mail.To = TxtForgetPass.Text;
                        mail.Body = "Hi" + TxtForgetPass + "\nThis Is Your New Password : " + newpass;
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();
                        MessageBox.Show("Check Your Email for Your New Password", "Message", MessageBoxButton.OK);

                    }
                }
                else
                {
                    MessageBox.Show("Your email is not registered");
                }
                

                
            }

            catch (Exception)
            {

            }
        }


        private void TxtForgetPass_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z)-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtUserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
