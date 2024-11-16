using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using market.enumaration;
using market.model;


namespace market.dao
{
    public class Repository
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        int returnvalue;
        List<LoginTable> loginTableList;
        public Repository()
        {
            con = new SqlConnection("Data Source=DESKTOP-ROH57NF\\SQLEXPRESS;Initial Catalog=market;User ID=sa;password=1");

        }
        public void baglantiAyarla()
        {
            if (con.State == System.Data.ConnectionState.Closed) //sql bağlantım eğer kapalıysa onu aç,açıksa da onu kapat diyerek  bağlantı ayarlanır önce
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
        }
        public User  login(string kullaniciAdi, string sifre)
        {
            con.Open();
            cmd = new SqlCommand("select *  from loginTable where kullaniciAdi=@kullaniciAdi  and sifre=@sifre ",con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                User user = new User();
                user.id=int.Parse(dr["id"].ToString());
                user.kullaniciAdi = dr["kullaniciAdi"].ToString() ;
                user.sifre= dr["sifre"].ToString();
                user.yetki = dr["yetki"].ToString();
                user.emailAdres  = dr["emailAdres"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                user.status = LoginStatus.basarili;
                return user;
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.basarisiz;
                return user;
            }
        }
        public List<LoginTable> getLoginTable()

        {
            loginTableList = new List<LoginTable>();

            con.Open();
            cmd = new SqlCommand("guvenlikSorusuGetir_sp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                LoginTable loginTable = new LoginTable();
                loginTable.id = int.Parse(dr["id"].ToString());
                loginTable.kullaniciAdi = dr["kullaniciAdi"].ToString();
                loginTable.sifre = dr["sifre"].ToString();
                loginTable.yetki = dr["yetki"].ToString();
                loginTable.emailAdres = dr["emailAdres"].ToString();
                loginTable.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                loginTable.guvenlikCevabı = dr["guvenlikCevabi"].ToString();
                loginTableList.Add(loginTable);

            }
            con.Close();


            return loginTableList;

        }
        public  LoginStatus   doAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabı)

        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from loginTable where kullaniciAdi=@kullaniciAdi and  guvenliksorusu=@guvenlikSorusu and guvenlikCevabı=@guvenlikCevabı",con);
          
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabı", guvenlikCevabı);

            returnvalue =(int)cmd.ExecuteScalar();
            con.Close();
            if (returnvalue ==1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
          





           
        }
        public LoginStatus changePassword(string kullaniciAdi,string sifre)
        {
            con.Open();
            cmd = new SqlCommand("sifreGuncelle_sp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            returnvalue = cmd.ExecuteNonQuery();
          


            con.Close();
            return LoginStatus.basarili;

        }
        public string   checkEmailAdres(string kullaniciAdi)
        {
            con.Open();
            cmd = new SqlCommand("select emailAdres from loginTable where kullaniciAdi=@kullaniciAdi",con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            string emailAdres= (string) cmd.ExecuteScalar();
            con.Close();
            return emailAdres;
        }
     }
            
 }
    


