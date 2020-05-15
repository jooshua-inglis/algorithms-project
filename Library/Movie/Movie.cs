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
        public int Runtime { get; } // in seconds 
        public DateTime ReleaseDate { get; }
        public int BorrowedCount { get; set; }

        public Movie(
            string classification,
            string director,
            string genre,
            string title,
            string staring,
            int runtime,
            DateTime releaseDate)
        {
            Classification = classification;
            Director = director;
            Genre = genre;
            Title = title;
            Staring = staring;
            Runtime = runtime;
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
                .Append($"\nStaring: {Staring}")
                .Append($"\nGenre: {Genre}")
                .Append($"\nRuntime: {Runtime}s")
                .Append($"\nRelease Date: {date}")
                .Append($"\nCount: {Count}");

            if (Avaliable != null)
                output.Append($"\nAvaliable: {Avaliable}");

            return output.ToString();
        }
    }
}
