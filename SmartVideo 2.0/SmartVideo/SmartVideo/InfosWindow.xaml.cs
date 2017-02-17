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
using DTOLibrary;
using System.Collections.ObjectModel;

namespace SmartVideo
{
    /// <summary>
    /// Logique d'interaction pour InfosWindows.xaml
    /// </summary>
    public partial class InfosWindows : Window
    {

        public InfosWindows(FilmDTO film, ServiceReference.ServiceWCFSmartClient client)
        {
            InitializeComponent();
            film = client.GetFilmInfo(film.id);
            posterImg.Source = new BitmapImage(new Uri("http://image.tmdb.org/t/p/w185/"+film.poster_path, UriKind.RelativeOrAbsolute));
            title.Content = film.titre;
            oriTitle.Content = film.original_title;
            runtime.Content = film.runtime + " minutes";
            genresLB.ItemsSource = new ObservableCollection<GenreDTO>(film.genres);
            genresLB.DisplayMemberPath = "Name";
            actorsLB.ItemsSource = new ObservableCollection<ActorDTO>(film.actors);
            actorsLB.DisplayMemberPath = "name";
            realisatorsLB.ItemsSource = new ObservableCollection<RealisateurDTO>(film.realisateurs);
            realisatorsLB.DisplayMemberPath = "Name";

        }
    }
}
