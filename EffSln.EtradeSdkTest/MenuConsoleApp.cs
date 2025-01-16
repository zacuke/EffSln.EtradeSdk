using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffSln.EtradeSdkTest
{
    public abstract class MenuConsoleApp : IHostedService
    {

        public abstract Task StartAsync(CancellationToken cancellationToken);

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Generic method to handle menus dynamically using a dictionary of options.
        /// </summary>
        /// <param name="options">Dictionary of menu options where keys are choices, and values are (description, action).</param>
        /// <param name="menuTitle">Title to display at the top of the menu.</param>
        public void HandleMenu(Dictionary<int, (string Description, Action Action)> options, string menuTitle)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine(menuTitle);
                Console.WriteLine(new string('-', menuTitle.Length));

                // Display the menu
                foreach (var option in options)
                {
                    Console.WriteLine($"{option.Key}. {option.Value.Description}");
                }

                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && options.ContainsKey(choice))
                {
                    options[choice].Action.Invoke(); // Invoke the corresponding action
                    if (choice == 0)
                    {
                        running = false; // Exit the loop for "0" (Back/Exit)
                    }
                    else
                    {
                        // Pause for the user to see results
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Generic method to dynamically build menu options for any collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection items.</typeparam>
        /// <param name="items">The collection to build the menu from.</param>
        /// <param name="displaySelector">Function to select the display string for each item.</param>
        /// <param name="actionSelector">Function to define the action for each item.</param>
        /// <returns>A dictionary representing the menu options.</returns>
        public static Dictionary<int, (string Description, Action Action)> BuildMenuOptions<T>(
            IEnumerable<T> items,
            Func<T, string> displaySelector,
            Func<T, Action> actionSelector)
        {
            // Map each item to a key-value pair for the menu dictionary
            return items
                .Select((item, index) => new KeyValuePair<int, (string, Action)>(
                    index + 1, // Menu options are 1-based
                    (displaySelector(item) ?? throw new Exception("Display value unexpectedly null"), actionSelector(item))
                ))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
