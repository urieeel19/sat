namespace WebApplication1.BusinessLogic.Provider
{
    public interface IUserProvider
    {
        StreamReader ReadUsersFromFile();
    }
    public class UserProvider : IUserProvider
    {
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
