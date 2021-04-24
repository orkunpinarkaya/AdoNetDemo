using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=ETrade;integrated security=true");
        public  List<Product> GelAll()
        {
            ConnectionControl();
          

            SqlCommand command = new SqlCommand("select * from Products",_connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount=Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice=Convert.ToDecimal(reader["UnitPrice"])
                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;
          
        }
        //public DataTable GelAll2()
        //{
        //    SqlConnection connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;initial catalog=ETrade;integrated security=true");
        //    if (connection.State == ConnectionState.Closed)
        //    {
        //        connection.Open();
        //    }

        //    SqlCommand command = new SqlCommand("select * from Products", connection);

        //    SqlDataReader reader = command.ExecuteReader();

        //    DataTable datatable = new DataTable();
        //    datatable.Load(reader);
        //    reader.Close();
        //    connection.Close();
        //    return datatable;

        //}
        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand _command = new SqlCommand("insert into Products values(@name,@unitPrice,@stockAmount)",_connection);
            _command.Parameters.AddWithValue("@name", product.Name);
            _command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            _command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            _command.ExecuteNonQuery();

            _connection.Close();

        }
        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand _command = new SqlCommand("update Products set Name=@name,UnitPrice=@unitPrice,StockAmount=@stockAmount where Id=@id", _connection);
            _command.Parameters.AddWithValue("@name", product.Name);
            _command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            _command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            _command.Parameters.AddWithValue("@id", product.Id);
            _command.ExecuteNonQuery();

            _connection.Close();

        }
        public void Delete(int id)
        {
            ConnectionControl();
            SqlCommand _command = new SqlCommand("delete from products where Id=@id", _connection);
           
            _command.Parameters.AddWithValue("@id", id);
            _command.ExecuteNonQuery();

            _connection.Close();

        }
    }
}
