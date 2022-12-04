namespace WebApplication1.DAL.Domain
{
    public abstract class UserType
    {
        public string Id { get; set; }
        public abstract void SetMoney(User user);
    }
}
