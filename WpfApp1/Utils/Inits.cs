using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.TestDB_1DataSetTableAdapters;

namespace WpfApp1.Utils
{
    public class Inits
    {
        public static int UserId = 0;
        public static string Url = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public static TestDB_1DataSet DataSet = new TestDB_1DataSet();
        public static ClientsTableAdapter Clients = new ClientsTableAdapter();
        public static SuppliersTableAdapter Suppliers = new SuppliersTableAdapter();
        public static JobTitleTableAdapter JobTitle = new JobTitleTableAdapter();
        public static EmployeesTableAdapter Employees = new EmployeesTableAdapter();
        public static StockTableAdapter Stock = new StockTableAdapter();
        public static ReceiptsTableAdapter Receipts = new ReceiptsTableAdapter();
        public static TypeProductTableAdapter TypeProduct = new TypeProductTableAdapter();
        public static GoodsTableAdapter Goods = new GoodsTableAdapter();
        public static PurchasesTableAdapter Purchases = new PurchasesTableAdapter();
        public static UsersTableAdapter Users = new UsersTableAdapter();
    }
}
