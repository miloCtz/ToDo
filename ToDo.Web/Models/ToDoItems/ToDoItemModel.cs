namespace ToDo.Web.Models.ToDoItems;

public class ToDoItemModel
{
    public ToDoItemModel(int id, string title, bool isDone = false)
    {
        Id = id;
        Title = title;
        IsDone = isDone;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsDone { get; set; }
    public bool IsEditing { get; set; }

    public void Edit() => IsEditing = true;
    public void CompleteEdit() => IsEditing = false;
    public void Toggle() => IsDone = !IsDone;
}