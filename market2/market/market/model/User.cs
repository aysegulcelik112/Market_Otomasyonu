using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using market.enumaration;

namespace market.model
{
   public  class User
    {
        public int id{ get; set; }
        public  string  kullaniciAdi { get; set; }
        public  string sifre { get; set; }
        public  string yetki { get; set; }
        public  string emailAdres  { get; set; }
        public string guvenlikSorusu { get; set; }
        public  string guvenlikCevabi{ get; set; }
        public  LoginStatus status { get; set; }
    }
}
