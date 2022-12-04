namespace WebApplication1.Application
{
    public class Response<T>
    {
        public T Data
        {
            get;
            set;
        }

        public string Result
        {
            get;
            set;
        }

        public IList<string> Errors
        {
            get;
            set;
        }

        public bool Succeeded
        {
            get;
            set;
        }
    }
}
