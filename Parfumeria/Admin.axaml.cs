using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Parfumeria
{
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            ZakazButton.Click += ZakazButton_Click;
            TovarButton.Click += TovarButton_Click;
        }

        private async void TovarButton_Click(object? sender, RoutedEventArgs e)
        {
            AdminTovar adminTovar = new AdminTovar();
            await adminTovar.ShowDialog(this);
            Close();
        }

        private async void ZakazButton_Click(object? sender, RoutedEventArgs e)
        {
            AdminZakaz adminZakaz = new AdminZakaz();
            await adminZakaz.ShowDialog(this);
            Close();
        }
    }
}
