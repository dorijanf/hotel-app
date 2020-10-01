using Newtonsoft.Json;

namespace HotelApp.API.Models
{
    public class ResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public int? EntityId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
