using System.Security.Cryptography.X509Certificates;

namespace PasswordProject
{

    public class MenuManager
    {
        private readonly User _user;
        public static int UserPosition = -1;
        public static int LoggedInUserPosition = -1;        
        private UserManager _userManager;

        public MenuManager(User user)
        {
            this._user = user;
            this._userManager = new UserManager();
        }
        public void StartMenu()
        {
            _userManager.GetUsers();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU LOGIN\n");

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Create user");
                Console.WriteLine("[3] Quit program");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.LogInToSystem();
                        break;
                    case "2":
                        _userManager.CreateUser();
                        Console.Clear();
                        Console.WriteLine("You created your account. Log in with your details.");
                        StartMenu();
                        break;
                    case "3":
                        isRunning = Quit(isRunning);
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        StartMenu();
                        return;
                        

                }

            }
        }

        public void UserSystemMenu()
        {
            Console.Clear();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU USERSYSTEM\n");

                Console.WriteLine("[1] Edit user");
                Console.WriteLine("[2] Delete user");
                Console.WriteLine("[3] Log out");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.GetIndividualUser();
                        EditMenu();
                        break;
                    case "2":
                        _userManager.GetIndividualUser();
                        _userManager.DeleteUser();
                        break;
                    case "3":
                        isRunning = LogOut(isRunning);
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        UserSystemMenu();
                        return;
                       

                }

            }
        }

        public void UserSystemMenuModerator()
        {
            Console.Clear();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU USERSYSTEM FOR MODERATOR\n");
        
                Console.WriteLine("[1] View all users");
                Console.WriteLine("[2] Log out");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.GetAllUsernames();
                        _userManager.SelectUser();
                        _userManager.GetIndividualUser();
                        _userManager.PromoteUserModerator();
                        break;
                    case "2":
                        isRunning = LogOut(isRunning);
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        UserSystemMenuAdmin();
                        return;


                }

            }
        }
        public void UserSystemMenuAdmin()
        {
            Console.Clear();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU USERSYSTEM FOR ADMIN\n");

                Console.WriteLine("[1] Create user");
                Console.WriteLine("[2] View all users");
                Console.WriteLine("[3] Log out");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.CreateUser();
                        Console.Clear();
                        Console.WriteLine("New created user: " + _userManager.Users[UserPosition].Username);
                        break;
                    case "2":
                        _userManager.GetAllUsernames();
                        _userManager.SelectUser();
                        _userManager.GetIndividualUser();
                        UserMenu();
                        break;
                    case "3":
                        isRunning = LogOut(isRunning);
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        UserSystemMenuAdmin();
                        return;
                        

                }

            }
        }

        public void UserMenu()
        {

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nMENU INDIVIDUAL USER\n");

                Console.WriteLine("[1] Edit user");
                Console.WriteLine("[2] Promote user");
                Console.WriteLine("[3] Demote user");
                Console.WriteLine("[4] Delete user");
                Console.WriteLine("[5] Go back to Menu for user system");

                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.GetIndividualUser();
                        EditMenu();
                        break;
                    case "2": _userManager.PromoteUserAdmin();
                        break;
                    case "3": _userManager.DemoteUser();
                        break;
                    case "4":
                        if (_userManager.Users[LoggedInUserPosition].Access == "Admin")
                        {
                            _userManager.DeleteUser();
                        }
                        else
                        {
                            Console.WriteLine("Only admin have access to delete a user");
                        }
                        break;
                    case "5":
                        UserSystemMenuAdmin();
                        return;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        UserMenu();
                        return;
                        

                }

            }
        }


        public void EditMenu()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nEDIT USER\n");

                Console.WriteLine("[1] Edit name");
                Console.WriteLine("[2] Edit password");
                Console.WriteLine("[3] Go back to user menu");


                Console.Write("\nSelect an option: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _userManager.EditUserName();
                        Console.Clear();
                        _userManager.GetIndividualUser();
                        break;
                    case "2":
                        _userManager.EditPassword();
                        Console.Clear();
                        _userManager.GetIndividualUser();
                        break;
                    case "3":
                        if (_userManager.Users[LoggedInUserPosition].Access == "Admin")
                        {
                            _userManager.GetAllUsernames();
                            UserSystemMenuAdmin();
                        }
                        else
                        {
                            _userManager.GetIndividualUser();
                            UserSystemMenu();
                        }
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        EditMenu();
                        return;

                }

            }

        }



        public bool Quit(bool isRunning)
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

        public bool LogOut(bool isRunning)
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
                Console.Clear();
                LoggedInUserPosition = -1;
                isRunning = false;
                return isRunning;
            }

            Console.Clear();

            return isRunning;
        }
    }
}