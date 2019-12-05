using CRUDB32.Context;
using CRUDB32.Model;
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

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for UserControlRegister.xaml
    /// </summary>
    public partial class UserControlRegister : UserControl
    {
        int roleid;
        MyContext myContext = new MyContext();
        public UserControlRegister()
        {
            InitializeComponent();
            //DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
            //DataGridItem.ItemsSource = myContext.Items.ToList();
            DataGridUser.ItemsSource = myContext.Users.ToList();
            CBRegister.ItemsSource = myContext.Roles.ToList();
        }

        private void btnSubmitReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var idrole = myContext.Roles.Where(p => p.Id == roleid).FirstOrDefault();

                if ((TxtNameReg.Text == "") || (TxtEmailReg.Text == ""))
                {
                    if (TxtNameReg.Text == "")
                    {
                        MessageBox.Show("Name is requiered", "Caution", MessageBoxButton.OK);
                        TxtNameReg.Focus();
                    }
                    else if (TxtEmailReg.Text == "")
                    {
                        MessageBox.Show("Email is required", "Caution", MessageBoxButton.OK);
                        TxtEmailReg.Focus();
                    }
                    else if (CBRegister.Text == "")
                    {
                        MessageBox.Show("Role is required", "Caution", MessageBoxButton.OK);
                        CBRegister.Focus();
                    }

                }
                else
                {
                    var email = myContext.Users.Where(p => p.Email == TxtEmailReg.Text).FirstOrDefault();
                    //foreach (var email in myContext.Suppliers)
                    //{
                    //    if (email.Email == TxtEmail.Text)
                    //    {
                    //        validEmail = false;
                    //    }
                    //}
                    if (email == null)
                    {

                        //string message = "Halo this message has been sent from wpf. And Your password is ";
                        string pass = Guid.NewGuid().ToString();
                        var push = new User(TxtNameReg.Text, TxtEmailReg.Text, pass, idrole);
                        myContext.Users.Add(push);
                        var result = myContext.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show(result + " row has been inserted");
                        }


                        //Outlook._Application _app = new Outlook.Application();
                        //Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        //mail.Subject = TxtNameReg.Text;
                        //mail.To = TxtEmailReg.Text;
                        //mail.Body = message+ TxtPassword.Text;
                        //mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        //((Outlook._MailItem)mail).Send();
                        //MessageBox.Show("Your Message has been succesfully sent.", "Message", MessageBoxButton.OK);
                    }

                    else
                    {
                        MessageBox.Show("Email has been used");
                    }
                    DataGridUser.ItemsSource = myContext.Users.ToList();

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
            }

            //DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
            //TxtName.Text = "";
            //TxtEmail.Text = "";
            //CBSupplierName.ItemsSource = myContext.Suppliers.ToList();
        }

        private void CBRegister_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roleid = Convert.ToInt32(CBRegister.SelectedValue.ToString());
        }

        private void TxtNameReg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtEmailReg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtEmailReg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
