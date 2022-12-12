using System.Security.Cryptography.X509Certificates;

namespace PasswordProject
{
    public class MenuManager
    {
        private readonly User user;
        private readonly LogIn logIn;
        public static int UserPosition = -1;

        public MenuManager(User user, LogIn logIn)
        {
            this.user = user;
            this.logIn = logIn;
        }
        public void StartMenu()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("MENU LOGIN");

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Registrate user");
                Console.WriteLine("[3] Quit program");

                Console.WriteLine("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        logIn.LogInToSystem();
                        break;
                    case "2":
                        user.CreateUser();
                        user.AllUserNames();
                        UserSystemMenu();
                        break;
                    case "3":
                        isRunning = Quit(isRunning);
                        break;
                    default:
                        break;

                }

            }
        }

        public static void UserSystemMenu()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU USERSYSTEM\n");

                Console.WriteLine("[1] Edit user");
                Console.WriteLine("[2] Delete user");
                Console.WriteLine("[3] Log out");

                Console.WriteLine("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": User.EditUser();
                        break;
                    case "2":
                        break;
                    case "3":
                        isRunning = LogOut(isRunning);
                        break;
                    default:
                        break;

                }

            }
        }
        public void UserSystemMenuAdmin()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("MENU USERSYSTEM");

                Console.WriteLine("[1] Create user");
                Console.WriteLine("[2] Edit user");
                Console.WriteLine("[3] View user");
                Console.WriteLine("[4] Log out");

                Console.WriteLine("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        user.CreateUser();
                        break;
                    case "3":
                        break;
                    default:
                        break;

                }

            }
        }

        public void UserMenu()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("MENU INDIVIDUAL USER");

                Console.WriteLine("[1] Promote user");
                Console.WriteLine("[2] Demote user");
                Console.WriteLine("[3] Delete user");
                Console.WriteLine("[4] Go back to Menu for user system");

                Console.WriteLine("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        user.CreateUser();
                        break;
                    case "3":
                        break;
                    default:
                        break;

                }

            }
        }

        public void UserMenuReadOnly()
        {

            //Info om valda användaren
        }

        public void EditMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("EDIT USER");

                Console.WriteLine("[1] Edit name");
                Console.WriteLine("[2] Edit password");


                Console.WriteLine("Select an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    default:
                        break;

                }

            }

        }

        public static bool Quit(bool isRunning)
        {
            Console.Write("\nDo you want to quit y/n? ");
            var exit = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(exit))
            {
                Console.WriteLine("Input cannot be empty");
                Quit(isRunning);
                return isRunning;
            }

            if (exit == "Y")
            {
                isRunning = false;
                return isRunning;
            }

            Console.Clear();

            return isRunning;
        }

        public static bool LogOut(bool isRunning)
        {
            Console.Write("\nDo you want to log out y/n? ");
            var input = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input cannot be empty");
                Quit(isRunning);
                return isRunning;
            }

            if (input == "Y")
            {
                isRunning = false;
                return isRunning;
            }

            Console.Clear();

            return isRunning;
        }
    }
}