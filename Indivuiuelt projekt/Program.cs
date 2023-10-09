using System.Net.NetworkInformation;
using System.Xml;

namespace Indivuiuelt_projekt
    
{
    internal class Program
    {
        static List<string> users = new List<string> { "Anas", "Tobias", "Johanna", "Chris", "Ubbe" };
        static List<string> pins = new List<string> { "1234", "5678", "8912", "3456", "1337" };
        static List<double[]> Konton = new List<double[]>
{
    new double[] {100.00, 500.30},
    new double[] {1500.00, 700},
    new double[] {400.00, 10000, 20},
    new double[] {460.00, 210},
    new double[] {350, 150}
};
        static int currentUserIndex = -1;
        static int loginAttempts = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Välkomen till Eazy-Bank!");

            Program.Login();
           


        }


        static bool Login()
        {
            int maxLoginAttempts = 3;
            int loginAttempts = 0;

            while (loginAttempts < maxLoginAttempts)
            {
                Console.Write("Ange användarnamn: ");
                string username = Console.ReadLine();
                Console.Write("Ange PIN-kod: ");
                string pin = Console.ReadLine();

                int userIndex = users.IndexOf(username);

                if (userIndex >= 0 && userIndex < pins.Count && pins[userIndex] == pin)
                {
                    currentUserIndex = userIndex;
                    Console.WriteLine($"Inloggningen lyckades för användare {username}.");
                    return true;
                }
                else
                {
                    loginAttempts++;
                    Console.WriteLine("Inloggningen misslyckades. Försök igen.");
                    if (loginAttempts >= maxLoginAttempts)
                    {
                        Console.WriteLine("Du har skrivit in fel pinkod tre gånger. Programmet startas om.");
                        Environment.Exit(0);
                    }
                }
            }
            return false;
        }



    }


}
