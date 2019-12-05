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

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        MyContext myContext = new MyContext();

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var email = myContext.Users.Where(p => p.Email == txtUserEmail.Text).FirstOrDefault();



                if ((txtUserEmail.Text == "") || (txtPassword.Password == ""))
                {
                    if (txtUserEmail.Text == "")
                    {
                        MessageBox.Show("Name is requiered", "Caution", MessageBoxButton.OK);
                        txtUserEmail.Focus();
                    }
                    else if (txtPassword.Password == "")
                    {
                        MessageBox.Show("Email is required", "Caution", MessageBoxButton.OK);
                        txtPassword.Focus();
                    }

                }

                else
                {
                    if (email != null)
                    {
                        var pass = email.Password;
                        pass = txtPassword.Password;
                        if (txtPassword.Password == pass)
                        {
                            MainWindow dashboard = new MainWindow();
                            dashboard.Show();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Email and password is not valid");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Email and password is not valid");
                    }
                }



            }
            catch (Exception) { }


        }

        private void txtUserEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z)-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtUserEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
