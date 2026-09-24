using Shoe.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Shoe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SearchTextBox.Text = "Поиск по ФИО...";
            SearchTextBox.Foreground = System.Windows.Media.Brushes.Gray;

            LoadUsers();
        }

        private void LoadUsers(string searchText = "")
        {
            try
            {
                using var db = new shoe_shopContext();
                var query = db.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchText) && searchText != "Поиск по ФИО...")
                {
                    query = query.Where(u => u.FullName.Contains(searchText));
                }

                var users = query.ToList();
                userTable.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}");
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text == "Поиск по ФИО...") return;
            LoadUsers(SearchTextBox.Text);
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "Поиск по ФИО...")
            {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                SearchTextBox.Text = "Поиск по ФИО...";
                SearchTextBox.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
