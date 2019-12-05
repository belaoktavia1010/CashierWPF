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
    /// Interaction logic for UserControlItem.xaml
    /// </summary>
    public partial class UserControlItem : UserControl
    {
        int supplierid;
        MyContext myContext = new MyContext();

        

        int lastStock;
        public UserControlItem()
        {
            InitializeComponent();
            DataGridItem.ItemsSource = myContext.Items.ToList();
            CBSupplierName.ItemsSource = myContext.Suppliers.ToList();
        }

        private void genreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            supplierid = Convert.ToInt32(CBSupplierName.SelectedValue.ToString());
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

        private void RefreshItem_Click(object sender, RoutedEventArgs e)
        {
            TxtNameItem.Text = "";
            TxtPriceItem.Text = "";
            TxtStockItem.Text = "";
            CBSupplierName.SelectedItem = -1;
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

        private void TxtStockItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPriceItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtPriceItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtStockItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TxtNameItem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtStockItem_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtNameItem_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtNameItem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
