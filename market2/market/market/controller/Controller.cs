using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using market.dao;
using market.enumaration;
using System.Data;
using System.Data.SqlClient;
using market.model;
using System.Diagnostics;

namespace market.controller
{
   public class Controller
    {
       Repository repository;
       public Controller()
        {
            repository = new Repository();
        }
        public User login(string kullaniciAdi, string sifre)// metodalrın isimlerini controllerde de repositoryde de aynı vermeye çalışmalıyız ki birbirleriyle aynı olduklarını anlayalım
        {
            User result;//metod seviseyesinde tanımladıım ki heryerden erişebileyim . ekstra bir kac kere yazmama gerk kalmasın
            if(!string .IsNullOrEmpty (kullaniciAdi ) && !string .IsNullOrEmpty(sifre ))
            {
               result=    repository.login(kullaniciAdi, sifre);
                return result;
               
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.eksikParametre;
                return  user;
            }
           
        }
        public List<LoginTable> getLoginTable()
        {
            List<LoginTable> loginTableList = repository.getLoginTable();
            return loginTableList;
        }
        public LoginStatus doAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabı)
        {
            if(!string .IsNullOrEmpty(kullaniciAdi ) && !string .IsNullOrEmpty (guvenlikSorusu) && !string .IsNullOrEmpty (guvenlikCevabı ))
            {
                LoginStatus result= repository.doAuthentication(kullaniciAdi, guvenlikSorusu, guvenlikCevabı);
                if(result ==LoginStatus.basarili)
                {
                    return result;
                }
                else
                {
                    return LoginStatus.basarisiz;
                }
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
            
        }
        public LoginStatus changePassword(string kullaniciAdi, string sifre)
        {
            if(!string.IsNullOrEmpty (kullaniciAdi )&& !string .IsNullOrEmpty (sifre))
            {
               return  repository.changePassword(kullaniciAdi, sifre);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }

        }
        public string  checkEmailAdres (string kullaniciAdi)
        {
             return repository.checkEmailAdres(kullaniciAdi);
        }

    }
}
