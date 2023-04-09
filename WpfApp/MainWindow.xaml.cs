using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();

        public MainWindow()
        {
            client.BaseAddress = new Uri("http://localhost:5274/customer/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
            LoadData();
        }

        private void clearData()
        {
            name_txt.Clear();
            age_txt.Clear();
            gender_txt.Clear();
            city_txt.Clear();
        }

        private void ClearDataBtn_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private async void LoadData()
        {
            var response = await client.GetStringAsync("");
            Console.WriteLine(response);
            var json = JsonConvert.DeserializeObject<string>(response);
            datagrid.Columns.Clear();
            List<Customer> all_date = JsonConvert.DeserializeObject<List<Customer>>(json);
            datagrid.ItemsSource = all_date;
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private Customer createCustomer()
        {
            Customer customer = new Customer();
            customer.Name = name_txt.Text;
            return customer;
        }

        private async void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            await client.PostAsJsonAsync("", createCustomer());
            LoadData();
        }

        private async void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(id_txt.Text, out id))
            {
                string url = string.Format("{0}", id);
                await client.PutAsJsonAsync(url, createCustomer());
            }

            LoadData();
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(id_txt.Text, out id))
            {
                string url = string.Format("{0}", id);
                await client.DeleteAsync(url);
            }

            LoadData();
        }
    }
}
