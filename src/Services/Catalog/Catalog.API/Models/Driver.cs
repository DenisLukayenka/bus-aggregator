namespace Services.Catalog.API.Models;

public class Driver
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }

    public DateTime? BirthDate { get; set; }
}