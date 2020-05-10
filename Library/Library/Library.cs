using System;
using MemberService;
using MovieService;

namespace Program
{
    public class Library
    {
        public MemberCollection Members { get; set; }
        public Movie[] MostPopular { get; set; }
        public MovieCollection Movies { get; set; }
        private int mostPopularCount = 0;

        public Library()
        {
            Members = new MemberCollection();
            Movies = new MovieCollection();
            MostPopular = new Movie[10];
        }

        public void AddMovie(Movie movie)
        {
            movie.Avaliable = 1;
            Movies.AddMovie(movie);
            if (mostPopularCount <= 10)
                mostPopularCount++;
            UpdateMostPopular(movie);
        }

        private void SortByBorrowedCount(Movie[] A, int i)
        {
            Movie v = A[i];
            int j = i - 1;
            while (j >= 0 && (A[j].BorrowedCount < v.BorrowedCount))
            {
                A[j + 1] = A[j--];
            }
            A[j + 1] = v;
        }

        public void UpdateMostPopular(Movie updatedMovie)
        {
            if (MostPopular[9] != null && updatedMovie.BorrowedCount < MostPopular[9].BorrowedCount)
                return;
            int i;
            bool exists = false;
            for (i = 0; i < 10 && MostPopular[i] != null; ++i)
            {
                if (updatedMovie.Title == MostPopular[i].Title)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
                MostPopular[Math.Min(i, 9)] = updatedMovie;

            if (i > 0)
                SortByBorrowedCount(MostPopular, Math.Min(i, 9));
        }

        public void BorrowMovie(Member member, Movie movie)
        {
            var memberMovie = movie.Copy();
            memberMovie.Avaliable = null;

            movie.BorrowedCount++;
            UpdateMostPopular(movie);
            member.BorrowedMovies.AddMovie(memberMovie);
            movie.Avaliable--;
        }

        public void BorrowMovie(Member member, string title)
        {
            var movie = Movies.FindMovie(title);

            if (movie == null || movie.Avaliable == 0)
                throw new MovieError("Movie not avaliable for borrowing");

            BorrowMovie(member, movie);
        }

        public void DeleteMovie(string title)
        {
            var movie = Movies.FindMovie(title);
            if (movie == null || movie.Avaliable == 0)
            {
                throw new MovieError("Movie not in collection or all copies are borrowed");
            }
            Movies.DeleteMovie(title);
            MostPopular = new Movie[10];

            var allSorted = Movies.GetBorrowCountArray();

            var mostPopularCount = Math.Min(9, Movies.NodeCount);

            for (int i = 0; i < mostPopularCount; ++i)
                MostPopular[mostPopularCount - i] = allSorted[i];
        }

        public void ReturnMovie(Member member, string title)
        {
            if (member.BorrowedMovies.FindMovie(title) != null)
            {
                member.BorrowedMovies.DeleteMovie(title);
                Movies.FindMovie(title).Avaliable++;
            }
            else
                throw new MovieError("Member has not borrowed movie");
        }
    }
}
