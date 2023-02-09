using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Parfumeria.database;
using System.Collections.Generic;
using System.Linq;

namespace Parfumeria
{
    public partial class AdminTovar : Window
    {
        bool isAdmin = false;
        public AdminTovar()
        {
            InitializeComponent();
            InitializeWindow();
            LoadTovar();
            DataContext = isAdmin;
            this.isAdmin = isAdmin;
        }

        public void InitializeWindow()
        {
            SortComboBox.SelectionChanged += SortComboBox_SelectionChanged;
            FilterComboBox.SelectionChanged += FilterComboBox_LostFocus;
            AddTovarButton.Click += AddTovarButton_Click;
            BackButton.Click += BackButton_Click;
            SearchTextBox.AddHandler(KeyUpEvent, SearchTextBox_TextInput, RoutingStrategies.Tunnel);


            LoadTovar();


        }

        private async void BackButton_Click(object? sender, RoutedEventArgs e)
        {
            Auterling auterling = new Auterling();
            await auterling.ShowDialog(this);
            Close();
        }

        private void FilterComboBox_LostFocus(object? sender, SelectionChangedEventArgs e)
        {
            LoadTovar();
        }

        private void LoadTovar()
        {

            List<Product> products = new List<Product>();
            string SearchText = SearchTextBox.Text ?? "";

            var Products = Helper.postgres.Products.Select(x => new
            {

                x.Productname,
                x.Productdescription,
                x.Mainimage,
                x.Productarticlenumber,
                x.Productcost,
                x.Productmanufacturer,
                x.Productdiscountamount,

            });

            if (!string.IsNullOrEmpty(SearchText))
            {
                Products = Products.Where(x => x.Productname.Contains(SearchText));
            }


            if (SortComboBox.SelectedIndex == 0)
            {
                Products = Products.OrderBy(x => x.Productcost);
            }
            else
            {
                Products = Products.OrderByDescending(x => x.Productcost);
            }




            switch (FilterComboBox.SelectedIndex)
            {
                case 1:
                    Products = Products.Where(x => x.Productdiscountamount >= 0 && x.Productdiscountamount < 10);
                    break;

                case 2:
                    Products = Products.Where(x => x.Productdiscountamount >= 10 && x.Productdiscountamount < 15);
                    break;

                case 3:
                    Products = Products.Where(x => x.Productdiscountamount >= 15 && x.Productdiscountamount < 20);
                    break;

                case 4:
                    Products = Products.Where(x => x.Productdiscountamount >= 20 && x.Productdiscountamount < 25);
                    break;

                case 5:
                    Products = Products.Where(x => x.Productdiscountamount >= 25 && x.Productdiscountamount < 30);
                    break;




            }

            TovarListBox.Items = Products.Select(x => new
            {

                Title = x.Productname,
                Description = x.Productdescription,
                x.Mainimage,

                Cost = x.Productcost,
                Manufacturer = x.Productmanufacturer,
                Discountamount = x.Productdiscountamount + "%",
                Color = x.Productdiscountamount >= 15 ? Brushes.Chartreuse : Brushes.White
            });

        }


        private async void AddTovarButton_Click(object? sender, RoutedEventArgs e)
        {
            AddingTovar addingTovar = new AddingTovar();
            await addingTovar.ShowDialog(this);
            LoadTovar();
        }

        private async void RedactTovarButton_Click(object? sender, RoutedEventArgs e)
        {
            string button = (string)(sender as Button).Tag;

            AddingTovar addingTovar = new AddingTovar(button);
            await addingTovar.ShowDialog(this);
            LoadTovar();
        }

        private async void DeleteTovarButton_Click(object? sender, RoutedEventArgs e)
        {
            string button = (string)(sender as Button).Tag;

            Helper.postgres.Products.Remove(Helper.postgres.Products.Find(button));
            Helper.postgres.SaveChanges();
            LoadTovar();
        }

        private void SortComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadTovar();
        }

        private void SearchTextBox_TextInput(object? sender, KeyEventArgs e)
        {
            LoadTovar();
        }
    }
}
