//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace tryMJcard.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_gameRecords
    {
        public int id { get; set; }
        public int roomId { get; set; }
        public string gameRecords { get; set; }
        public Nullable<System.DateTime> stateTime { get; set; }
        public Nullable<decimal> orderId { get; set; }
        public Nullable<int> round { get; set; }
        public Nullable<int> julebuid { get; set; }
        public Nullable<int> roomtype { get; set; }
    }
}
