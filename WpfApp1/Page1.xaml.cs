using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using WpfApp1.Utils;
using System.IO;
using Newtonsoft.Json;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Input;
using static WpfApp1.TestDB_1DataSet;

namespace WpfApp1
{
    public partial class Page1 : Page
    {
        private Window1 window;
        public Page1(Window1 window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void save_Click(object sender, RoutedEventArgs e) => window.SetData("save", ((IEnumerable<DataRow>)ColourDataGrid.ItemsSource).ToList());

        private void import_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok && Path.GetExtension(dialog.FileName) == ".json")
                window.SetData("import", null, Path.GetFullPath(dialog.FileName));
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog() { IsFolderPicker = true };
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
                window.SetData("export", null, Path.GetFullPath($"{dialog.FileName}\\{window.ComboBoxer.Items[window.ComboBoxer.SelectedIndex].ToString()}.json"));
        }

        public void export(string dir)
        {
            using (StreamWriter sw = File.CreateText(dir)) sw.WriteLine(JsonConvert.SerializeObject(ColourDataGrid.ItemsSource));
        }
    }
}
