using System.Globalization;
using System;
using Program;
using MemberService;

namespace UI
{
    class StaffMenu : LibraryMenu
    {
        public StaffMenu(string parentPage, Library library) : base(parentPage, library)
        {
            name = "Staff Menu";
        }

        [Command("Add a new movie DVD")]
        public void AddMovie()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();

            Console.Write("Rating (out of 10): ");
            var rating = Convert.ToInt32(double.Parse(Console.ReadLine()) * 10);

            Console.Write("Director: ");
            var director = Console.ReadLine();

            Console.Write("Staring: ");
            var staring = Console.ReadLine();

            DateTime releaseDate;
            do
            {
                Console.Write("Release Data (dd/mm/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate));

            Console.Write("Genre: ");
            var genre = Console.ReadLine();

            string classification;
            do
            {
                Console.Write("Classification (G, PG, M, M15+): ");
                classification = Console.ReadLine();
            } while (!MovieService.Movie.IsValidClassification(classification));


            var newMovie = new MovieService.Movie(classification, director, genre, title, staring, rating, releaseDate);

            library.AddMovie(newMovie);
        }

        [Command("Remove a movie DVD")]
        public void RemoveMovie()
        {
            Console.Write("Enter Movie title: ");
            try
            {
                library.DeleteMovie(Console.ReadLine());
                Console.WriteLine("Successfully delete movie");
            }
            catch (MovieService.MovieError err)
            {
                Console.WriteLine(err.Message);
            }
            EnterToContinue();
        }

        [Command("Register a new member")]
        public void RegisterMember()
        {
            Console.Write("First Name: ");
            var first = Console.ReadLine();

            Console.Write("Last Name: ");
            var last = Console.ReadLine();

            Console.Write("Phone Number: ");
            var number = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            Console.Write("Address: ");
            var address = Console.ReadLine();

            try
            {
                var member = new Member(address, first, last, number, password);
                library.Members.AddMember(member);
            }
            catch (UserError err)
            {
                Console.WriteLine("\nError: " + err.Message);
            }

            EnterToContinue();
        }

        [Command("Find a registered member's phone number")]
        public void FindPhoneNumber()
        {
            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();

             Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();
            
            try
            {
                var member = library.Members.FindMember(firstName + lastName);
                Console.WriteLine("Phone Number: \n" + member.Number);
            }
            catch (UserError err)
            {
                Console.WriteLine(err.Message);
            }
            EnterToContinue();
        }
    }
}

