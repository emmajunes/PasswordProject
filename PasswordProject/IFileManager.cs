namespace PasswordProject
{
    public interface IFileManager
    { //skapa på detta sätt??
        string CurrentDir { get; set; }
        string Path { get; set; }
        List<User> Users { get; set; }

        void CreateJson();
        void GetJson();
        void UpdateJson();
    }
}