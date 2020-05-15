using System;
using MemberService;

namespace Program
{
    static class Debug
    {
        public static void DebugPrefill(Library lib)
        {

            lib.Members.AddMember(new Member("", "t", "", "0448424240", "aaaa"));
            lib.Members.AddMember(new Member("2", "Josh", "Inglis", "0448424240", "abcd"));

            var movies = new MovieService.Movie[] {
                new MovieService.Movie("M", "Gus Van Sant", "Drama", "Good Will Hunting",  "Matt Damon" , 100, new System.DateTime()),
                new MovieService.Movie("PG", "Sam Raimi", "Action", "Spider Man",  "Tobey Maguire" , 100, new System.DateTime()),
                new MovieService.Movie("M15+", "Brett Ratner", "Thriller", "Red Dragon",  "Anthony Hopkins" , 50, new System.DateTime()),
                new MovieService.Movie("M15+", "Sylvester Stallone", "Action", "Rambo",  "Sylvester Stallone" , 50, new System.DateTime()),
                new MovieService.Movie("M", "Jon Favreau", "Action", "Iron Man",  "Robert Downy jn" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Peyton Reed", "Action", "Ant Man",  "Paul Rudd" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Andrew Stanton", "Animated", "Wall-e",  "Ben Burtt" , 50, new System.DateTime()),
                new MovieService.Movie("M", "Taika Watiti", "Action", "Thor: Ragnarok",  "Chris Hemsworth" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Chris Columbus", "Action", "Harry Potter",  "Daniel Radcliffe" , 50, new System.DateTime()),
                new MovieService.Movie("PG", "Christopher Nolan", "Drama", "Intersteller",  "Matthew McConaughey" , 50, new System.DateTime()),
                new MovieService.Movie("M15+", "James Camerson", "Action", "Terminator",  "Me" , 50, new System.DateTime())
            };

            movies[0].BorrowedCount = 6;
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

            var joshi = lib.Members.FindMember("JoshInglis", "abcd");

            for (int i = 0; i < 5; ++i)
            {
                lib.BorrowMovie(joshi, movies[0].Title);
                lib.ReturnMovie(joshi, movies[0].Title);
            }
        }
    }
}
