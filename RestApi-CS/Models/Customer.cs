namespace RestAPI.Models;

using Newtonsoft.Json;
using System.Text.Json;

public class Customer
{
    [JsonRequired]
    public string name { get; set; }
}