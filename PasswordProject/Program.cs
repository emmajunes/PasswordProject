namespace PasswordProject
{
    internal class Program
    {

        static void Main(string[] args)
        {

            FileManager.CreateJson(ApplicationConstants.UserPath);

            var menuManager = new MenuManager(new User());
            menuManager.StartMenu();

        }
    }
}