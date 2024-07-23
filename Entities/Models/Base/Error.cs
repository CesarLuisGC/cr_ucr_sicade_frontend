namespace Entities.Models.Base
{
    public class Error
    {
        public string date { get; set; }
        public string process { get; set; }
        public string origin { get; set; }
        public string message { get; set; }

        public Error()
        {
            this.date = string.Empty;
            this.process = string.Empty;
            this.origin = string.Empty;
            this.message = string.Empty;
        }
    }
}
