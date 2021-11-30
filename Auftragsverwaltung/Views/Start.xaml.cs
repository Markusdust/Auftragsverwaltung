
using System.Windows;

using BusinessLogik;

namespace Auftragsverwaltung.Views
{
    /// <summary>
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        private Controller controller;

        public Start(Controller c)
        {
            InitializeComponent();
            controller = c;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            controller.testkundeanlegen();
        }
    }
}
