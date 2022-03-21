namespace LetsCode.DTO;

public class CardDTO
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Conteudo { get; set; }
    public string Lista { get; set; }
}


public class CreateCardDTO
{
    public string? Titulo { get; set; }
    public string? Conteudo { get; set; }
}
