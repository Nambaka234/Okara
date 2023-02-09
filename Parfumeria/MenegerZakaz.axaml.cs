using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Parfumeria.database;
using System.Collections.Generic;
using System.Linq;

namespace Parfumeria
{
    public partial class MenegerZakaz : Window
    {
        bool isAdmin = false;
        public MenegerZakaz()
        {
            InitializeComponent();
            InitializeWindow();
            DataContext = isAdmin;
        }

        public void InitializeWindow()
        {
            SortComboBox.SelectionChanged += SortComboBox_SelectionChanged;
            FilterComboBox.SelectionChanged += FilterComboBox_LostFocus;
            AddZakazButton.Click += AddZakazButton_Click;
            BackButton.Click += BackButton_Click;
            SearchTextBox.AddHandler(KeyUpEvent, SearchTextBox_TextInput, RoutingStrategies.Tunnel);

            LoadZakaz();
        }

        private async void BackButton_Click(object? sender, RoutedEventArgs e)
        {
            Auterling auterling = new Auterling();
            await auterling.ShowDialog(this);
            Close();
        }

        private void LoadZakaz()
        {
            List<Order> orders = new List<Order>();
            string SearchText = SearchTextBox.Text ?? "";
            var Orders = Helper.postgres.Orders.Select(x => new
            {
                Orderid = x.Orderid,
                Status = x.Orderstatus,
                Deliverydate = x.Orderdeliverydate,
                Pickuppoint = x.Orderpickuppoint
            });

            if (!string.IsNullOrEmpty(SearchText))
                Orders = Orders.Where(t => t.Pickuppoint.Contains(SearchText));

            if (SortComboBox.SelectedIndex == 0)
            {
                Orders = Orders.OrderBy(x => x.Pickuppoint);
            }
            else
            {
                Orders = Orders.OrderByDescending(x => x.Pickuppoint);
            }

            switch (FilterComboBox.SelectedIndex)
            {
                case 1:
                    Orders = Orders.Where(x => x.Status == "Новый");
                    break;

                case 2:
                    Orders = Orders.Where(x => x.Status == "Завершен");
                    break;
            }

            ZakazListBox.Items = Orders.ToList();

        }

        private async void AddZakazButton_Click(object? sender, RoutedEventArgs e)
        {
            AddingZakaz addingZakaz = new AddingZakaz();
            await addingZakaz.ShowDialog(this);
            Close();
        }

        private void FilterComboBox_LostFocus(object? sender, SelectionChangedEventArgs e)
        {
            LoadZakaz();
        }

        private void SortComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadZakaz();
        }

        

        private async void RedactZakazButton_Click(object? sender, RoutedEventArgs e)
        {
            int button = (int)(sender as Button).Tag;

            AddingZakaz addingZakaz = new AddingZakaz(button);
            await addingZakaz.ShowDialog(this);
            LoadZakaz();
        }

        private void SearchTextBox_TextInput(object? sender, KeyEventArgs e)
        {
            LoadZakaz();
        }
    }
}
