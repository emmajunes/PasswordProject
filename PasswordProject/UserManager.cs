﻿using System.Net.Mail;

namespace PasswordProject
{
    public class UserManager
    {
        public List<User> Users { get; set; } = new List<User>();
        private static string _path = ApplicationConstants.UserPath;

        public void GetUsers()
        {
            Users = FileManager.GetJson<User>(_path);

        }
        public void CreateUser()
        {
            GetUsers();

            Console.WriteLine("\nEnter username: ");
            string username = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty. Try again!");
                CreateUser();
                return;
            }

            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();

            var isValidEmail = ValidateEmail(email);

            if (!isValidEmail)
            {
                Console.WriteLine("Invalid email!");
                CreateUser();
                return;
            }

            Console.WriteLine("Enter a strong password: ");
            string password = HidePassword();

            var isValidPassword = ValidatePassword(password);

            if (!isValidPassword)
            {
                Console.WriteLine("Invalid email! Choose a password that includes specialcharacter and atleast one number and one uppercase character.");
                CreateUser();
                return;
            }

            var newUser = new User()
            {
                Username = username,
                Email = email,
                Password = password,
                Access = "User"
            };


            Users.Add(newUser);

            MenuManager.UserPosition = Users.IndexOf(newUser);

            FileManager.UpdateJson(_path, Users);

        }

        public bool ValidateEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            foreach (var user in Users)
            {
                if (user.Email == email)
                {
                    Console.WriteLine("Mail already exists!");
                    return false;
                }

            }

            try
            {
                MailAddress m = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool ValidatePassword(string passWord)
        {
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=', '!', '/', '?', '*', '-', '[', ']', '"', '(', ')', '{', '}', '~', '¤' };
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }

        public string HidePassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }

        public void GetAllUsernames()
        {
            GetUsers();
            Console.Clear();

            Console.WriteLine("\nOVERVIEW OF USERS: \n");
            var index = 1;
            foreach (var user in Users)
            {
                Console.WriteLine($"[{index}] {user.Username}");
                index++;
            }
        }

        public void SelectUser()
        {
            Console.WriteLine("\nSelect user: ");
            int input = Convert.ToInt32(Console.ReadLine());

            MenuManager.UserPosition = Convert.ToInt32(input - 1);
        }

        public void GetIndividualUser()
        {
            Console.Clear();

            if (MenuManager.UserPosition < 0)
            {
                Console.WriteLine("\nINFO ABOUT USER: \n");
                Console.WriteLine("USERNAME: " + Users[MenuManager.LoggedInUserPosition].Username);
                Console.WriteLine("EMAIL: " + Users[MenuManager.LoggedInUserPosition].Email);
                Console.WriteLine("PASSWORD: " + Users[MenuManager.LoggedInUserPosition].Password);
                Console.WriteLine("ACCESS: " + Users[MenuManager.LoggedInUserPosition].Access);
            }
            if (MenuManager.UserPosition >= 0)
            {
                Console.WriteLine("\nINFO ABOUT USER: \n");
                Console.WriteLine("USERNAME: " + Users[MenuManager.UserPosition].Username);
                Console.WriteLine("EMAIL: " + Users[MenuManager.UserPosition].Email);
                Console.WriteLine("PASSWORD: " + Users[MenuManager.UserPosition].Password);
                Console.WriteLine("ACCESS: " + Users[MenuManager.UserPosition].Access);
            }


        }

        public void DeleteUser()
        {
            //var selectedUser = Users[MenuManager.UserPosition];

            Console.WriteLine("Do you want to delete this user y/n? ");
            var deleteAnswer = Console.ReadLine().ToUpper();

            if (String.IsNullOrWhiteSpace(deleteAnswer))
            {
                Console.WriteLine("Input field cannot be empty");
                DeleteUser();
                return;
            }

            if (deleteAnswer == "Y")
            {
                Console.Clear();
               
                if (MenuManager.UserPosition < 0)
                {
                    Users.RemoveAt(MenuManager.LoggedInUserPosition);
                    FileManager.UpdateJson(_path, Users);
                    Console.WriteLine("You deleted your account.");
                    Thread.Sleep(1500);
                    var menu = new MenuManager(new User());
                    menu.StartMenu();

                    return;
                }
                if (MenuManager.UserPosition >= 0)
                {
                    Console.WriteLine("Deleted user: " + Users[MenuManager.UserPosition].Username);
                    Users.RemoveAt(MenuManager.UserPosition);
                    FileManager.UpdateJson(_path, Users);

                    return;
                }

            }

            else if (deleteAnswer == "N")
            {
                return;
            }

            else
            {
                Console.WriteLine("Answer needs to be a letter of y or n");
                DeleteUser();
                return;
            }
        }

        public void PromoteUserAdmin()
        {
            var selectedUser = Users[MenuManager.UserPosition];

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nPromote user to: \n");
                Console.WriteLine("[1] Admin");
                Console.WriteLine("[2] Moderator");
                Console.WriteLine("[3] Go back to menu");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        selectedUser.Access = "Admin";
                        Console.WriteLine("Selected user: " + selectedUser.Access);
                        FileManager.UpdateJson(_path, Users);
                        Console.Clear();
                        GetIndividualUser();
                        Console.WriteLine(selectedUser.Username + " is promoted to admin");
                        return;
                    case "2":
                        selectedUser.Access = "Moderator";
                        FileManager.UpdateJson(_path, Users);
                        Console.Clear();
                        GetIndividualUser();
                        Console.WriteLine(selectedUser.Username + " is promoted to moderator");
                        return;
                    case "3":
                        Console.Clear();
                        var menu = new MenuManager(new User());
                        menu.UserMenu();
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        PromoteUserAdmin();
                        return;


                }

            }

        }

        public void PromoteUserModerator()
        {
            var selectedUser = Users[MenuManager.UserPosition];

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nPromote user to: \n");
                Console.WriteLine("[1] Moderator");
                Console.WriteLine("[2] Go back to menu");
                var input = Console.ReadLine();

                switch (input)
                {

                    case "1":
                        selectedUser.Access = "Moderator";
                        FileManager.UpdateJson(_path, Users);
                        Console.Clear();
                        GetIndividualUser();
                        Console.WriteLine(selectedUser.Username + " is promoted to moderator");
                        return;
                    case "2":
                        Console.Clear();
                        var menu = new MenuManager(new User());
                        menu.UserSystemMenuModerator();
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        PromoteUserModerator();
                        return;

                }

            }

        }

        public void DemoteUser()
        {
            var selectedUser = Users[MenuManager.UserPosition];

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nDemote user to: \n");
                Console.WriteLine("[1] User");
                Console.WriteLine("[2] Moderator");
                Console.WriteLine("[3] Go back to user menu");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        selectedUser.Access = "User";
                        FileManager.UpdateJson(_path, Users);
                        Console.Clear();
                        GetIndividualUser();
                        Console.WriteLine(selectedUser.Username + " is demoted to user");
                        return;
                    case "2":
                        selectedUser.Access = "Moderator";
                        FileManager.UpdateJson(_path, Users);
                        Console.Clear();
                        GetIndividualUser();
                        Console.WriteLine(selectedUser.Username + " is demoted to moderator");
                        return;
                    case "3":
                        Console.Clear();
                        var menu = new MenuManager(new User());
                        menu.UserMenu();
                        break;
                    default:
                        Console.WriteLine("There is no option recognized to your input. Try again!");
                        DemoteUser();
                        return;


                }

            }

            FileManager.UpdateJson(_path, Users);

        }
        public void EditUserName()
        {

            Console.WriteLine("Write a new username: ");
            var newUsername = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newUsername))
            {
                Console.WriteLine("List title cannot be empty");
                EditUserName();
                return;
            }
            if (MenuManager.UserPosition < 0)
            {
                var loggedInUser = Users[MenuManager.LoggedInUserPosition];
                loggedInUser.Username = newUsername;
            }
            if (MenuManager.UserPosition >= 0)
            {
                var selectedUser = Users[MenuManager.UserPosition];
                selectedUser.Username = newUsername;

            }


            FileManager.UpdateJson(_path, Users);
        }

        public void EditPassword()
        {

            Console.WriteLine("Write a new password: ");
            var newPassword = HidePassword();

            var isValidPassword = ValidatePassword(newPassword);

            if (!isValidPassword)
            {
                Console.WriteLine("Invalid email! Choose a password that includes specialcharacter and atleast one number and one uppercase character.");
                CreateUser();
                return;
            }

            if (MenuManager.UserPosition < 0)
            {
                var loggedInUser = Users[MenuManager.LoggedInUserPosition];
                loggedInUser.Password = newPassword;
            }
            if (MenuManager.UserPosition >= 0)
            {
                var selectedUser = Users[MenuManager.UserPosition];
                selectedUser.Password = newPassword;

            }

            FileManager.UpdateJson(_path, Users);
        }

        public void LogInToSystem()
        {
            Console.WriteLine("\nEnter username: ");
            var login = Console.ReadLine();

            foreach (var user in Users)
            {
                if (user.Username == login)
                {
                    MenuManager.LoggedInUserPosition = Users.IndexOf(user);
                }

            }

            if (MenuManager.LoggedInUserPosition == -1)
            {
                Console.WriteLine("User does not exist!");
                


            }

            Console.WriteLine("Enter password: ");
            var passwordInput = HidePassword();

            if (String.IsNullOrWhiteSpace(passwordInput))
            {
                Console.WriteLine("Password field cannot be empty");
                
            }
            if (Users[MenuManager.LoggedInUserPosition].Password != passwordInput)
            {
                Console.WriteLine("Wrong password. Try again!");
                MenuManager.LoggedInUserPosition = -1;
                
            }
            var menu = new MenuManager(new User());

            if (Users[MenuManager.LoggedInUserPosition].Password == passwordInput)
            {
                Console.WriteLine("Logging in...");
                Thread.Sleep(2000);
                if (Users[MenuManager.LoggedInUserPosition].Access == "Admin")
                {
                    menu.UserSystemMenuAdmin();
                }
                else if (Users[MenuManager.LoggedInUserPosition].Access == "Moderator")
                {
                    menu.UserSystemMenuModerator();
                }
                else
                {
                    menu.UserSystemMenu();
                }

            }
           

            //var correctPassword = Users[MenuManager.LoggedInUserPosition].Password;
            //return passwordInput == correctPassword;

        }
    }


}