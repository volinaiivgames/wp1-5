using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.TestDB_1DataSetTableAdapters;
using System.Linq;
using WpfApp1.Utils;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace WpfApp1
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            int jobTitleId = Methods.GetDataJobTitleId();
            foreach (DataTable table in Inits.DataSet.Tables)
            {
                string name = table.TableName;
                if (jobTitleId.Equals(0)) ComboBoxer.Items.Add(name);
                else if (jobTitleId.Equals(1) && (name.Equals("Suppliers") || name.Equals("Receipts") || name.Equals("Stock") || name.Equals("TypeProduct") || name.Equals("Goods"))) ComboBoxer.Items.Add(name);
                else if (jobTitleId.Equals(2) && (name.Equals("Purchases") || name.Equals("Clients"))) ComboBoxer.Items.Add(name);
            }
            ComboBoxer.SelectedIndex = 0;
        }

        public void SetData(string type = "", List<DataRow> data = null, string dir = "")
        {
            if (ComboBoxer.SelectedIndex < 0) return;
            string tableName = ComboBoxer.Items[ComboBoxer.SelectedIndex].ToString();
            Page1 page = new Page1(this);

            if (tableName.Equals("Clients"))
            {
                var dataTable = new TestDB_1DataSet.ClientsDataTable();
                if (data != null)
                {
                    foreach (TestDB_1DataSet.ClientsRow row in data) dataTable.ImportRow(row);
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.ClientsDataTable>(File.ReadAllText(dir));

                Inits.Clients.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Clients.GetData();
            }
            else if (tableName.Equals("Employees"))
            {
                var dataTable = new TestDB_1DataSet.EmployeesDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.JobTitle.GetData().Rows, element.Field<int>("jobTitle_id"))) MessageBox.Show("Ошибка в jobTitle_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.EmployeesDataTable>(File.ReadAllText(dir));
                
                Inits.Employees.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Employees.GetData();
            }
            else if (tableName.Equals("Goods"))
            {
                var dataTable = new TestDB_1DataSet.GoodsDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.TypeProduct.GetData().Rows, element.Field<int>("typeProduct_id"))) MessageBox.Show("Ошибка в typeProduct_id возможно нет того что вы указали");
                        else if (!Methods.IsImportRow(Inits.Receipts.GetData().Rows, element.Field<int>("receipt_id"))) MessageBox.Show("Ошибка в receipt_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.GoodsDataTable>(File.ReadAllText(dir));
                
                Inits.Goods.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Goods.GetData();
            }
            else if (tableName.Equals("JobTitle"))
            {
                var dataTable = new TestDB_1DataSet.JobTitleDataTable();
                if (data != null)
                {
                    foreach (TestDB_1DataSet.JobTitleRow row in data) dataTable.ImportRow(row);
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.JobTitleDataTable>(File.ReadAllText(dir));
                
                Inits.JobTitle.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.JobTitle.GetData();
            }
            else if (tableName.Equals("Purchases"))
            {
                var dataTable = new TestDB_1DataSet.PurchasesDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.Employees.GetData().Rows, element.Field<int>("employee_id"))) MessageBox.Show("Ошибка в employee_id возможно нет того что вы указали");
                        else if (!Methods.IsImportRow(Inits.Clients.GetData().Rows, element.Field<int>("client_id"))) MessageBox.Show("Ошибка в client_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.PurchasesDataTable>(File.ReadAllText(dir));
                
                Inits.Purchases.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Purchases.GetData();
            }
            else if (tableName.Equals("Stock"))
            {
                var dataTable = new TestDB_1DataSet.StockDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.Employees.GetData().Rows, element.Field<int>("employee_id"))) MessageBox.Show("Ошибка в employee_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.StockDataTable>(File.ReadAllText(dir));
                
                Inits.Stock.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Stock.GetData();
            }
            else if (tableName.Equals("Suppliers"))
            {
                var dataTable = new TestDB_1DataSet.SuppliersDataTable();
                if (data != null)
                {
                    foreach (TestDB_1DataSet.SuppliersRow row in data) dataTable.ImportRow(row);
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.SuppliersDataTable>(File.ReadAllText(dir));
                
                Inits.Suppliers.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Suppliers.GetData();
            }
            else if (tableName.Equals("TypeProduct"))
            {
                var dataTable = new TestDB_1DataSet.TypeProductDataTable();
                if (data != null)
                {
                    foreach (TestDB_1DataSet.TypeProductRow row in data) dataTable.ImportRow(row);
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.TypeProductDataTable>(File.ReadAllText(dir));
                
                Inits.TypeProduct.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.TypeProduct.GetData();
            }
            else if (tableName.Equals("Receipts"))
            {
                var dataTable = new TestDB_1DataSet.ReceiptsDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.Suppliers.GetData().Rows, element.Field<int>("supplier_id"))) MessageBox.Show("Ошибка в supplier_id возможно нет того что вы указали");
                        else if (!Methods.IsImportRow(Inits.Stock.GetData().Rows, element.Field<int>("stock_id"))) MessageBox.Show("Ошибка в stock_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.ReceiptsDataTable>(File.ReadAllText(dir));
                
                Inits.Receipts.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Receipts.GetData();
            }
            else if (tableName.Equals("Users"))
            {
                var dataTable = new TestDB_1DataSet.UsersDataTable();
                if (data != null)
                {
                    foreach (var element in data)
                    {
                        if (element.RowState == DataRowState.Deleted) dataTable.ImportRow(element);
                        else if (!Methods.IsImportRow(Inits.Employees.GetData().Rows, element.Field<int>("employee_id"))) MessageBox.Show("Ошибка в employee_id возможно нет того что вы указали");
                        else dataTable.ImportRow(element);
                    }
                }
                else if (type.Equals("import")) dataTable = JsonConvert.DeserializeObject<TestDB_1DataSet.UsersDataTable>(File.ReadAllText(dir));
                
                Inits.Users.Update(dataTable);
                page.ColourDataGrid.ItemsSource = Inits.Users.GetData();
            }

            FramePage.Content = page;
            if (type.Equals("export")) page.export(dir);
        }

        private void ComboBoxer_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetData("loadPage");

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
