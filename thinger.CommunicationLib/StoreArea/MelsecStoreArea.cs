using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.CommunicationLib.StoreArea
{
    public class MelsecStoreArea
    {
        public static readonly MelsecStoreArea X = new MelsecStoreArea(156, "X*", 1, 16);

        public static readonly MelsecStoreArea Y = new MelsecStoreArea(157, "Y*", 1, 16);

        public static readonly MelsecStoreArea X8 = new MelsecStoreArea(156, "X*", 1, 8);

        public static readonly MelsecStoreArea Y8 = new MelsecStoreArea(157, "Y*", 1, 8);

        public static readonly MelsecStoreArea M = new MelsecStoreArea(144, "M*", 1, 10);

        public static readonly MelsecStoreArea L = new MelsecStoreArea(146, "L*", 1, 10);

        public static readonly MelsecStoreArea F = new MelsecStoreArea(147, "F*", 1, 10);

        public static readonly MelsecStoreArea V = new MelsecStoreArea(148, "V*", 1, 10);

        public static readonly MelsecStoreArea B = new MelsecStoreArea(160, "B*", 1, 16);

        public static readonly MelsecStoreArea D = new MelsecStoreArea(168, "D*", 0, 10);

        public static readonly MelsecStoreArea W = new MelsecStoreArea(180, "W*", 0, 16);

        public static readonly MelsecStoreArea Z = new MelsecStoreArea(204, "Z*", 0, 10);

        public static readonly MelsecStoreArea TN = new MelsecStoreArea(194, "TN", 0, 10);

        public static readonly MelsecStoreArea SN = new MelsecStoreArea(200, "SN", 0, 10);

        public static readonly MelsecStoreArea CN = new MelsecStoreArea(197, "CN", 0, 10);

        public byte AreaBinaryCode { get; set; } = 0;


        public string AreaASCIICode { get; set; }

        public byte AreaType { get; set; } = 0;


        public int FromBase { get; set; }

        public MelsecStoreArea(byte areaBinaryCode, string areaASCIICode, byte areaType, int fromBase)
        {
            AreaBinaryCode = areaBinaryCode;
            AreaASCIICode = areaASCIICode;
            AreaType = areaType;
            FromBase = fromBase;
        }
    }
}
