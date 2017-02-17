using DTOLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {
        public ObservableCollection<RequeteDTO> Reqs = new ObservableCollection<RequeteDTO>();
        public RequestsWindow()
        {
            InitializeComponent();
            requetesListView.ItemsSource = Reqs;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            refreshListView(this, null);
        }
        private void refreshListView(object sender, RoutedEventArgs e)
        {
            //requetesListView.Items.Clear();
            List<RequeteDTO> listReq = BusinessLogicLayer.BLLVideotheque.getAllWaitingRequest();
            foreach (var f in listReq)
            {
                if(!Reqs.Any(p => p.id == f.id))
                {
                    Reqs.Add(f);
                }
            }


        }

    }
}
