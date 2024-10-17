namespace Route.Talabat.Core.Application.Exception
{
    public class NotfoundException :ApplicationException
    {

        public NotfoundException(string name,object key)
            :base($"{name} With {key} is Not Found")
        {
            

        }
    }
}
