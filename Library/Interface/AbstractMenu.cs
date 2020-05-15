using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Program;

namespace UI
{
    // Add this attribute to any method in a LIbraryMenu class to make it a Menu item.
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

    /* Base Library menu class. Extend this class to create a menu. Each command
     * must be a function with a Command attribute. The name field of the atribute is
     * the name that will be displayed on the menu. The class must be initiated with 
     * the a library object and the name of the parent menu. */
    abstract public class AbstractMenu
    {
        protected Library library;
        protected string parentName;
        protected string name;
        protected string backCommand;
        List<Command> commands;

        public AbstractMenu(string parentName, Library library)
        {
            this.library = library;
            this.parentName = parentName;
            this.backCommand = $"Return to {parentName}";
            commands = RegesterCommands();
        }

        // Pads input string with '=' on both sides with total length length.
        // <example>
        // input: length=30, source="Hello World" 
        // returns: =========Hello World==========
        // </example>
        public string PadBoth(string source, int length)
        {
            int spaces = length - source.Length;
            int padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft, '=').PadRight(length, '=');
        }

        // Prints all registered commands to the console
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

        // Waits for user input and executes appropriate command
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

        // Executed when the object is constructed. Finds all methods with a Command attribute and
        // puts them into a list. This list is then checked whenever the user inputs a command.
        private List<Command> RegesterCommands()
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
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
