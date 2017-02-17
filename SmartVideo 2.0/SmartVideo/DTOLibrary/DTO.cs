using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLibrary
{
    public class RequeteDTO
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _idFilm;

        public int idFilm
        {
            get { return _idFilm; }
            set { _idFilm = value; }
        }

        private string _status;

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Description
        {
            get {
                switch (status)
                {
                    case "Request_Film_Created": return "Demande du film créé";
                    case "Waiting_Film_Available": return "Film n'est pas disponible et sera réservé quand il est disponible";
                    case "Film_Owned": return "Film disponible";
                    case "Request_Film_Disposal": return "Film en attente de retour au dépot";
                    case "Film_Disposed": return "Film rentré au dépot";
                    default: return "Problème de status";
                }
            }

        }

    }

    public class FilmDTO : IEquatable<FilmDTO>
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _titre;

        public string titre
        {
            get { return _titre; }
            set { _titre = value; }
        }

        private string _original_title;

        public string original_title
        {
            get { return _original_title; }
            set { _original_title = value; }
        }

        private int _runtime;

        public int runtime
        {
            get { return _runtime; }
            set { _runtime = value; }
        }
        private string _poster_path;

        public string poster_path
        {
            get { return _poster_path; }
            set { _poster_path = value; }
        }
        private bool _available;

        public bool available
        {
            get { return _available; }
            set { _available = value; }
        }

        private List<GenreDTO> _genres;

        public List<GenreDTO> genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        private List<ActorDTO> _actors;

        public List<ActorDTO> actors
        {
            get { return _actors; }
            set { _actors = value; }
        }
        private List<RealisateurDTO> _realisateurs;

        public List<RealisateurDTO> realisateurs
        {
            get { return _realisateurs; }
            set { _realisateurs = value; }
        }
        public override string ToString()
        {
            return id + " | " + titre + " | " + this.original_title + " | " + runtime + " | " + poster_path + " | " + available + " | " + string.Join(", ", genres) + " | " + string.Join(", ", actors) + " | " + string.Join(", ", realisateurs);
        }

        public bool Equals(FilmDTO other)
        {
            if (other.id == this.id)
                return true;
            else
                return false;
        }
    }
    public class ActorDTO
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _character;

        public string character
        {
            get { return _character; }
            set { _character = value; }
        }
        public override string ToString()
        {
            return id+ " " + name + " " + character;
        }
    }
    public class GenreDTO
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public override string ToString()
        {
            return id + " " + Name;
        }
    }
    public class RealisateurDTO
    {
        private int _id;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public override string ToString()
        {
            return id + " " + Name;
        }
    }
    public class PostDTO : IEquatable<PostDTO>
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _titre;

        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }
        private string _contenu;

        public string Contenu
        {
            get { return _contenu; }
            set { _contenu = value; }
        }
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public bool Equals(PostDTO other)
        {
            if (other.Id == this.Id)
                return true;
            else
                return false;
        }
    }
    
}
