using System;
using MemberService;
using MovieService;

namespace Program
{
    public class Library
    {
        public MemberCollection Members { get; set; }
        public MovieCollection Movies { get; set; }

        public Library()
        {
            Members = new MemberCollection();
            Movies = new MovieCollection();
        }

        public void AddMovie(Movie movie)
        {
            movie.Avaliable = 1;
            Movies.AddMovie(movie);
        }

        private void BorrowMovie(Member member, Movie movie)
        {
            var memberMovie = movie.Copy();
            memberMovie.Avaliable = null;
            movie.BorrowedCount++;
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

        public Movie[] GetMostPopularMovies()
        {
            var movieArray = Movies.AsArray();

            SortBorrowedCount(movieArray, 0, Movies.CountUnique - 1);
            Array.Resize(ref movieArray, Math.Min(Movies.CountUnique, 10));

            return movieArray;
        }

        private void SortBorrowedCount(Movie[] arr, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;

                SortBorrowedCount(arr, l, m);
                SortBorrowedCount(arr, m + 1, r);
                MergeBorrowedCount(arr, l, m, r);
            }
        }

        private void MergeBorrowedCount(Movie[] arr, int l, int m, int r)
        {
            int n1 = m - l + 1;
            int n2 = r - m;

            var L = new Movie[n1];
            var R = new Movie[n2];

            int i; int j;

            for (i = 0; i < n1; ++i)
                L[i] = arr[l + i];
            for (j = 0; j < n2; ++j)
                R[j] = arr[m + 1 + j];

            i = 0; j = 0;

            int k = l;
            while (i < n1 && j < n2)
            {
                if (L[i].BorrowedCount >= R[j].BorrowedCount)
                    arr[k] = L[i++];
                else
                    arr[k] = R[j++];

                k++;
            }

            while (i < n1)
                arr[k++] = L[i++];

            while (j < n2)
                arr[k++] = R[j++];
        }
    }
}
