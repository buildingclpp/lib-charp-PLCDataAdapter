using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCData
{
    public class PLCDataHelper
    {
        public static List<PLCDataItem> GenerateFromRange(char alpha, int begin, int end, int initSize = 2)
        {
            List<PLCDataItem> res = new List<PLCDataItem>();
            for (int i = begin; i <= end; i = i + initSize)
            {
                var item = new PLCDataItem(alpha + i.ToString(), alpha + i.ToString());
                res.Add(item);
            }
            return res;
        }
        public static List<PLCDataItem> GenerateFromCSV(string path, int initSize = 2)
        {
            List<PLCDataItem> res = new List<PLCDataItem>();
           
            return res;
        }

        public static Int32 ToInt32(short left, short right)
        {

            Byte[] rBytes = BitConverter.GetBytes((short)left);
            Byte[] lBytes = BitConverter.GetBytes((short)right);

            Byte[] combined = new Byte[lBytes.Length + rBytes.Length];
            combined[0] = rBytes[0];
            combined[1] = rBytes[1];
            combined[2] = lBytes[0];
            combined[3] = lBytes[1];

            return BitConverter.ToInt32(combined, 0); ;
        }

        public static float ToFloat(short left, short right)
        {
            return ((float)ToInt32(left, right) / 1000);
        }

        public static short[] FromInt32(Int32 data) {
            byte[] bytes = BitConverter.GetBytes(data);
            short[] res = new short[PLCDataItem.SIZE];
            res[0] = BitConverter.ToInt16(bytes, 0);
            res[1] = BitConverter.ToInt16(bytes, 2);
            return res;
        }

        public static short[] FromFloat(float data)
        {
            Int32 tmp = Convert.ToInt32(Math.Ceiling(data * 1000));
            byte[] bytes = BitConverter.GetBytes(tmp);
            short[] res = new short[PLCDataItem.SIZE];
            res[0] = BitConverter.ToInt16(bytes, 0);
            res[1] = BitConverter.ToInt16(bytes, 2);
            return res;
        }

    }
}
