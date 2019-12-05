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
using CRUDB32.Context;
using CRUDB32.Model;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Collections.ObjectModel;

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyContext myContext = new MyContext();
        int supplierid;
        int itemid;
        int lasttotal;
        int lastQ;
        int lastStock;
        int roleid;
        string report = "ID \t" + "Name\t" + "Price\t" + "Quantity" + "Total\n";
        ObservableCollection<List> col;
        List<TransactionItem> TransList = new List<TransactionItem>();



        public MainWindow()
        {
            InitializeComponent();
            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
            DataGridItem.ItemsSource = myContext.Items.ToList();
            CBSupplierName.ItemsSource = myContext.Suppliers.ToList();
            CBRegister.ItemsSource = myContext.Roles.ToList();
            TxtOrderDate.Text = DateTimeOffset.Now.LocalDateTime.ToString();
            CBItems.ItemsSource = myContext.Items.ToList();
            DataGridUser.ItemsSource = myContext.Users.ToList();

            col = new ObservableCollection<List>();
            //DataGridTransaction.DataContext = col;

            var addTrans = new Transaction();
            myContext.Transactions.Add(addTrans);
            myContext.SaveChanges();
            TxtIdTrans.Text = Convert.ToString(addTrans.Id);



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
            CBSupplierName.ItemsSource = myContext.Suppliers.ToList();

        }


        #region

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void TxtEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z)-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var num = Convert.ToInt32(TxtId.Text);
            var uNum = myContext.Suppliers.Where(p => p.Id == num).FirstOrDefault();
            uNum.Name = TxtName.Text;
            uNum.Email = TxtEmail.Text;
            myContext.SaveChanges();
            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
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

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

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

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {

            var data = DataGridItem.SelectedItem;
            string Id = (DataGridItem.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtIdItem.Text = Id;
            string name = (DataGridItem.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtNameItem.Text = name;
            string stock = (DataGridItem.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            TxtStockItem.Text = stock;
            string price = (DataGridItem.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            TxtPriceItem.Text = price;
            string namesupplier = (DataGridItem.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
            CBSupplierName.Text = namesupplier;

        }

        private void genreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            supplierid = Convert.ToInt32(CBSupplierName.SelectedValue.ToString());
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string messageBoxText = "Are you sure you want to delete the selected Items?";
                string caption = "Confirm";
                MessageBoxButton button = MessageBoxButton.YesNo;
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.Yes)
                {
                    var num = Convert.ToInt32(TxtIdItem.Text);
                    var uNum = myContext.Items.Where(p => p.Id == num).FirstOrDefault();
                    myContext.Items.Remove(uNum);
                    myContext.SaveChanges();
                    DataGridItem.ItemsSource = myContext.Items.ToList();

                }
            }
            catch (Exception)
            {
            }
        }

        private void UpdateItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var num = Convert.ToInt32(TxtIdItem.Text);
                var uNum = myContext.Items.Where(p => p.Id == num).FirstOrDefault();
                uNum.Name = TxtNameItem.Text;
                uNum.Stock = Convert.ToInt32(TxtStockItem.Text);
                uNum.Price = Convert.ToInt32(TxtPriceItem.Text);
                myContext.SaveChanges();

                MessageBox.Show("1 row has been updated");

                DataGridItem.ItemsSource = myContext.Items.ToList();
            }
            catch (Exception)
            {
            }

        }

        private void InsertItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if ((TxtNameItem.Text == "") || (TxtPriceItem.Text == ""))
                {
                    if (TxtNameItem.Text == "")
                    {
                        MessageBox.Show("Name is requiered", "Caution", MessageBoxButton.OK);
                        TxtNameItem.Focus();
                    }
                    else if (TxtPriceItem.Text == "")
                    {
                        MessageBox.Show("Price is required", "Caution", MessageBoxButton.OK);
                        TxtPriceItem.Focus();
                    }
                    else if (TxtStockItem.Text == "")
                    {
                        MessageBox.Show("Stock is required", "Caution", MessageBoxButton.OK);
                        TxtStockItem.Focus();
                    }

                }
                else
                {
                    int price = Convert.ToInt32(TxtPriceItem.Text);
                    int stock = Convert.ToInt32(TxtStockItem.Text);

                    var supplier = myContext.Suppliers.Where(p => p.Id == supplierid).FirstOrDefault();
                    var item = myContext.Items.Where(p => p.Name == TxtNameItem.Text).FirstOrDefault();


                    if (TxtNameItem.Text != "")
                    {
                        if (item != null)
                        {
                            var lastQty = item.Stock;
                            var lastPrice = item.Price;
                            if (TxtPriceItem.Text == lastPrice.ToString())
                            {
                                lastStock = stock + lastQty;
                                item.Stock = Convert.ToInt32(lastStock);


                                var result2 = myContext.SaveChanges();
                                if (result2 > 0)
                                {
                                    MessageBox.Show("Stock has been inserted");
                                }
                                else
                                {
                                    MessageBox.Show("Stok Cant be Updated");
                                }
                            }
                            else
                            {
                                var pushItem = new Items(TxtNameItem.Text, stock, price, supplier);
                                myContext.Items.Add(pushItem);
                                var result = myContext.SaveChanges();
                                if (result > 0)
                                {
                                    MessageBox.Show("1 row has been inserted");
                                }
                            }


                            DataGridItem.ItemsSource = myContext.Items.ToList();
                        }
                        else
                        {
                            var pushItem = new Items(TxtNameItem.Text, stock, price, supplier);
                            myContext.Items.Add(pushItem);
                            var result = myContext.SaveChanges();
                            if (result > 0)
                            {
                                MessageBox.Show("1 row has been inserted");
                            }
                        }
                    }
                    DataGridItem.ItemsSource = myContext.Items.ToList();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
            }

            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();

            CBItems.ItemsSource = myContext.Items.ToList();


        }

        private void TxtStockItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RefreshItem_Click(object sender, RoutedEventArgs e)
        {
            TxtNameItem.Text = "";
            TxtPriceItem.Text = "";
            TxtStockItem.Text = "";
            CBSupplierName.SelectedItem = -1;
        }

        private void TxtNameItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtNameItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TxtPriceItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtPriceItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtStockItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            e.Handled = regex.IsMatch(e.Text);
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void CBItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemid = Convert.ToInt32(CBItems.SelectedValue.ToString());
            var data = DataGridItem.SelectedItem;
            var supplier = myContext.Items.Where(p => p.Id == itemid).FirstOrDefault();
            TxtPrice.Text = supplier.Price.ToString();
            TxtStock.Text = supplier.Stock.ToString();


        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int transid = Convert.ToInt32(TxtIdTrans.Text);
            if (TxtQuantity.Text == "")
            {
                MessageBox.Show("Quantity is empty");
            }
            else
            {
                itemid = Convert.ToInt32(CBItems.SelectedValue.ToString());
                var itemname = myContext.Items.Where(w => w.Id == itemid).FirstOrDefault();
                var transitem = myContext.Transactions.Where(w => w.Id == transid).FirstOrDefault();

                int q = Convert.ToInt32(TxtQuantity.Text);
                int s = Convert.ToInt32(TxtStock.Text);
                if (q != 0)
                {
                    if (s >= q)
                    {

                        int price = Convert.ToInt32(TxtPrice.Text);
                        int quantity = Convert.ToInt32(TxtQuantity.Text);
                        int tot = price * quantity;
                        int tempStock = s - quantity;

                        int stock = itemname.Stock;
                        var stock2 = s - stock;

                        int stockminus = stock2;
                        TxtTotal1.Text = TotalPrice.ToString();

                        lasttotal += tot;

                        TransList.Add(new TransactionItem { Quantity = quantity, Transaction = transitem, Item = itemname });

                        DataGridTransaction.Items.Add(new { Name = CBItems.Text, Quantity = TxtQuantity.Text, Price = TxtPrice.Text, TotalPrice = tot.ToString() });

                        TxtTotal1.Text = lasttotal.ToString();
                        TxtTotal2.Text = lasttotal.ToString();
                        TxtStock.Text = tempStock.ToString();

                        itemname.Stock = tempStock;
                        myContext.SaveChanges();

                        DataGridItem.ItemsSource = myContext.Items.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Quantity");
                    }

                }
                else
                {
                    MessageBox.Show("Quantity is Null");
                }

            }

        }

        private void BtnSubmitTrans_Click(object sender, RoutedEventArgs e)
        {
            if (TxtPay.Text == "")
            {
                MessageBox.Show("Pay is Empty");
            }
            else
            {

                int pay = Convert.ToInt32(TxtPay.Text);
                int change = pay - lasttotal;
                int id = Convert.ToInt32(TxtIdTrans.Text);
                var tid = myContext.Transactions.Where(t => t.Id == id).FirstOrDefault();
                int totalprice = Convert.ToInt32(TxtTotal2.Text);

                if (lasttotal < pay)
                {
                    foreach (var s in TransList)
                    {
                        myContext.TransactionItems.Add(s);
                        tid.TotalPrice = totalprice;
                        myContext.SaveChanges();
                        report+= s.Item.Id + "\t" + s.Item.Name + "\t"+ s.Item.Price + "\t"+ s.Quantity + "\t" + s.Transaction.TotalPrice;
                    }
                    MessageBox.Show("Your Change : " + change.ToString());
                    TxtChange.Text = change.ToString();


                    using (PdfDocument document = new PdfDocument())
                    {
                        //Add a page to the document
                        PdfPage page = document.Pages.Add();

                        //Create PDF graphics for the page
                        PdfGraphics graphics = page.Graphics;

                        //Set the standard font
                        PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                        //Draw the text
                        graphics.DrawString(report, font, PdfBrushes.Black, new PointF(0, 0));

                        //Save the document
                        document.Save("Output.pdf");

                        #region View the Workbook
                        //Message box confirmation to view the created document.
                        if (MessageBox.Show("Do you want to view the PDF?", "PDF has been created",
                            MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            try
                            {
                                //Launching the Excel file using the default Application.[MS Excel Or Free ExcelViewer]
                                System.Diagnostics.Process.Start("Output.pdf");

                                //Exit
                                Close();
                            }
                            catch (Win32Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        else
                            Close();
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("Your Payment Is Not Valid");
                }
            }


        }

        private void TxtPay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtPay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            var data = DataGridTransaction.SelectedItem;
            string itemcart = (DataGridTransaction.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            string Qcart = (DataGridTransaction.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
            string totalcart = (DataGridTransaction.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
            int total = Convert.ToInt32(TxtPrice.Text);

            if (DataGridTransaction.SelectedItem != null)
            {
                int maxstock = Convert.ToInt32(TxtStock.Text);

                int stockcart = Convert.ToInt32(Qcart);
                int pricecart = Convert.ToInt32(totalcart);

                var item = myContext.Items.Where(i => i.Name == itemcart).FirstOrDefault();
                int stocknow = item.Stock;
                int realstock = Convert.ToInt32(Qcart) + stocknow;
                //int realtotal = total - pricecart;

                item.Stock = realstock;
                myContext.SaveChanges();

                TxtStock.Text = realstock.ToString();
                //TxtTotal1.Text = realtotal.ToString();
                DataGridTransaction.Items.RemoveAt(DataGridTransaction.SelectedIndex);
                DataGridItem.ItemsSource = myContext.Items.ToList();
            }


        }




        private void DataGrid_SelectionChanged_4(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtTotal2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtStock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddTrans_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CBRegister_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            roleid = Convert.ToInt32(CBRegister.SelectedValue.ToString());
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
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK);
            }

            DataGridSupplier.ItemsSource = myContext.Suppliers.ToList();
            TxtName.Text = "";
            TxtEmail.Text = "";
            CBSupplierName.ItemsSource = myContext.Suppliers.ToList();
        }

        private void TxtEmailReg_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^A-Za-z)-9.@]+$");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtEmailReg_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            TxtNameReg.Text = "";
            TxtEmailReg.Text = "";
        }

        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = DataGridUser.SelectedItem;
            string Id = (DataGridUser.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
            TxtNameReg.Text = Id;
            string name = (DataGridUser.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
            TxtEmailReg.Text = name;
        }

        private void TxtIdTrans_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
