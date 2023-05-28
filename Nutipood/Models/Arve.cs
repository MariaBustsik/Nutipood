using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nutipood.Models
{
    public class Arve
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Summa { get; set; }
        public string KlientNimi { get; set; }
        public string Photo { get; set; }

        //названия продуктов, перенесенных из корзины
        public string TootedStr { get { return "Tooted: " + String.Join(", ", App.Ostukorv.GetProductNames()); } }


        public Arve()
        {
            //значок по умолчанию для счетов

            this.Photo = "youtube_profile_image.png";

        }



    }
}
