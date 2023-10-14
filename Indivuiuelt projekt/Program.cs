using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using System.Xml;

namespace Indivuiuelt_projekt

{
    internal class Program
    {
        static List<string> users = new List<string> { "anas", "tobias", "johanna", "chris", "ubbe" }; // användare
        static List<string> pins = new List<string> { "1234", "5678", "8912", "3456", "1337" }; // Pink-koder till användare
        static double[][] Konton = new double[][] // array med konton och summor
{
    new double[] {17459.48, 50000.34,2500},
    new double[] {1560.99, 1337.25},
    new double[] {400.34, 10000.78},
    new double[] {460.21, 210.11,4670},
    new double[] {350.56, 150.05}
};
        static int currentUserIndex = -1;
        static int loginAttempts = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till Eazy-Bank!"); 
            while (true) // huvud loop för programet
            {
                if (Login())
                {
                    while (true)
                    {
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
                                    TransferBetweenAccounts();
                                    break;

                                case 3:
                                    WithdrawMoney();
                                    break;

                                case 4:
                                    Logout();
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("Ogiltigt val.");
                                    break;
                            }
                        }
                        Console.WriteLine("Klicka på Enter för att komma till huvudmenyn.");
                        Console.ReadLine();
                        Console.Clear();
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
                string username = Console.ReadLine().ToLower();
                Console.Write("Ange PIN-kod: ");
                string pin = Console.ReadLine();
                Console.Clear();
                int userIndex = users.IndexOf(username); // programet skall hitta användaren i "users" och lagra i userindex

                if (userIndex >= 0 && userIndex < pins.Count && pins[userIndex] == pin) // kontroll av att användare och pim matchar
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
                        Console.WriteLine("Du har skrivit in fel pinkod tre gånger. Starta om programet");
                        Environment.Exit(0);
                    }
                }
            }
            return false;
        }

        static void ShowAccountsAndBalnace()
        {
            Console.Clear();
            Console.WriteLine("Dina konton och saldon: ");
            double[] userAccounts = Konton[currentUserIndex];
            string[] accountNames = new string[] { "1. Lönekonto", "2. Sparkonto", "3. Nöjeskonto" };

            for (int i = 0; i < userAccounts.Length; i++)
            {
                Console.WriteLine($"{accountNames[i]}: {userAccounts[i]:C}");
            }
        }


        static void TransferBetweenAccounts()
        {

            Console.WriteLine("Överföring mellan konton: ");
            ShowAccountsAndBalnace();
            Console.WriteLine("Välj konto efter siffra för att kunna överföra pengar");
            int fromAccount = int.Parse(Console.ReadLine()) - 1; // -1 gör så att man träffar rätt i index
            if (fromAccount < 0 || fromAccount >= Konton[currentUserIndex].Length) // kontroll av kontot om det är giltigt
            {
                Console.WriteLine("Ogiltigt kontoval");
                return;

            }
            Console.Write("Välj ett konto att flytta pengarna till: ");
            int toAccount = int.Parse(Console.ReadLine()) - 1; 
            if (toAccount < 0 || toAccount >= Konton[currentUserIndex].Length) // kontroll av kontot om det är giltigt
            {
                Console.WriteLine("Ogiltigt kontoval.");
                return;
            }
            Console.WriteLine("Ange summan du vill flytta");
            double amount = double.Parse(Console.ReadLine());
            if (amount > 0 && Konton[currentUserIndex][fromAccount] >= amount) // kontroll av summan att den är giltig
            {
                Console.Clear();
                Konton[currentUserIndex][fromAccount] -= amount; // beloppet dras av ifrån valt konto
                Konton[currentUserIndex][toAccount] += amount; // beloppet adderas till valt konto
                Console.WriteLine($"Överföringen är genomförd. Nytt saldo på konto {fromAccount + 1}: {Konton[currentUserIndex][fromAccount]:C}");
                Console.WriteLine($"Nytt saldo på konto {toAccount + 1}: {Konton[currentUserIndex][toAccount]:C}");

            }
            else
            {
                Console.WriteLine("Ogiltig summa eller otillräckligt saldo.");
            }

        }

        static void WithdrawMoney() // mer eller mindre samma som överföringen
        {
            Console.WriteLine("Ta ut pengar: ");
            ShowAccountsAndBalnace();
            Console.WriteLine("Väj ett konto efter siffra för att ta ut pengar ifrån ösnkat konto: ");
            int accountIndex = int.Parse(Console.ReadLine()) - 1;
            if (accountIndex < 0 || accountIndex >= Konton[currentUserIndex].Length)
            {
                Console.WriteLine("Ogilgitgt kontoval.");
                return;
            }
            Console.WriteLine("Ange summa att ta ut: ");
            double amount = double.Parse(Console.ReadLine());
            if(amount > 0 && Konton[currentUserIndex][accountIndex] >= amount)
            {
                Console.WriteLine("Ange PIN-kod för att bekräfta uttag: ");
                string pin = Console.ReadLine();
                if (pins[currentUserIndex] == pin)
                {
                    Console.Clear();
                    Konton[currentUserIndex][accountIndex] -= amount;
                    Console.WriteLine($"Uttag genomfört. Nytt saldo på kontot {accountIndex + 1}: {Konton[currentUserIndex][accountIndex]:C}");
                }
                else
                {
                    Console.WriteLine("Felaktig PIN-kod. Försök igen.");
                }
            }
            else
            {
                Console.WriteLine("Ogiltig summa eller otillräckligt saldo.");
            }
        }

        static void Logout() 
        {
            Console.Clear();
            Console.WriteLine($"Loggar ut användare {users[currentUserIndex]}");
            currentUserIndex = -1;
            Console.Clear();
            Login();
        }
    }
}
