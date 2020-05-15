using System;
using Program;
using MemberService;


namespace UI
{
    class MainMenu : AbstractMenu
    {
        public MainMenu(string parentPage, Library library) : base(parentPage, library)
        {
            name = "Main Menu";
            backCommand = "Exit";
        }

        [Command("Staff Login")]
        public void GotoStaff()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            if (username == "staff" && password == "today123")
            {
                var page = new StaffMenu(name, library);
                page.CommandWait();
            }
            else
                Console.WriteLine("Incorrect username password combo");
        }

        [Command("Member Login")]
        public void GotoMember()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            try
            {
                var user = library.Members.FindMember(username, password);
                var page = new MemberMenu(name, library, user);
                page.CommandWait();
            }
            catch (UserError err)
            {
                Console.WriteLine(err.Message);
                return;
            }
        }
    }
}
