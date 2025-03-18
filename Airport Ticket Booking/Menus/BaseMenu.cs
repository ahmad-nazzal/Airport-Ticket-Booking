using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Menus
{
    public abstract class BaseMenu
    {
        protected void DisplayOptions(string title, string[] options)
        {
            Console.Clear();
            Console.WriteLine($"===== {title} =====");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.Write("Select an option: ");
        }

        protected int GetUserChoice(int maxOption)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= maxOption)
                {
                    return choice;
                }
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        protected T SelectFromList<T>(string title, List<T> items)
        {
            if (items == null || items.Count == 0)
            {
                Console.WriteLine($"No {title.ToLower()} available.");
                Console.ReadKey();
                return default;
            }

            Console.Clear();
            Console.WriteLine($"===== Select a {title} =====");

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i]}");
            }

            Console.Write("Select an option (or press Enter to skip): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return default; // Allows skipping

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= items.Count)
            {
                return items[choice - 1];
            }

            Console.WriteLine("Invalid choice. Please enter a valid number.");
            Console.ReadKey();
            return default;
        }
    }
    
}

