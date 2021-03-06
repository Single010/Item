//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Order.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Mediciner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mediciner()
        {
            this.Appointment = new HashSet<Appointment>();
            this.Comment = new HashSet<Comment>();
            this.Question = new HashSet<Question>();
        }
    
        public int Mid { get; set; }
        [Required(ErrorMessage = "账号必需填写")]
        public string Mloginname { get; set; }
        [Required(ErrorMessage = "密码必需填写")]
        public string Mpwd { get; set; }
        [Required(ErrorMessage = "姓名必需填写")]
        public string Mname { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "介绍必需填写")]
        public string Titles { get; set; }
        public string Mspec { get; set; }
        public Nullable<System.DateTime> MtimeA { get; set; }
        public Nullable<System.DateTime> MtimeB { get; set; }
        public Nullable<System.DateTime> MtimeC { get; set; }
        public int Mcount { get; set; }
        public int Hid { get; set; }
        public int Did { get; set; }
        public string MPic { get; set; }
        public Nullable<int> Mstate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Dept Dept { get; set; }
        public virtual Hospital Hospital { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Question { get; set; }
    }
}
