using CRUDB32.Context;
using CRUDB32.Model;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CRUDB32
{
    /// <summary>
    /// Interaction logic for UserControlTransaction.xaml
    /// </summary>
    public partial class UserControlTransaction : UserControl
    {
        MyContext myContext = new MyContext();
        int itemid;
        int lasttotal;
        int lastQ;
        int lastStock;
        string report = "ID \t" + "Name\t" + "Price\t" + "Quantity\t" + "Total\n";
        ObservableCollection<List> col;
        List<TransactionItem> TransList = new List<TransactionItem>();
        public UserControlTransaction()
        {
            InitializeComponent();
            TxtOrderDate.Text = DateTimeOffset.Now.LocalDateTime.ToString();
            CBItems.ItemsSource = myContext.Items.ToList();

            col = new ObservableCollection<List>();
            //DataGridTransaction.DataContext = col;

            var addTrans = new Transaction();
            myContext.Transactions.Add(addTrans);
            myContext.SaveChanges();
            TxtIdTrans.Text = Convert.ToString(addTrans.Id);
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
            }


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
                        report += s.Item.Id + "\t" + s.Item.Name + "\t" + s.Item.Price + "\t" + s.Quantity + "\t" + s.Transaction.TotalPrice;
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
                        graphics.DrawString(report, font, PdfBrushes.Black, new System.Drawing.PointF(0, 0));

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

        private void Close()
        {
            throw new NotImplementedException();
        }

       

        private void TxtPay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TxtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPay_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {

        }

        private void CBItems_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            itemid = Convert.ToInt32(CBItems.SelectedValue.ToString());
            var supplier = myContext.Items.Where(p => p.Id == itemid).FirstOrDefault();
            TxtPrice.Text = supplier.Price.ToString();
            TxtStock.Text = supplier.Stock.ToString();
        }

        private void DataGridTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtPay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtStock_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
