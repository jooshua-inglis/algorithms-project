using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Program;

namespace UI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class Command : System.Attribute
    {
        public string Name { get; set; }
        public MethodInfo Func { get; set; }
        public Command(string name)
        {
            Name = name;
        }
    }

    abstract public class LibraryMenu
    {
        protected Library library;
        protected string parentName;
        protected string name;
        protected string backCommand;
        List<Command> commands;

        public string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft, '=').PadRight(length, '=');

        }

        public void DisplayCommands()
        {
            var totalLen = 30;
            Console.WriteLine(PadBoth(new string(name), totalLen));

            int i = 1;
            foreach (var command in commands)
                Console.WriteLine($"{i++}: {command.Name}");
            Console.WriteLine($"0: {backCommand}");
            Console.WriteLine(new string('=', 30));
        }

        public void CommandWait()
        {
            var loop = true;
            int commandId;
            while (loop)
            {
                DisplayCommands();
                Console.Write($"Please make a selection (1-{commands.Count}) or 0 to {backCommand}: ");
                var character = Console.ReadLine();
                if (int.TryParse(character.ToString(), out commandId) &&
                    (commandId < 0 || commandId > commands.Count))
                    continue;
                else if (commandId == 0)
                    return;

                commands[commandId - 1].Func.Invoke(this, null);
            }
        }

        public LibraryMenu(string parentName, Library library)
        {
            this.library = library;
            this.parentName = parentName;
            this.backCommand = $"Return to {parentName}";
            commands = RegesterCommands();
        }

        public List<Command> RegesterCommands()
        {
            List<Command> commands = new List<Command>();
            foreach (var method in this.GetType().GetMethods())
            {
                var attr = (Command)method.GetCustomAttributes(typeof(Command), true).FirstOrDefault();
                if (attr == null) continue;
                attr.Func = method;
                commands.Add(attr);
            }

            return commands;
        }
        public static void EnterToContinue()
        {
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }
    }
}
