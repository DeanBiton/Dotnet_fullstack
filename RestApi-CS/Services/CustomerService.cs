using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;
using RestAPI.Models;
using Newtonsoft.Json;
using System.Formats.Asn1;
using System.Reflection;

namespace RestAPI.Services;

public class CustomerService
{
    SqlConnection con;
    private readonly HttpClient client;
    string serverName = "DESKTOP-5R5EJ4F";
    string databaseName = "Final";

    public CustomerService()
    {
        client = new HttpClient();
        con = new SqlConnection(string.Format("server={0}; database={1}; Integrated Security=True;", serverName, databaseName));
    }

    async public Task<DataTable> Get()
    {
        //JsonNode jsonNode = await useRandomUserApi("http://randomuser.me/api/?results=2&gender=male");
        //Console.WriteLine(jsonNode.ToString());
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int Post(JsonElement value)
    {
        Customer cus = JsonConvert.DeserializeObject<Customer>(value.ToString());
        string command = string.Format("Insert into Customer({0}) VALUES({1})", Customer.getFieldNames(), cus.getFieldsString());
        SqlCommand cmd = new SqlCommand(command, con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    public void Put(int id, JsonElement value)
    {
        Customer cus = JsonConvert.DeserializeObject<Customer>(value.ToString());
        SqlCommand cmd = new SqlCommand("UPDATE Customer SET Name = '" + cus.name + "', Birthday = '" + cus.birthday + "' WHERE ID = '" + id + "' ", con);
        con.Open();
        int querySuccess = cmd.ExecuteNonQuery();
        con.Close();
        if(querySuccess == 0)
            throw new Exception("ID doesn't exist");
    }

    public void Delete(int id)
    {
        SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE ID = '" + id + "' ", con);
        con.Open();
        int querySuccess = cmd.ExecuteNonQuery();
        con.Close();
        if (querySuccess == 0)
            throw new Exception("ID doesn't exist");
    }

    async private Task<JsonNode> useRandomUserApi(string url)
    {
        JsonNode? jsonNode = null;
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var jsonObject = await response.Content.ReadFromJsonAsync<JsonObject>();
        jsonObject?.TryGetPropertyValue("results", out jsonNode);
        if (jsonNode == null)
            throw new Exception(String.Format("Failed to get results from url: {0}", url));

        return jsonNode;
    }
}