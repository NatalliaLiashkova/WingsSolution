using Newtonsoft.Json;

namespace WingsAPI.Models
{
    public class ErrorDetails
    {
        #region Properties
        public int StatusCode { get; set; }
        public string Message { get; set; }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
