using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActProgTypeLib;

namespace PLCData
{
    public class MitsuPLCAdapter
    {
        private ActProgType plc;
        public List<PLCDataItem> Items = new List<PLCDataItem>();
        public MitsuPLCAdapter(ActProgType _plc, List<PLCDataItem> _items)
        {
            this.plc = _plc;
            this.Items = _items;
        }
        public short[] ReadAddresss(string addr)
        {
            if (this.Items.Count <= 0) throw new IndexOutOfRangeException();
            PLCDataItem item = this.Items.Find(x => x.Addr == addr);
            if (item == null) throw new EntryPointNotFoundException();
            return plcReadShortArray(item);
        }

        private short plcReadShort(PLCDataItem item)
        {
            short tmp;
            int iret = plc.GetDevice2(item.Addr, out tmp);
            return tmp;
        }

        private short[] plcReadShortArray(PLCDataItem item)
        {
            short[] tmp = new short[PLCDataItem.SIZE];
            int iret = plc.ReadDeviceBlock2(item.Addr, PLCDataItem.SIZE, out tmp[0]);
            return tmp;
        }

        public float[] ReadAllToFloat()
        {
            int length = (this.Items[this.Items.Count - 1].NumAddr - this.Items[0].NumAddr);
            float[] res = new float[length / PLCDataItem.SIZE];

            short[] tmp = new short[length];
            //Read Data form PLC
            int iret;
            iret = plc.ReadDeviceBlock2(this.Items[0].Addr, length, out tmp[0]);
            for (int i = 0; i < length / PLCDataItem.SIZE; i++)
            {
                res[i] = PLCDataHelper.ToFloat(tmp[PLCDataItem.SIZE * i], tmp[(PLCDataItem.SIZE * i) + 1]);
            }
            return res;
        }
        public void ReadAll()
        {
            int length = ((this.Items[this.Items.Count - 1].NumAddr + PLCDataItem.SIZE) - this.Items[0].NumAddr);
            float[] res = new float[length / PLCDataItem.SIZE];

            short[] tmp = new short[length];
            //Read Data form PLC
            int iret;
            iret = plc.ReadDeviceBlock2(this.Items[0].Addr, length, out tmp[0]);
            for (int i = 0; i < length / PLCDataItem.SIZE; i++)
            {
                this.Items[i].SetData(new short[] { tmp[PLCDataItem.SIZE * i], tmp[(PLCDataItem.SIZE * i) + 1] });
            }
        }
        
        public void WriteAll() {
            int length = ((this.Items[this.Items.Count - 1].NumAddr + PLCDataItem.SIZE) - this.Items[0].NumAddr);
            List<short> data = new List<short>();
            foreach (PLCDataItem item in this.Items) {
                data.AddRange(item.GetData());
            }
            short[] refArr = data.ToArray();
            int iret = plc.WriteDeviceBlock2(this.Items[0].Addr, length, ref refArr[0]);
        }

        private void PLCCheckConnection(int iretCode)
        {
            if (iretCode != 0)
            {
                throw new Exception("PLC conection error");
                try
                {
                   
                }
                catch
                {
                    //
                }
            }
            else
            {
            }
        }
    }
}
