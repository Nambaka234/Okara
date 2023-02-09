using Avalonia.Controls;
using Avalonia.Interactivity;
using Parfumeria.database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System;
using Tmds.DBus;
using System.Linq;
using Avalonia.Controls.Shapes;
using System.IO;
using Path = System.IO.Path;

namespace Parfumeria
{
    public partial class AddingTovar : Window
    {
        public AddingTovar()
        {
            InitializeComponent();


            SaveButton.Click += AddButton_Click;

            SelectFileButton.Click += SelectFileButton_Click;
            BackButton.Click += BackButton_Click;
            Closing += AddingService_Closing;

            DataContext = product;
        }



        public AddingTovar(string Productarticlenumber)
        {
            InitializeComponent();
            product = Helper.postgres.Products.Find(Productarticlenumber);

            SaveButton.Click += SaveButton_Click;
            BackButton.Click += BackButton_Click;
            SelectFileButton.Click += SelectFileButton_Click;

            Closing += AddingService_Closing;

            DataContext = product;
        }

        private async void BackButton_Click(object? sender, RoutedEventArgs e)
        {
            Auterling auterling = new Auterling();
            await auterling.ShowDialog(this);
            Close();
        }

        private async void SelectFileButton_Click(object? sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filters?.Add(fileFilter);

            string[]? result = await dialog.ShowAsync(this);
            if (result == null)
                return;

            string imageName = Path.GetFileName(result[0]);
            File.Copy(result[0], $"./products/{imageName}", true);

            photo.Text = $"\\products\\{imageName}";
        }


        private void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            Helper.postgres.SaveChanges();
            this.Close();
        }

        private void AddingService_Closing(object? sender, CancelEventArgs e)
        {
            Hide();
        }

        public Product product;

        private readonly FileDialogFilter fileFilter = new FileDialogFilter()
        {
            Extensions = new List<string>() { "png", "jpg", "jpeg" },
            Name = "Image Files"
        };

        private void AddButton_Click(object? sender, RoutedEventArgs e)
        {
            product = new Product();
            PostgresContext database = new PostgresContext();
            var query6 =
                from ag in database.Products
                select ag.Productarticlenumber;



            product.Productarticlenumber = articulenamuber.Text;
            product.Productdescription = description.Text;
            product.Productname = name.Text;
            product.Productcategory = category.Text;
            product.Productmanufacturer = manufacturer.Text;
            product.Productcost = Convert.ToInt32(cost.Text);
            product.Productdiscountamount = Convert.ToInt16(discountamount.Text);
            product.Productquantityinstock = Convert.ToInt32(quantityinstock.Text);

            product.Productphoto = photo.Text;


            Helper.postgres.Add(product);
            Helper.postgres.SaveChanges();
            this.Close();
        }
    }
}
