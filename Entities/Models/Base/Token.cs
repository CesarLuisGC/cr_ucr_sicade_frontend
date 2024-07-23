namespace Entities.Models.Base
{
    public class Token
    {
        #region Properties
        public string accessToken { get; set; }

        public DateTime expiration { get; set; }
        #endregion

        #region Constructor
        public Token()
        {
            accessToken = string.Empty;
            expiration = DateTime.MinValue;
        }
        #endregion
    }
}