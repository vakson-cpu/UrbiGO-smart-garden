namespace Inf_Transfer.utils
{
   public class CustomResponse<T>
    {
        public string message { get; set; }
        public bool succeeded { get; set; }

        public T data { get; set; }
        public CustomResponse(string message, bool succeeded, T data)
        {
            this.message = message;
            this.succeeded = succeeded;
            this.data = data;
        }

    }
}