using System.Text;
using System;
using MovieService;

namespace MemberService
{
    public class Member
    {
        public string Address { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Number { get; }
        public int NumberInt { get { return Int32.Parse(Number); } }
        public string Password { get; }
        public string UserName { get { return FirstName + LastName; } }
        public int BorrowedCount { get; set; }

        public MovieCollection BorrowedMovies { get; }

        public Member(string address, string firstName, string lastName, string number, string password)
        {
            try
            {
                Int32.Parse(number);
            }
            catch (FormatException)
            {
                throw new UserError("Phone number in invalid format");
            }

            if (password.Length != 4)
                throw new UserError("Password must of length 4");

            Address = address;
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            Password = password;
            BorrowedMovies = new MovieCollection();
        }

        public bool CheckPassword(string password)
        {
            return password == Password;
        }

        public static bool operator >(Member member1, Member member2)
        {
            return member1.UserName.CompareTo(member2.UserName) == 1;
        }
        public static bool operator <(Member member1, Member member2)
        {
            return member1.UserName.CompareTo(member2.UserName) == -1;
        }

        override public string ToString()
        {
            var output = new StringBuilder()
                .Append($"Username: {UserName}")
                .Append($"\nName: {FirstName} {LastName}")
                .Append($"\nAddress: {Address}")
                .Append($"\nPhone Number {Number}");

            return output.ToString();
        }
    }
}
