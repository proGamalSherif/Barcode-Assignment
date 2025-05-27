namespace Assignment.Responses
{
    public class RequestResponses<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public static RequestResponses<T> Success(string message="Success",T data = default)
        {
            return new RequestResponses<T> { IsSuccess=true,  Message = message, Data = data };
        }
        public static RequestResponses<T> Failure(string message = "Fail", T data = default)
        {
            return new RequestResponses<T> { IsSuccess=false, Message = message, Data = data };
        }
    }
}
