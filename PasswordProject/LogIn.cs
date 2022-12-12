namespace PasswordProject
{
    public class LogIn
    {
        
        public void LogInToSystem()
        {
            Console.WriteLine("Enter username: ");
            var userName = Console.ReadLine();

            foreach(var user in FileManager.Users)
            {
                if(user.UserName == userName)
                {
                    MenuManager.UserPosition = FileManager.Users.IndexOf(user);
                }
                
            }

            if(MenuManager.UserPosition == -1)
            {
                Console.WriteLine("User does not exist!");
            }

            Console.WriteLine("Enter password: ");
            var passwordInput = Console.ReadLine();

            var correctPassword = FileManager.Users[MenuManager.UserPosition].Password;

            if (passwordInput == correctPassword)
            {
                Console.WriteLine("Correct password!");
                MenuManager.UserSystemMenu();

            }
            else
            {
                Console.WriteLine("Wrong password. Try again!");
                MenuManager.UserPosition = -1;
                LogInToSystem();
                return;
            }

        }

    }
}