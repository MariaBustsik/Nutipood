using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nutipood.Models
{
    public class SmartDevice
    {
        //определение перечислений для упрощения ограничений

        public enum e_type { Nutitelefon, Tahvelarvuti, Smart_watch }

        public enum e_platform { Android, iOS, Muu }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Model { get; set; }
        public string Tootja { get; set; }

        //использовать перечисление для упрощения ограничений
        public e_type Type { get; set; }
        public e_platform Platvorm { get; set; }
        public double Hind { get; set; }
        public string ImageUrl { get; set; }




    }
}
