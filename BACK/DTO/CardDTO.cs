namespace LetsCode.DTO;

public class CardDTO
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string List { get; set; }
}


public class CreateCardDTO
{
    public string? Title { get; set; }
    public string? Content { get; set; }
}
