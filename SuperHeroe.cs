namespace DapperCRUD;

public class SuperHeroe
{
    public int id { get; set; }

    public string heroeName { get; set; } = string.Empty;

    public string firstName { get; set; } = string.Empty;

    public string lastName { get; set; } = string.Empty;

    public string city { get; set; } = string.Empty;

    public string registrationDate { get; set; } = DateTime.UtcNow.ToString();

}
