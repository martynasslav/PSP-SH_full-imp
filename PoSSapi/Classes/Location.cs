namespace Classes;

public class Location
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Client> Clients { get; set; }

    static public Location GenerateRandom(string? id=null)
    {
        var location = new Location();
        location.Id = id ?? Guid.NewGuid().ToString();
        location.Name = "name";
        location.Address = "address";
        location.Clients = new List<Client>();
        return location;
    }
}
