namespace Cinema.DataAccess.Exceptions
{
    public class SaveFailedException : Exception
    {
        public SaveFailedException()
        {
            
        }

        public SaveFailedException(string msg, Exception ex) : base(msg, ex)
        {
            
        }
    }
}
