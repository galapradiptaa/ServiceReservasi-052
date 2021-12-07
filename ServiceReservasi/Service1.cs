using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;

namespace ServiceReservasi
{
    public class Service
    {
        string constring = "Data Source=ASUS-FOS9A28D;Initial Catalog=WCFReservasi;Persist Security Info=True;User ID=sa;Password=123";
        SqlConnection connection;
        SqlCommand com;
    }

    public List <DetailLokasi> DetailLokasi()
    {
        List <DetailLokasi> LokasiFull = new List<DetailLokasi>();
        try
        {
            string sql = "select ID_lokasi, Nama_lokasi, Deskripsi_full, Kuota from dbo.Lokasi";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                DetailLokasi data = new DetailLokasi();

                data.IDLokasi = reader.GetString(0);
                data.NamaLokasi = reader.GetString(1);
                data.DeskripsiFull = reader.GetString(2);
                data.Kuota = reader.GetInt32(3);
                LokasiFull.Add(data); //mengumpulkan data dari array
            }
            connection.Close(); //untuk menutup akses ke database   
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return LokasiFull;
    }

    public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
    {
        string a = "gagal";
        try
        {
            string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + "', " + JumlahPemesanan + ", '" + IDLokasi + "')";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            com.ExecuteNonQuery();
            connection.Close();
            a = "sukses";
        }
        catch (Exception es)
        {
            Console.WriteLine (es);
        }
        return a;
    }
}
