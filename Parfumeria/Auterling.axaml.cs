using Avalonia.Controls;
using Avalonia.Interactivity;
using Parfumeria.database;
using System.Collections.Generic;
using System.Linq;

namespace Parfumeria
{
    public partial class Auterling : Window
    {
        public Auterling()
        {
            InitializeComponent();
            SigninButton.Click += SigninButton_Click;
        }

        private async void SigninButton_Click(object? sender, RoutedEventArgs e)
        {
            PostgresContext postgresContext = new PostgresContext();

            var users =
                from u in postgresContext.Users
                where LoginTextBox.Text == u.Userlogin
                where PasswordTextBox.Text == u.Userpassword
                select u.Userrole;

            List<int> ints = users.ToList();

            if (ints.Count() > 0)
            {
                int rol = ints.ElementAt(0);

                if (rol == 1)
                {
                    Meneger meneger = new Meneger();
                    await meneger.ShowDialog(this);
                    Close();
                }

                else if (rol == 2)
                {
                    Klient klient = new Klient();
                    await klient.ShowDialog(this);
                    Close();
                }

                else if (rol == 3)
                {
                    Admin admin = new Admin();
                    await admin.ShowDialog(this);
                    Close();
                }
            }
            else 
            {
                MainWindow mainWindow = new MainWindow();
                await mainWindow.ShowDialog(this);
                Close();
            }
        }
    }
}
