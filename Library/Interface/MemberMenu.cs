using System;
using Program;
using MemberService;

namespace UI
{
    class MemberMenu : LibraryMenu
    {
        private Member member;

        public MemberMenu(string parentPage, Library library, Member member) : base(parentPage, library)
        {
            this.member = member;
            name = "Member Menu";
        }

        [Command("Display all movies")]
        public void DisplayAll()
        {
            Console.WriteLine(new string('=', 30));
            library.Movies.PrintInorder();
            EnterToContinue();
        }

        [Command("Borrow a movie DVD")]
        public void Borrow()
        {
            Console.Write("Title of movie you'd like to borrow: ");
            var title = Console.ReadLine();
            try
            {
                library.BorrowMovie(member, title);
                Console.WriteLine($"Successfull borrowed {title}");
            }
            catch (MovieService.MovieError err)
            {
                Console.WriteLine(err.Message);
            }

            EnterToContinue();
        }

        [Command("Return a movie DVD")]
        public void ReturnDvd()
        {
            Console.Write("Enter title of movie to return: ");
            string title = Console.ReadLine();

            try
            {
                library.ReturnMovie(member, title);
                Console.WriteLine("Successfull returned movie");
            }
            catch (MovieService.MovieError err)
            {
                Console.WriteLine(err.Message);
            }
            EnterToContinue();
        }

        [Command("List current borrowed movie DVDs")]
        public void ListCurrent()
        {
            member.BorrowedMovies.PrintInorder();
            EnterToContinue();
        }

        [Command("Display top 10 most popular movies")]
        public void TopTen()
        {
            int count = 1;
            foreach (var movie in library.GetMostPopularMovies())
            {
                Console.WriteLine($"{count++}: {movie.Title,-20} Borrowed {movie.BorrowedCount} times");
            }
            EnterToContinue();
        }
    }
}
