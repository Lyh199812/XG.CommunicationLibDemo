using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Common.Config
{
    /// <summary>
    /// 变量
    /// </summary>
    public class Variable
    {

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 起始索引
        /// </summary>
        public string VarAddress { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 偏移量或长度
        /// </summary>
        public int OffsetOrLength { get; set; }

        
        public string CurValue { get; set; }



    }
}
