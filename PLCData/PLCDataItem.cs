using System;
using System.Collections.Generic;

namespace PLCData
{
    public class PLCDataItem { 
        public string Addr = null;
        public char AlphaAddr = 'D';
        public int NumAddr = 0;
        public string Name = null;
        public const int SIZE = 2; // 1 = 16bits/2bytes; *default 2 = 32bits/4bytes;
        private short[] data;
        public bool Skip = false;

        public PLCDataItem(string addr, string name)
        {
            this.Addr = addr;
            this.AlphaAddr = this.Addr[0];
            this.NumAddr = int.Parse(this.Addr.Substring(1, this.Addr.Length-1));
            this.Name = name;
        }

       
        public void SetData(short[] input)
        {
            this.data = input;
        }

        public short[] GetData()
        {
            return this.data;
        }
    }
    

   
}
