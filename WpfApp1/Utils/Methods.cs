using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.TestDB_1DataSetTableAdapters;

namespace WpfApp1.Utils
{
    class Methods
    {
        public static T GetData<T>(DataRowCollection dataRows, string columnName, T param)
        {
            foreach (DataRow row in dataRows)
            {
                if (row.Field<T>(columnName).Equals(param)) return row.Field<T>(columnName);
            }
            return default;
        }

        public static int GetDataEmployeeId()
        {
            return Methods.GetData<int>(Inits.Users.GetData().Rows, "employee_id", Inits.UserId);
        }

        public static int GetDataJobTitleId()
        {
            return Methods.GetData(Inits.Employees.GetData().Rows, "jobTitle_id", GetDataEmployeeId());
        }


        public static bool IsImportRow(DataRowCollection rows, int index)
        {
            foreach (DataRow item in rows)
            {
                if (item.Field<int>("id") == index) return true;
            }
            return false;
        }
    }
}
