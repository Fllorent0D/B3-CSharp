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

namespace SmartVideo
{
    /// <summary>
    /// Logique d'interaction pour SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private ServiceReference.ServiceWCFSmartClient clientWCF;
        private MainWindow parent;
        public SearchWindow(MainWindow parent)
        {
            InitializeComponent();
            clientWCF = parent.getClientWFC();
            typeRecherche.SelectedIndex = 0;
            this.parent = parent;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            parent.setFilms(clientWCF.rechercherFilms(typeRecherche.Text, textRecherche.Text).ToList());
        }
    }
}
