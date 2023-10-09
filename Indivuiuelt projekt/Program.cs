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
    new double[] {17000, 50000},
    new double[] {1500, 700},
    new double[] {400, 10000, 20},
    new double[] {460, 210},
    new double[] {350, 150}
};
        static int currentUserIndex = -1;
        static int loginAttempts = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Välkomen till Eazy-Bank!");
            while (true)
            {
                if (Login())
                {
                    while (true)
                    {
                        Console.WriteLine("Välkomen till ditt konto.");
                        Console.WriteLine("\nVälj vad du vill göra");
                        Console.WriteLine("1. Se över konton & saldo");
                        Console.WriteLine("2. Överföring mellan konton");
                        Console.WriteLine("3. Ta ut pengar");
                        Console.WriteLine("4. Logga ut");
                        int choice;
                        if (int.TryParse(Console.ReadLine(), out choice))
                        {
                            switch (choice)
                            {
                                case 1:
                                    ShowAccountsAndBalnace();
                                    break;
                                case 2:
                                    //TransferBetweenAccounts();
                                    break;
                                case 3:
                                    //WithdrawMoney();
                                    break;
                                case 4:
                                    //LOgout();
                                    break;
                                default:
                                    Console.WriteLine("Ogiltigt val.");
                                    break;
                                    {
                                        Console.WriteLine("Tryck på enter för att komma till huvudmenyn.");
                                        Console.ReadLine();
                                    }
                            }
                        }
                    }
                }
            }
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

        static void ShowAccountsAndBalnace()
        {
            Console.WriteLine("Dina konton och saldon: ");
            double[] userAccounts = Konton[currentUserIndex];
            for (int i= 0; i < userAccounts.Length; i++)
            {
                Console.WriteLine($"Konto {i +1}: {userAccounts[i]}");
            }
        }

    }


}
