using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Nodes;
using RestAPI.Models;
using Newtonsoft.Json;

namespace RestAPI.Services;

public class CustomerService
{
    SqlConnection con;
    
    public CustomerService()
    {
        con = new SqlConnection("server=DESKTOP-5R5EJ4F; database=Final; Integrated Security=True;");
    }

    async public Task<DataTable> Get()
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Customer", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    async public Task<int> Post(JsonElement value)
    {
        Customer cus = JsonConvert.DeserializeObject<Customer>(value.ToString());
        SqlCommand cmd = new SqlCommand("Insert into Customer(Name) VALUES('" + cus.name + "')", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    async public Task<int> Put(int id, JsonElement value)
    {
        Customer cus = JsonConvert.DeserializeObject<Customer>(value.ToString());
        SqlCommand cmd = new SqlCommand("UPDATE Customer SET Name = '" + cus.name + "' WHERE ID = '" + id + "' ", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }

    async public Task<int> Delete(int id)
    {
        SqlCommand cmd = new SqlCommand("DELETE FROM Customer WHERE ID = '" + id + "' ", con);
        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();
        return i;
    }
}