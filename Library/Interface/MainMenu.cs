using System;
using Program;
using MemberService;


namespace UI
{
    class MainMenu : LibraryMenu
    {
        public MainMenu(string parentPage, Library library) : base(parentPage, library)
        {
            name = "Main Menu";
            backCommand = "Exit";
        }

        [Command("Staff Login")]
        public void GotoStaff()
        {
            var page = new StaffMenu(name, library);
            page.CommandWait();
        }

        [Command("Member Login")]
        public void GotoMember()
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var Password = Console.ReadLine();

            try
            {
                var user = library.Members.FindMember(username, Password);
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

