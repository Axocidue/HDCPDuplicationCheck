//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rhine
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODINFO
    {
        public PRODINFO()
        {
            this.UNITs = new HashSet<UNIT>();
        }
    
        public decimal PRODINFOID { get; set; }
        public string DPYPRODNUM { get; set; }
        public string DPYREVSTATE { get; set; }
        public string KRCPRODNUM { get; set; }
        public string KRCREVSTATE { get; set; }
        public string APPSWPRODNUM { get; set; }
        public string APPSWREVSTATE { get; set; }
        public string DSFPRODNUM { get; set; }
        public string DSFREVSTATE { get; set; }
        public string KRYPRODNUM { get; set; }
        public string KRYREVSTATE { get; set; }
        public string ACCESSORY { get; set; }
        public string APPSWVERNUM { get; set; }
        public string DSFVERNUM { get; set; }
    
        public virtual ICollection<UNIT> UNITs { get; set; }
    }
}