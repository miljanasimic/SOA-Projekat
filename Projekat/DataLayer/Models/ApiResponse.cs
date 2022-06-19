namespace DataLayer.Models
{
    public class ApiResponse<T>
    {
        public T Response { get; set; }

        public int Results{ get; set; }
    }
}
