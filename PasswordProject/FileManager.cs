using System.Collections.Generic;
using System.Text.Json;

namespace PasswordProject
{
    public class FileManager
    {
        private static readonly string _currentDir = Environment.CurrentDirectory;
        private static readonly string _path = Directory.GetParent(_currentDir).Parent.Parent.FullName + @"\Users.json";
        public static List<User> Users = GetJson();

        public void CreateJson()
        {
            if (!File.Exists(_path))
            {
                using (var fs = File.Create(_path)) { }

                File.WriteAllText(_path, "[]");
            }
        }

        public static List<User> GetJson()
        {
            var jsonData = File.ReadAllText(_path);

            var lists = JsonSerializer.Deserialize<List<User>>(jsonData);

            return lists;
        }

        public static void UpdateJson()
        {
            var jsonData = JsonSerializer.Serialize(Users);

            File.WriteAllText(_path, jsonData);
        }
    }
}