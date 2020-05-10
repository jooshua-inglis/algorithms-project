using System.Text;
using System;

namespace MovieService
{
    public class Movie
    {
        public int? Avaliable { get; set; } // Only used for library
        public string Classification { get; }
        public int Count { get; set; }
        public string Director { get; }
        public string Genre { get; }
        public string Key { get { return Title.ToLower(); } }
        public string Title { get; }
        public string Staring { get; }
        public int Rating { get; } // rating out of 100
        public DateTime ReleaseDate { get; }
        public int BorrowedCount { get; set; }

        public Movie(
            string classification,
            string director,
            string genre,
            string title,
            string staring,
            int rating,
            DateTime releaseDate)
        {
            Classification = classification;
            Director = director;
            Genre = genre;
            Title = title;
            Staring = staring;
            Rating = rating;
            ReleaseDate = releaseDate;
            Count = 1;
        }

        public static bool IsValidClassification(string classification)
        {
            foreach (var validClassification in new string[] { "G", "PG", "M", "M15+" })
                if (validClassification == classification)
                    return true;
            return false;
        }
        private int CompareTitles(Movie other)
        {
            return Title.CompareTo(other.Title);
        }

        public Movie Copy()
        {
            var newMovie = (Movie)this.MemberwiseClone();
            newMovie.Count = 1;
            newMovie.Avaliable = 1;
            return newMovie;
        }

        override public string ToString()
        {
            var date = ReleaseDate.ToString("dd/MM/yyyy");
            var output = new StringBuilder()
                .Append($"Title: {Title}")
                .Append($"\nDirector: {Director}")
                .Append($"\nStaring: {Staring[0]}")
                .Append($"\nGenre: {Genre}")
                .Append($"\nRating: {Rating}")
                .Append($"\nRelease Date: {date}")
                .Append($"\nCount: {Count}");

            if (Avaliable != null)
                output.Append($"\nAvaliable: {Avaliable}");

            return output.ToString();
        }
    }
}
