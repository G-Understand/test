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
    
    public partial class tb_userjiesuan
    {
        public int id { get; set; }
        public Nullable<long> orderid { get; set; }
        public string uid { get; set; }
        public Nullable<System.DateTime> setdate { get; set; }
        public Nullable<int> totalfen { get; set; }
        public string roomid { get; set; }
        public string roominfo { get; set; }
        public string gametype { get; set; }
        public Nullable<int> qiangzhuangcount { get; set; }
        public Nullable<int> zuozhuangcount { get; set; }
        public Nullable<int> tuizhucount { get; set; }
        public Nullable<int> isfangzhu { get; set; }
        public Nullable<int> isdayingjia { get; set; }
        public Nullable<int> istuhao { get; set; }
        public Nullable<int> jushu { get; set; }
        public string wanfa { get; set; }
        public string difen { get; set; }
        public Nullable<int> fee { get; set; }
        public Nullable<decimal> tax { get; set; }
    }
}
