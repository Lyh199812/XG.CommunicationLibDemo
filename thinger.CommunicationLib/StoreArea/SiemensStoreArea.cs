using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.CommunicationLib.StoreArea
{
    public class SiemensStoreArea
    {
        public static readonly SiemensStoreArea I = new SiemensStoreArea(DataType.Input, 0);

        public static readonly SiemensStoreArea Q = new SiemensStoreArea(DataType.Output, 0);

        public static readonly SiemensStoreArea M = new SiemensStoreArea(DataType.Memory, 0);

        public static readonly SiemensStoreArea V = new SiemensStoreArea(DataType.DataBlock, 1);

        public static readonly SiemensStoreArea DB = new SiemensStoreArea(DataType.DataBlock, 0);

        public static readonly SiemensStoreArea T = new SiemensStoreArea(DataType.Timer, 0);

        public static readonly SiemensStoreArea C = new SiemensStoreArea(DataType.Counter, 0);

        public int DBNo { get; set; } = 0;


        public DataType DataType { get; set; } = DataType.DataBlock;


        public SiemensStoreArea(DataType dataType, int dbNo)
        {
            DataType = dataType;
            DBNo = dbNo;
        }
    }
}
