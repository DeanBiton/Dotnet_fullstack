namespace RestAPI.Models;

using Newtonsoft.Json;
using System.Text.Json;

public class Customer
{
    [JsonRequired]
    public string name { get; set; }
    [JsonRequired]
    public string birthday { get; set; }

    static public string getFieldNames()
    {
        return "Name, Birthday";
    }

    public string getFieldsString()
    {
        return string.Format("'{0}', '{1}'", name, birthday);
    }
}