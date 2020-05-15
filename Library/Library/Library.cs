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

        // MergeSort
        private void SortBorrowedCount(Movie[] arr, int i, int j)
        {
            if (i < j)
            {
                int m = (i + j) / 2;

                SortBorrowedCount(arr, i, m);
                SortBorrowedCount(arr, m + 1, j);
                MergeBorrowedCount(arr, i, m, j);
            }
        }

        // Part of merge sort 
        private void MergeBorrowedCount(Movie[] A, int i, int m, int j)
        {
            int n = A.Length;
            var T = new Movie[n];
            A.CopyTo(T, 0);

            int p = i, q = m + 1, r = i;

            while (p <= m && q <= j)
            {
                if (A[p].BorrowedCount >= A[q].BorrowedCount)
                    T[r] = A[p++];
                else
                    T[r] = A[q++];
                r++;
            }
            if (p <= m)
                Array.Copy(A, p, T, r, j - r + 1);
            if (q <= j)
                Array.Copy(A, q, T, r, j - r + 1);
            T.CopyTo(A, 0);
        }
    }
}
