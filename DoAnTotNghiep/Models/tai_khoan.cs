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
    
    public partial class tai_khoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tai_khoan()
        {
            this.bai_kiem_tra = new HashSet<bai_kiem_tra>();
            this.bai_lam = new HashSet<bai_lam>();
        }
    
        public int ID { get; set; }
        public string tai_khoan1 { get; set; }
        public string mat_khau { get; set; }
        public int loai { get; set; }
        public string ho { get; set; }
        public string ten { get; set; }
        public Nullable<System.DateTime> ngay_sinh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bai_kiem_tra> bai_kiem_tra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bai_lam> bai_lam { get; set; }
        public virtual loai_tai_khoan loai_tai_khoan { get; set; }
    }
}
