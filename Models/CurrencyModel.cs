using System;
using System.ComponentModel.DataAnnotations;

namespace datetime_tests.Models
{
    public class CurrencyModel
    {
        [Key]
        public int Cur_ID { get; set; }
        public int? Cur_ParentID { get; set; }
        public string Cur_Code { get; set; }
        public string Cur_Abbreviation { get; set; }
        public string Cur_Name { get; set; }
        public string Cur_Name_Bel { get; set; }
        public string Cur_Name_Eng { get; set; }
        public string Cur_QuotName { get; set; }
        public string Cur_QuotName_Bel { get; set; }
        public string Cur_QuotName_Eng { get; set; }
        public string Cur_NameMulti { get; set; }
        public string Cur_Name_BelMulti { get; set; }
        public string Cur_Name_EngMulti { get; set; }
        public int Cur_Scale { get; set; }
        public int Cur_Periodicity { get; set; }
        public System.DateTime Cur_DateStart { get; set; }
        public System.DateTime Cur_DateEnd { get; set; }
    }
}
