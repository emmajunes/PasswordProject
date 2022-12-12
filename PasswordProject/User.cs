using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace PasswordProject
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
   
        // promote? demote?

        public void CreateUser()
        {

            Console.WriteLine("Enter username: ");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            var newUser = new User()
            {
                UserName = userName,
                Email = email,
                Password = password,
            };

            FileManager.Users.Add(newUser);

            FileManager.UpdateJson();

           
            
        }

        public void AllUserNames()
        {
            Console.Clear();

            Console.WriteLine("\nOVERVIEW OF USERS: \n");
            var index = 1;
            foreach (var user in FileManager.Users)
            {
                Console.WriteLine($"[{index}] {user.UserName}");
                index++;
            }
        }
        public static void EditUser()
        {
            var selectedUser = FileManager.Users[MenuManager.UserPosition];

            Console.WriteLine("Write a new username: ");
            var newUsername = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(newUsername))
            {
                Console.WriteLine("List title cannot be empty");
                EditUser();
                return;
            }

            selectedUser.UserName = newUsername;

            FileManager.UpdateJson();
        }

    }
}