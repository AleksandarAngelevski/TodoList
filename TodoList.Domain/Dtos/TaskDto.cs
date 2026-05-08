namespace Domain.Dtos;

public class TaskDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string UserId { get; set; }
    public bool Finished{ get; set; }
    public TaskDto(string userId, string name, string? description,bool finished = false)
    {
        Name = name;
        Description = description ?? "";
        UserId = userId;
        Finished = finished;

    }
}