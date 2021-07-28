﻿using System;

namespace PLCDataAdapter
{
    public class PLCDataItem { 
        public string Addr = null;
        private char alphaAddr = 'D';
        private int numAddr = 0;
        public string Name = null;
        public int Size = 1; // 1 = 16 bits;2 = 32 bits;
        //private T data;

        public PLCDataItem(string addr, string name, int size)
        {
            this.Addr = addr;
            //this.alphaAddr = this.Addr[0];
            //this.numAddr = int.Parse(this.Addr.Substring(1, this.Addr.Length));
            this.Name = name;
            this.Size = size;
        }

        //public void SetData(T input) {
        //    this.data = input;
        //}

        //public T GetData()
        //{
        //    return this.data;
        //}
    }
}
