using Avalonia.Controls;
using Avalonia.Interactivity;
using Parfumeria.database;
using System;
using System.Linq;
using System.ComponentModel;
using System.IO;

namespace Parfumeria
{
    public partial class AddingZakaz : Window
    {
        public AddingZakaz()
        {
            InitializeComponent();
            order = new Order();
            SaveButton.Click += AddButton_Click;
            Closing += AddingZakaz_Closing;
            BackButton.Click += BackButton_Click;

            DataContext = order;
        }

        public AddingZakaz(int Id)
        {
            InitializeComponent();
            order = Helper.postgres.Orders.Find(Id);

            SaveButton.Click += SaveButton_Click;
            BackButton.Click += BackButton_Click;

            Closing += AddingZakaz_Closing;

            DataContext = order;
        }

        private async void BackButton_Click(object? sender, RoutedEventArgs e)
        {
            Auterling auterling = new Auterling();
            await auterling.ShowDialog(this);
            Close();
        }

        public Order order;

        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            Helper.postgres.SaveChanges();
            this.Close();
        }

        private void AddingZakaz_Closing(object? sender, CancelEventArgs e)
        {
           Hide();
        }

        private void AddButton_Click(object? sender, RoutedEventArgs e)
        {
            order = new Order();
            PostgresContext database = new PostgresContext();

            var query3 =
                from or in database.Orders
                select or.Orderid;

            order.Orderid = Convert.ToInt32(query3.LongCount()) + 1;
            order.Orderstatus = status.Text;
            order.Orderdeliverydate = Convert.ToDateTime(deliverydate.Text);
            order.Orderpickuppoint = pickuppoint.Text;

            Helper.postgres.Add(order);
            Helper.postgres.SaveChanges();
            this.Close();
        }       
    }
}
