namespace DataLayer.DTOs
{
    public class GatewayResponse<T, V>
    {
        public T ApiReponse { get; set; }

        public V ServiceResponse { get; set; }

    }
}
