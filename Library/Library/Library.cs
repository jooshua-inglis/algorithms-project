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
            if (Movies.NodeCount <= 10)
                AddToMostPopular(movie, Movies.NodeCount - 1);
        }

        private void InsertionPut(Movie[] A, int i)
        {
            Movie v = A[i];
            int j = i - 1;
            while (j >= 0 && (A[j].BorrowedCount < v.BorrowedCount))
            {
                A[j + 1] = A[j--];
            }
            A[j + 1] = v;
        }

        private void SortByBorrowedCount(Movie[] A, int max)
        {
            for (int i = 0; i < max; ++i)
                InsertionPut(A, i);
        }

        private void ReorderMostPopular(Movie updatedMovie)
        {
            /* Only updates the order of items and subsitutes items. Items are never
            added or subtracted */

            if (Movies.NodeCount <= 10)
            {
                SortByBorrowedCount(MostPopular, Movies.NodeCount);
                return;
            }
            if (updatedMovie.BorrowedCount < MostPopular[9].BorrowedCount)
                return;

            if (MovieTitleInArray(MostPopular, updatedMovie.Title) == -1 )
                MostPopular[9] = updatedMovie;
            else
                SortByBorrowedCount(MostPopular, 10);
        }

        private int MovieTitleInArray(Movie[] MovieArray, string title)
        {
            int count = 0;
            foreach (var movie in MovieArray)
            {
                if (movie == null)
                    break;
                else if (title.ToLower() == movie.Key)
                    return count;
                count++;
            }
            return -1;
        }

        public void AddToMostPopular(Movie updatedMovie, int index)
        {
            if (MovieTitleInArray(MostPopular, updatedMovie.Title) == -1)
                MostPopular[index] = updatedMovie;
        }

        public void UpdateMostPopular()
        {
            MostPopular = new Movie[10];

            var allSorted = Movies.GetBorrowCountArray();
            Array.Resize(ref allSorted, 10);

            MostPopular = allSorted;
        }

        public void BorrowMovie(Member member, Movie movie)
        {
            var memberMovie = movie.Copy();
            memberMovie.Avaliable = null;
            movie.BorrowedCount++;
            member.BorrowedMovies.AddMovie(memberMovie);

            ReorderMostPopular(movie);
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

            if (MovieTitleInArray(MostPopular, title) != -1)
                UpdateMostPopular();
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
