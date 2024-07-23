public class ApiKey
{
    public string name { get; set; }
    public string value { get; set; }

    public ApiKey(string name, string value)
    {
        this.name = string.Empty;
        this.value = string.Empty;
    }
}