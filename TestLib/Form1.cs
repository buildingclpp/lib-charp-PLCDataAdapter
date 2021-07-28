using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCData;
using System.Diagnostics;
using ActProgTypeLib;

namespace TestLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var D10100 = new PLCData.PLCDataItem("D10160", "Temp01");
            Debug.WriteLine(D10100);
            ActProgType plc = new ActProgType();
            plc.ActCpuType = 0x90;
            plc.ActUnitType = 0x30;
            plc.Open();
            MitsuPLCAdapter plcAdapter = new MitsuPLCAdapter(plc,PLCDataHelper.GenerateFromRange('D',10100,10200));

            //short[] res = plcAdapter.ReadAddresss("D10160");
            //Int32 i32 = PLCDataHelper.ToInt32(res[0], res[1]);
            //float f = PLCDataHelper.ToFloat(res[0], res[1]);
            Stopwatch watch1 = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            Stopwatch watch3 = new Stopwatch();
            watch1.Start();           
            for (int i = 10100; i <= 10200; i = i + 2)
            {
                short[] tmp1 = plcAdapter.ReadAddresss('D' + i.ToString());
                //float tmp2 = PLCDataHelper.ToFloat(tmp1[0], tmp1[1]);
                //Debug.WriteLine('D' + i.ToString() + "=>" + tmp2);
            }
            watch1.Stop();
            plcAdapter.ReadAll();
            watch2.Start();
            Debug.WriteLine("====================================================================================");
            //for (int i = 0; i < plcAdapter.Items.Count; i++)
            //{
            //    short[] data = plcAdapter.Items[i].GetData();
            //    Debug.WriteLine(plcAdapter.Items[i].Addr + "=>" + PLCDataHelper.ToFloat(data[0],data[1]));
            //    short[] tmp1 = plcAdapter.ReadAddresss('D' + i.ToString());
            //    float tmp2 = PLCDataHelper.ToFloat(tmp1[0], tmp1[1]);
            //    Debug.WriteLine('D' + i.ToString() + "=>" + tmp2);
            //}
            for (int i = 10100; i <= 10108; i = i + 2)
            {
                short[] tmp1 = plcAdapter.ReadAddresss('D' + i.ToString());
                //float tmp2 = PLCDataHelper.ToFloat(tmp1[0], tmp1[1]);
                //Debug.WriteLine('D' + i.ToString() + "=>" + tmp2);
            }

            watch2.Stop();
            watch3.Start();
            int tmp4 = 10100;
            short[] tmp2 = plcAdapter.ReadAddresss('D' + tmp4.ToString());
            watch3.Stop();
            Debug.Write("watch1=>"+watch1.Elapsed.TotalSeconds.ToString());
            Debug.Write("watch2=>"+watch2.Elapsed.TotalSeconds.ToString());
            Debug.Write("watch3=>"+watch3.Elapsed.TotalSeconds.ToString());
            plcAdapter.Items.Find(x => x.Addr == "D10166").SetData(PLCDataHelper.FromFloat(999.123f));
            plcAdapter.WriteAll();
        }
    }
}
