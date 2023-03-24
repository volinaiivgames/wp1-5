using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.TestDB_1DataSetTableAdapters;
using System.Linq;
using System.Text.RegularExpressions;
using WpfApp1.TestDB_1DataSetTableAdapters;
using WpfApp1.Utils;
using static WpfApp1.TestDB_1DataSet;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void showPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (showPasswordButton.IsChecked == true)
            {
                Password2.Text = Password1.Password;
                Password2.Visibility = Visibility.Visible;
                Password1.Visibility = Visibility.Hidden;
            }
            else
            {
                Password1.Password = Password2.Text;
                Password2.Visibility = Visibility.Hidden;
                Password1.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = Inits.Users.GetData().Rows;
            string passBox = showPasswordButton.IsChecked == true ? Password2.Text : Password1.Password;
            bool login = false;
            bool password = false;
            if(Login.Text.Length < 5 || Login.Text.Length > 15) NotifyTextBlock.Text = "Логние должен содержать минимум 5 символов и не больше 15 символов";
            else if (passBox.Length < 5 || passBox.Length > 25) NotifyTextBlock.Text = "Пароль должен содержать минимум 5 символов и не больше 25 символов";
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    var user = (UsersRow)data[i];
                    if (user.login.Equals(Login.Text)) login = true; else continue;
                    if (user.password.Equals(passBox)) password = true; else continue;
                    Inits.UserId = user.id;
                }
                if(!login) NotifyTextBlock.Text = "Пользователь с таким логином не найден!";
                else if(!password) NotifyTextBlock.Text = "Вы ввели неправильный пароль!";
                else
                {
                    int jobTitleId = Methods.GetDataJobTitleId();
                    if (jobTitleId.Equals(0) || jobTitleId.Equals(1)) new Window1().Show();
                    else if (jobTitleId.Equals(2)) new Window2().Show();
                    this.Close();
                }
            }
        }

        private void Password1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            NotifyTextBlock.Text = "";
        }
    }
}
