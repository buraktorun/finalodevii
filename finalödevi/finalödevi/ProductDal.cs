using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace finalödevi
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DPProduct;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public List<product> UrunleriGetir()
        {
            BaglantiKontrol();
            SqlCommand command = new SqlCommand("select * from Products", _connection);
            SqlDataReader reader = command.ExecuteReader();
            List<product> products = new List<product>();
            while (reader.Read())
            {
                product product = new product()
                {
                    Id = (int)reader[0],
                    UrunAd = reader[1].ToString(),
                    UrunFıyat = Convert.ToDecimal(reader[2]),
                    UrunMıktar = Convert.ToInt32(reader[3]),
                    Umarka = reader[4].ToString()
                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;

        }
        public void Ekle(product product)
        {
            BaglantiKontrol();
            SqlCommand command = new SqlCommand("insert into Products values(@urunad,@urunfıyat,@urunmıktar,@urunmarka)", _connection);
            command.Parameters.AddWithValue("@urunad", product.UrunAd);
            command.Parameters.AddWithValue("@urunfıyat", product.UrunFıyat);
            command.Parameters.AddWithValue("@urunmıktar", product.UrunMıktar);
            command.Parameters.AddWithValue("@urunmarka", product.Umarka);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Guncelle(product product)
        {
            BaglantiKontrol();
            SqlCommand command = new SqlCommand("update Products set UrunAd=@urunad,UrunFıyat=@urunfıyat,UrunMıktar=@urunmıktar,Umarka=@urunmarka where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@urunad", product.UrunAd);
            command.Parameters.AddWithValue("@urunfıyat", product.UrunFıyat);
            command.Parameters.AddWithValue("@urunmıktar", product.UrunMıktar);
            command.Parameters.AddWithValue("@urunmarka", product.Umarka);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        public void Sil(int id)
        {
            BaglantiKontrol();
            SqlCommand command = new SqlCommand("delete from Products where Id=@id", _connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            _connection.Close();
        }

        private void BaglantiKontrol()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
    }
}
