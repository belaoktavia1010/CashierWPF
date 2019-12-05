using CRUDB32.Context;
using CRUDB32.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for UserControlSupplier.xaml
    /// </summary>
    public partial class UserControlSupplier : UserControl
    {
        MyContext myContext = new MyContext();

        public UserControlSupplier()
        {
            InitializeComponent();
            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if ((TxtName.Text == "") || (TxtEmail.Text == ""))
                {
                    if (TxtName.Text == "")
                    {
                        MessageBox.Show("Name is requiered", "Caution", MessageBoxButton.OK);
                        TxtName.Focus();
                    }
                    else if (TxtEmail.Text == "")
                    {
                        MessageBox.Show("Email is required", "Caution", MessageBoxButton.OK);
                        TxtEmail.Focus();
                    }

                }
                else
                {
                    var email = myContext.Suppliers.Where(p => p.Email == TxtEmail.Text).FirstOrDefault();
                    //foreach (var email in myContext.Suppliers)
                    //{
                    //    if (email.Email == TxtEmail.Text)
                    //    {
                    //        validEmail = false;
                    //    }
                    //}
                    if (email == null)
                    {

                        string message = "Halo this message has been sent from wpf";
                        var push = new Supplier(TxtName.Text, TxtEmail.Text);
                        myContext.Suppliers.Add(push);
                        var result = myContext.SaveChanges();
                        if (result > 0)
                        {
                            MessageBox.Show(result + " row has been inserted");
                        }


                        Outlook._Application _app = new Outlook.Application();
                        Outlook.MailItem mail = (Outlook.MailItem)_app.CreateItem(Outlook.OlItemType.olMailItem);
                        mail.Subject = TxtName.Text;
                        mail.To = TxtEmail.Text;
                        mail.Body = message;
                        mail.Importance = Outlook.OlImportance.olImportanceNormal;
                        ((Outlook._MailItem)mail).Send();
                        MessageBox.Show("Your Message has been succesfully sent.", "Message", MessageBoxButton.OK);
                    }

                    else
                    {
                        MessageBox.Show("Email has been used");
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
            }

            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
            TxtName.Text = "";
            TxtEmail.Text = "";
        }

        private void DataGridSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = DataGridSupplier.SelectedItem;
            string Id = (DataGridSupplier.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtId.Text = Id;
            string name = (DataGridSupplier.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtName.Text = name;
            string email = (DataGridSupplier.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            TxtEmail.Text = email;
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z)-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void BtnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string messageBoxText = "Are you sure you want to delete the selected Orders?";
                string caption = "Confirm";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    var num = Convert.ToInt32(TxtId.Text);
                    var uNum = myContext.Suppliers.Where(p => p.Id == num).FirstOrDefault();
                    myContext.Suppliers.Remove(uNum);
                    myContext.SaveChanges();
                    DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();

                    TxtName.Text = "";
                    TxtEmail.Text = "";
                }

            }
            catch (Exception)
            {
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt32(TxtId.Text);
            var uNum = myContext.Suppliers.Where(p => p.Id == num).FirstOrDefault();
            uNum.Name = TxtName.Text;
            uNum.Email = TxtEmail.Text;
            myContext.SaveChanges();
            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
        }
    }
}
