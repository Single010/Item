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
    
    public partial class Comment
    {
        public int Cid { get; set; }
        public int Uid { get; set; }
        public int Mid { get; set; }
        public int Aid { get; set; }
        public System.DateTime Ctime { get; set; }
        public string Comments { get; set; }
        public int Cstate { get; set; }
    
        public virtual Appointment Appointment { get; set; }
        public virtual Mediciner Mediciner { get; set; }
        public virtual User User { get; set; }
    }
}
