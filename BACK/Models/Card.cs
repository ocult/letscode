namespace LetsCode.Models;
public class Card : BaseEntity
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public KanbanListEnum List { get; private set; } = KanbanListEnum.TODO;

    public Card(string title, string content) : base()
    {
        Title = title;
        Content = content;
        Validate();
    }

    private Card(Guid id, string title, string content, KanbanListEnum list) : base(id)
    {
        Update(title, content, list);
    }

    public void Update(string title, string content, KanbanListEnum list)
    {
        Title = title;
        Content = content;
        List = list;
        Validate();
    }

    private void Validate()
    {
        if (String.IsNullOrWhiteSpace(Title) || String.IsNullOrWhiteSpace(Content))
        {
            throw new ArgumentNullException();
        }
    }
}
