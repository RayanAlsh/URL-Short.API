namespace URL_Short.Web;

public class ShortUrlRequestDTO
{
    public string Default_URL { get; set; }
    public Guid UserId { get; set; } // meaning when it's present we can store the data in the user , else then shorten anonymously
}
