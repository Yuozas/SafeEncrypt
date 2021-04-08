using System;
using System.Collections.Generic;
using System.Linq;

namespace SafeEncrypt
{
    public class ConsoleHandler
    {
        private readonly List<MenuOption> menuOptions;
        private int selectedOption = 0;

        public ConsoleHandler()
        {
            menuOptions = new List<MenuOption>();
            StartMenu();
        }

        private void StartMenu()
        {
            AddMenuOption("SHA1", 1, Hasher.Instance.GetHashedSHA1);
            AddMenuOption("SHA256", 2, Hasher.Instance.GetHashedSHA256);
            AddMenuOption("SHA384", 3, Hasher.Instance.GetHashedSHA384);
            AddMenuOption("SHA384", 4, Hasher.Instance.GetHashedSHA384);
            AddMenuOption("SHA512", 5, Hasher.Instance.GetHashedSHA512);
            AddMenuOption("MD5", 6, Hasher.Instance.GetHashedMD5);
            AddMenuOption("HMAC", 7, Hasher.Instance.GetHashedHMAC);
        }

        public void AddMenuOption(string name, int code, Func<string, string> hashedAction)
        {
            menuOptions.Add(
                new MenuOption(
                    name,
                    code,
                    hashedAction
                )
            );
        }

        public void DoAction()
        {
            if (selectedOption == 0)
                RunMenu();
            else
                RunAction();
        }

        private void RunAction()
        {
            DisplayAction();
            Console.Write("Text to hash entered: ");
            Console.WriteLine($"You're hash: {menuOptions[selectedOption - 1].HashedAction.Invoke(Console.ReadLine())}\n");
            RunRepeatAction();
        }

        private void DisplayAction()
        {
            Console.Clear();
            Console.WriteLine($"Hashing method selected: {menuOptions[selectedOption - 1].Name}");
            Console.WriteLine("Please enter text to hash...\n");
        }

        private void RunRepeatAction()
        {
            DisplayRepeatAction();

            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "y" || input == "n") break;
                DisplayRepeatAction(input);
            }
            if (input == "n") selectedOption = 0;
            DoAction();
        }

        private void DisplayRepeatAction(string erroMessage = "")
        {
            if (erroMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"Invalid input: '{erroMessage}'");
            }
            Console.WriteLine($"Do you wish to hash more text with: {menuOptions[selectedOption - 1].Name} ?");
            Console.WriteLine("y/n\n");
        }

        private void RunMenu()
        {
            int option = 0;

            DisplayMenu();

            string input;
            while (option == 0)
            {
                Console.Write("Option selected: ");
                input = Console.ReadLine();
                try
                {
                    option = int.Parse(input);
                    if (!option.InRange(menuOptions.First().Code, menuOptions.Last().Code))
                    {
                        option = 0;
                        throw new Exception();
                    }
                }
                catch
                {
                    DisplayMenu(errorMessage: input);
                    continue;
                }
            }
            selectedOption = option;
            DoAction();
        }

        private void DisplayMenu(string errorMessage = "")
        {
            Console.Clear();
            Console.WriteLine("================================SafeEncrypt================================");
            Console.WriteLine("Please select encryption method:\n");
            for (int i = 0; i < menuOptions.Count; i++)
                Console.WriteLine($"{menuOptions[i].Code}:{menuOptions[i].Name}");
            if (errorMessage != "")
                Console.WriteLine($"\nNo such option: '{errorMessage}'. please retry...");
            Console.WriteLine("\nTo select encription method write down the number...");
        }
    }
}