namespace Library.Model;

public class Employee
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public int Age { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Position { get; set; }
    public int  DepartmentId { get; set; } 
    public string? DepartmentName { get; set; }
}
