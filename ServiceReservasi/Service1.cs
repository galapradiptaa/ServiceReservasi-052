using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ServiceReservasi
{
    [ServiceContract]
    public interface Service1
    {
        string constring = "DataSource=ASUS-FOS9A28D; InitialCatalog=WCFReservasi; PersistSecurityInfo=True; UserID=sa; Password=123";
        SqlConnection connection;
        SqlCommand com;
    }

    public List<DetailLokasi> DetailLokasi()
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

    public List<Pemesanan> Pemesanan()
    {
        List<Pemesanan> pemesanans = new List<Pemesanan>();
        try
        {
            string sql = "select ID_reservasi, Nama_customer, No_telpon, " +
                "Jumlah_pemesanan, Nama_Lokasi from dbo.Pemesanan p join dbo.Lokasi 1 on p.ID_lokasi = 1.ID_lokasi";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = com.ExecuteReader();
            while (reader.Read())
            {
                Pemesanan data = new Pemesanan();
                data.IDPemesanan = reader.GetString(0);
                data.NamaCustomer = reader.GetString(1);
                data.NoTelpon = reader.GetString(2);
                data.JumlahPemesanan = reader.GetInt32(3);
                data.Lokasi = reader.GetString(4);
                pemesanan.Add(data);
            }
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return pemesanans;
    }
    
    public string pemesanan(string IDPemesanan, string NamaCustomer, string NoTelpon, int JumlahPemesanan, string IDLokasi)
    {
        string a = "gagal";
        try
        {
            string sql = "insert into dbo.Pemesanan values ('" + IDPemesanan + "', '" + NamaCustomer + "', '" + NoTelpon + "', " + JumlahPemesanan + ", '" + IDLokasi + "')";
            connection = new SqlConnection(constring); //fungsi connect ke database
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

    public string editPemesanan(string IDPemesanan, string NamaCustomer, string No_telpon)
    {
        string a = "gagal";
        try
        {
            string sql = "update dbo.Pemesanan set Nama_customer = '" + NamaCustomer + "', No_telpon = '" + No_telpon + "'" + "where ID_reservasi = '" + IDPemesanan + "' ";
            connection = new SqlConnection(constring);
            com = new SqlCommand(sql, connection);
            connection.Open ();
            com.ExecuteNonQuery();
            connection.Close();

            a = "sukses";
        }
        catch(Exception es)
        {
            Console.WriteLine(es);
        }
        return a;
    }

    public string deletePemesanan(string IDPemesanan)
    {
        string a = "gagal";
        try
        {
            string sql = "delete from dbo.Pemesanan where ID_reservasi = '" + IDPemesanan + "'";
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
