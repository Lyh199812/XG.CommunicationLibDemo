using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.CommunicationLib.StoreArea
{
    public class MelsecA1EStoreArea
    {
        public static readonly MelsecA1EStoreArea X = new MelsecA1EStoreArea(new byte[2] { 88, 32 }, "X*", 1, 8);

        public static readonly MelsecA1EStoreArea Y = new MelsecA1EStoreArea(new byte[2] { 89, 32 }, "Y*", 1, 8);

        public static readonly MelsecA1EStoreArea M = new MelsecA1EStoreArea(new byte[2] { 77, 32 }, "M*", 1, 10);

        public static readonly MelsecA1EStoreArea W = new MelsecA1EStoreArea(new byte[2] { 87, 32 }, "W*", 1, 16);

        public static readonly MelsecA1EStoreArea D = new MelsecA1EStoreArea(new byte[2] { 68, 32 }, "D*", 0, 10);

        public byte[] AreaBinaryCode { get; set; } = new byte[2];


        public string AreaASCIICode { get; set; }

        public byte AreaType { get; set; } = 0;


        public int FromBase { get; set; }

        public MelsecA1EStoreArea(byte[] areaBinaryCode, string areaASCIICode, byte areaType, int fromBase)
        {
            AreaBinaryCode = areaBinaryCode;
            AreaASCIICode = areaASCIICode;
            AreaType = areaType;
            FromBase = fromBase;
        }
    }
}
