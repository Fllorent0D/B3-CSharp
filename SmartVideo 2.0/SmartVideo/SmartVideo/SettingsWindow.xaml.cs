using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLogicLayer;

namespace SmartVideo
{
    /// <summary>
    /// Logique d'interaction pour SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(nameTB.Text.Length == 0 || instanceTB.Text.Length == 0 || hostTB.Text.Length == 0)
            {
                MessageBox.Show("Merci de remplir les 3 champs.","Erreur", MessageBoxButton.OK, MessageBoxImage.Exclamation );
                return;
            }
            RegistryHelper reg = new RegistryHelper();
            reg.Write("dbHost", hostTB.Text);
            reg.Write("dbInstance", instanceTB.Text);
            reg.Write("dbName", nameTB.Text);
            this.Close();
        }
    }
}
