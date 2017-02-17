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
    /// Logique d'interaction pour NewsWindows.xaml
    /// </summary>
    ///

    public partial class NewsWindows : Window
    {
        private ObservableCollection<PostDTO> Posts = new ObservableCollection<PostDTO>();
        public NewsWindows()
        {
            
            InitializeComponent();
            loadPosts();
            newsLB.ItemsSource = Posts;
            newsLB.DisplayMemberPath = "Titre";
            //Posts = BusinessLogicLayer.BLLVideotheque.getNews();


        }
        private void loadPosts()
        {
            List<PostDTO> listP = BusinessLogicLayer.BLLVideotheque.getNews();
            foreach (var f in listP)
            {
                Posts.Add(f);
            }
        }
        private void ajouter_Click(object sender, RoutedEventArgs e)
        {
            PostDTO p = BusinessLogicLayer.BLLVideotheque.addPost(titreTB.Text, contenuTB.Text);
            Posts.Add(p);
            newsLB.SelectedItem = p;
        }

        private void newsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(newsLB.SelectedItem != null)
            {
                titreTB.Text = ((PostDTO)newsLB.SelectedItem).Titre;
                contenuTB.Text = ((PostDTO)newsLB.SelectedItem).Contenu;
                supprimerBtn.IsEnabled = true;
                sauverBtn.IsEnabled = true;

            }
           

        }

        private void supprimerBtn_Click(object sender, RoutedEventArgs e)
        {
            BusinessLogicLayer.BLLVideotheque.deleteNews(((PostDTO)newsLB.SelectedItem).Id);
            Posts.Remove((PostDTO)newsLB.SelectedItem);
            supprimerBtn.IsEnabled = false;
            sauverBtn.IsEnabled = false;

        }

        private void sauverBtn_Click(object sender, RoutedEventArgs e)
        {
            PostDTO p = BusinessLogicLayer.BLLVideotheque.updateNews(((PostDTO)newsLB.SelectedItem).Id, titreTB.Text, contenuTB.Text);
            int pos = newsLB.SelectedIndex;

            Posts.Remove((PostDTO)newsLB.SelectedItem);
            Posts.Insert(pos, p);
            newsLB.SelectedItem = p;
        }
    }
}
