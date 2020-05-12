using MemberService;

namespace Program
{
    static class Debug
    {
        public static void DebugPrefill(Library lib)
        {
            lib.Members.AddMember(new Member("2", "Josh", "i", "0448424240", "abcd"));
            lib.Members.AddMember(new Member("2", "Whata", "B", "0458424240", "abcd"));
            lib.Members.AddMember(new Member("2", "Wta", "B", "0458424244", "abcd"));
            lib.Members.AddMember(new Member("2", "Whatmmeme", "B", "0458324244", "abcd"));

            var movies = new MovieService.Movie[] {
                new MovieService.Movie("PG", "Me", "Action", "Hello There",  "Me" , 100, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "General Kenobi",  "Me" , 100, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Simposnos",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Rambo",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Iron Man",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Super Man",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Wall-e",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Thor",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Harry Potter",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Interstaller",  "Me" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Me", "Action", "Terminator",  "Me" , 50, new System.DateTime())
            };

            movies[0].BorrowedCount = 135;
            movies[1].BorrowedCount = 1;
            movies[2].BorrowedCount = 5;
            movies[3].BorrowedCount = 100;
            movies[4].BorrowedCount = 5;
            movies[5].BorrowedCount = 60;
            movies[6].BorrowedCount = 4;
            movies[7].BorrowedCount = 50;
            movies[8].BorrowedCount = 5;
            movies[9].BorrowedCount = 4;
            movies[10].BorrowedCount = 22;


            foreach (var movie in movies)
                lib.AddMovie(movie);

            var joshi = lib.Members.FindMember("Joshi", "abcd");

            lib.GetMostPopularMovies();

            for (int i = 0; i < 5; ++i)
            {
                lib.BorrowMovie(joshi, "General Kenobi");
                lib.ReturnMovie(joshi, "General Kenobi");
            }
        }
    }
}
