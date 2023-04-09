using System;
using System.Data;
using System.Text.Json;

public class Customer
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Birthday { get; set; }

    public Customer()
    {
        Name = string.Empty;
    }
}