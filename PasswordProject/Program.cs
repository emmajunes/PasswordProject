namespace PasswordProject
{
    internal class Program
    {
        public static FileManager FileManager = new FileManager();

        static void Main(string[] args)
        {
        
            FileManager.CreateJson();

            var menuManager = new MenuManager(new User(),new LogIn());
            menuManager.StartMenu();
            
        }
    }
}