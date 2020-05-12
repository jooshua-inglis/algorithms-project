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

        public Movie[] GetMostPopularMovies()
        {
            var allSorted = Movies.GetBorrowCountArray();
            Array.Resize(ref allSorted, Math.Min(Movies.CountUnique, 10));

            return allSorted;
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
    }
}
