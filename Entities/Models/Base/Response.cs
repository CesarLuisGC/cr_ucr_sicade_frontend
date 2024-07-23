namespace Entities.Models.Base
{
    public class Response
    {
        #region Properties
        public int state { get; set; }
        public string message { get; set; }
        public object? data { get; set; }
        #endregion

        #region Constructor
        public Response()
        {
            state = -1;
            message = string.Empty;
            data = null;
        }
        #endregion
    }
}