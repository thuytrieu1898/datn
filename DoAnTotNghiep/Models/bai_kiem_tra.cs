//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnTotNghiep.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bai_kiem_tra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bai_kiem_tra()
        {
            this.bai_lam = new HashSet<bai_lam>();
            this.ct_bai_kiem_tra = new HashSet<ct_bai_kiem_tra>();
        }
    
        public int ID { get; set; }
        public Nullable<int> nguoi_tao { get; set; }
        public Nullable<int> chu_de { get; set; }
        public Nullable<int> so_cau_hoi { get; set; }
        public string ma_bkt { get; set; }
    
        public virtual chu_de chu_de1 { get; set; }
        public virtual tai_khoan tai_khoan { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bai_lam> bai_lam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ct_bai_kiem_tra> ct_bai_kiem_tra { get; set; }
    }
}
