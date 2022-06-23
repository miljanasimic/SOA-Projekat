namespace DataLayer.DTOs
{
    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T ResponseContent { get; set; }

        public string ErrorMessage { get; set; }
    }
}
