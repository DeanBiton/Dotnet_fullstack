using System.Data;
using System.Text.Json;

public class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }

    public Customer()
    {
        Name = string.Empty;
    }
}