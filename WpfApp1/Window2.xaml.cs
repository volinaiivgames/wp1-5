using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WpfApp1.Utils;
using static WpfApp1.TestDB_1DataSet;
using WpfApp1.TestDB_1DataSetTableAdapters;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public static List<PurchasesRow> BasketGoodsRow = new List<PurchasesRow>();
        public Window2()
        {
            InitializeComponent();
            foreach (var item in Inits.Goods.GetData()) GoodsComboBox.Items.Add(item.name);
            foreach (var item in Inits.Clients.GetData()) ClientsComboBox.Items.Add(item.name);
            PurchasesDataGrid.ItemsSource = Inits.Purchases.GetData();
        }

        private void addClient(object sender, RoutedEventArgs e) => new Window1().Show();

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void GoodsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => GoodsComboBox_SelectionChanged();

        public void GoodsComboBox_SelectionChanged()
        {
            var selectedItemId = GoodsComboBox.SelectedIndex;
            if (selectedItemId >= 0)
            {
                var item = Inits.Goods.GetData()[selectedItemId];
                if (item != null)
                {
                    var table = new GoodsDataTable();
                    table.ImportRow(item);
                    GoodsDataGrid.ItemsSource = table;
                    GoodsComboBox.Items.Refresh();
                }
            }
        }

        private void SetLastPriceTextBlock()
        {
            float price = 0;
            foreach (var item in BasketGoodsRow)
            {
                var itemGoods = Inits.Goods.GetData().SingleOrDefault(g => g.id == item.id);
                if (itemGoods != null) price += itemGoods.price * Convert.ToInt32(item.count);
            }
            LastPriceTextBlock.Text = $"{price} руб";
        }

        private void UpdateBasketDataGrid()
        {
            BasketDataGrid.ItemsSource = BasketGoodsRow;
            for (int i = BasketDataGrid.Columns.Count - 1; i >= 0; i--)
            {
                var column = BasketDataGrid.Columns[i];
                if (column.Header.ToString() != "goods" && column.Header.ToString() != "count")
                {
                    BasketDataGrid.Columns.RemoveAt(i);
                }
            }
            BasketDataGrid.Items.Refresh();
            SetLastPriceTextBlock();
        }

        private void ClearBasket()
        {
            if (BasketGoodsRow.Count == 0) return;
            BasketGoodsRow = new List<PurchasesRow>();
            UpdateBasketDataGrid();
        }
        private void UploadCheck(int index)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                var element = (DataRowView)PurchasesDataGrid.Items[index];
                var item = (PurchasesRow)element.Row;
                var goods = JsonConvert.DeserializeObject<List<int>>(item.goods);
                var count = JsonConvert.DeserializeObject<List<int>>(item.count);
                string dir = $"{dialog.FileName}\\Чек№{item.id}.txt";
                using (StreamWriter sw = File.CreateText(dir))
                {
                    sw.WriteLine("	   Магазин оружий");
                    sw.WriteLine($"	   Кассовый чек №{item.id}");
                    sw.WriteLine("");
                    for (int i = 0; i < goods.Count; i++)
                    {
                        var goods_ = goods[i];
                        var count_ = count[i];
                        var itemGoods = Inits.Goods.GetData().SingleOrDefault(g => g.id == goods_);
                        sw.WriteLine($"	{itemGoods.name}	-	{count_}");
                    }
                    sw.WriteLine("");
                    sw.WriteLine($"Итого к оплате: {item.total}");
                    sw.WriteLine($"Внесено: {item.contributed}");
                    sw.WriteLine($"Сдача: {item.change}");
                }
            }
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            int countLast = Convert.ToInt32(CountTextBox.Text);
            if (GoodsComboBox.SelectedIndex < 0) MessageBox.Show("Нужно выбрать продукт");
            else if (ClientsComboBox.SelectedIndex < 0) MessageBox.Show("Нужно выбрать клиента");
            else if (countLast <= 0) MessageBox.Show("Введите количество товара");
            else
            {
                var goodsDataGrid = (DataRowView)GoodsDataGrid.Items[0];
                var goodsItem = (GoodsRow)goodsDataGrid.Row;
                var client = Inits.Clients.GetData()[ClientsComboBox.SelectedIndex];
                if (goodsDataGrid == null || client == null) return;
                if (countLast > goodsItem.count) MessageBox.Show("Вы указали количество товара больше чем есть на складе");
                else
                {
                    var newRow = Inits.Purchases.GetData().NewPurchasesRow();
                    var BasketItem = BasketGoodsRow.SingleOrDefault(g => g.id == goodsItem.id);
                    if (BasketItem == null)
                    {
                        newRow.id = goodsItem.id;
                        newRow.goods = goodsItem.name;
                        newRow.count = countLast.ToString();
                        BasketGoodsRow.Add(newRow);
                    }
                    else
                    {
                        if (Convert.ToInt32(BasketItem.count) + countLast > goodsItem.count)
                        {
                            MessageBox.Show($"У вас в корзине есть этот товар в количестве ({BasketItem.count} шт) и вы хотите добавить ещё ({countLast} шт) на складе меньше чем вы хотите добавить в корзину");
                            return;
                        }
                        else BasketItem.count = (Convert.ToInt32(BasketItem.count) + countLast).ToString();
                    }
                    UpdateBasketDataGrid();
                }
            }
        }

        private void DleteBasketItem_Click(object sender, RoutedEventArgs e)
        {
            if (BasketDataGrid.SelectedIndex == -1) return;
            BasketGoodsRow.RemoveAt(BasketDataGrid.SelectedIndex);
            UpdateBasketDataGrid();
        }

        private void ClearBasket_Click(object sender, RoutedEventArgs e) => ClearBasket();

        private void SellItems_Click(object sender, RoutedEventArgs e)
        {
            var purchasesDataTable = new PurchasesDataTable();
            List<int> goodsIdList = new List<int>();
            List<int> goodsCountList = new List<int>();
            float contributed = JsonConvert.DeserializeObject<float>(contributedTextBox.Text);
            float total = 0;
            foreach (var item in BasketGoodsRow)
            {
                goodsIdList.Add(item.id);
                goodsCountList.Add(Convert.ToInt32(item.count));
                
                var itemGoods = Inits.Goods.GetData().SingleOrDefault(g => g.id == item.id);
                total += itemGoods.price * Convert.ToInt32(item.count);
            }
            if (contributed < total) MessageBox.Show("Товар стоит больше\nукажите правильную сумму");
            else
            {
                foreach (var item in BasketGoodsRow)
                {
                    var itemGoods = Inits.Goods.GetData().SingleOrDefault(g => g.id == item.id);
                    Inits.Goods.UpdateQuery(itemGoods.count - Convert.ToInt32(item.count), item.id);
                }
                float change = contributed - total;
                Inits.Purchases.Insert(Methods.GetDataEmployeeId(), JsonConvert.SerializeObject(goodsIdList), Inits.Clients.GetData()[ClientsComboBox.SelectedIndex].id, JsonConvert.SerializeObject(goodsCountList), total, contributed, change, DateTime.Now);
                Inits.Purchases.Update(purchasesDataTable);

                PurchasesDataGrid.ItemsSource = Inits.Purchases.GetData();
                PurchasesDataGrid.Items.Refresh();
                GoodsComboBox_SelectionChanged();
                ClearBasket();
                PurchasesDataGrid.SelectedIndex = Inits.Purchases.GetData().Count - 1;
                //UploadCheck(Inits.Purchases.GetData().Count - 1);
            }
        }

        private void UploadCheck_Click(object sender, RoutedEventArgs e)
        {
            if (PurchasesDataGrid.SelectedIndex < 0) return;
            UploadCheck(PurchasesDataGrid.SelectedIndex);
        }
    }
}
