using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace eq2memread
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Searching for EQ2 process...");
            Process[] processes = Process.GetProcessesByName("EverQuest2");
            //Process[] processes = Process.GetProcessesByName("war");

            /*
            Process[] localAll = Process.GetProcesses();

            System.Console.WriteLine(localAll.Length);

            for (int i=0;i<localAll.Length;i++)
            {
                Process procname = localAll[i];
      
                System.Console.WriteLine(procname.ProcessName);
            }
            */

            if (processes.Length > 0)
            {
                System.Console.WriteLine("EQ2 found.");
                System.Diagnostics.Process eqproc = processes[0];

                int addrbase = 0x400000;

                while (true)
                {

                    MemoryLoc Pmhp = new MemoryLoc(eqproc, addrbase + 0xFC6CC);
                    MemoryLoc max_hp = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0x8C);
                    MemoryLoc cur_hp = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0x88);

                    MemoryLoc cur_mp = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0x94);
                    MemoryLoc max_mp = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0x98);

                    MemoryLoc cur_str = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0xD8);
                    MemoryLoc cur_sta = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0xDA);
                    MemoryLoc cur_dex = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0xDC);
                    MemoryLoc cur_wis = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0xDE);
                    MemoryLoc cur_int = new MemoryLoc(eqproc, Pmhp.GetInt32() + 0xE0);

                    MemoryLoc Ploc = new MemoryLoc(eqproc, addrbase + 0x165DE0);
                    MemoryLoc cur_xaxis = new MemoryLoc(eqproc, Ploc.GetInt32() + 0x74);
                    MemoryLoc cur_yaxis = new MemoryLoc(eqproc, Ploc.GetInt32() + 0x7C);
                    MemoryLoc cur_zaxis = new MemoryLoc(eqproc, Ploc.GetInt32() + 0x78);
                    MemoryLoc heading = new MemoryLoc(eqproc, Ploc.GetInt32() + 0x8);

                    /*
                    for (int i = 0; i < 255; i++)
                    {
                        MemoryLoc testx = new MemoryLoc(eqproc, Ploc.GetInt32() + i);
                        if(testx.GetFloat() != 0)
                            System.Console.WriteLine("i: "+i+" "+testx.GetFloat());
                    }
                     * */

                    System.Console.WriteLine(cur_hp.GetInt32() + " / " + max_hp.GetInt32());
                    System.Console.WriteLine(cur_mp.GetInt32() + " / " + max_mp.GetInt32());
                    System.Console.WriteLine("STR: " + cur_str.GetInt16() + " STA: " + cur_sta.GetInt16() + " DEX: " + cur_dex.GetInt16() + " WIS: " + cur_wis.GetInt16() + " INT: " + cur_int.GetInt16()); 
                    System.Console.WriteLine("x: " + cur_xaxis.GetFloat() + " y: " + cur_yaxis.GetFloat() + " z: " + cur_zaxis.GetFloat());
                    System.Console.WriteLine("heading: " + heading.GetFloat());

                    MemoryLoc Ptarget = new MemoryLoc(eqproc, addrbase + 0xD5EE34);
                    MemoryLoc thp = new MemoryLoc(eqproc, Ptarget.GetInt32() + 0x168);
                    MemoryLoc tmp = new MemoryLoc(eqproc, Ptarget.GetInt32() + 0x170);
                    MemoryLoc tlvl = new MemoryLoc(eqproc, Ptarget.GetInt32() + 0x178);

                    MemoryLoc Ptargetx = new MemoryLoc(eqproc, addrbase + 0x12BEF0);
                    MemoryLoc targetx1 = new MemoryLoc(eqproc, Ptargetx.GetInt32() + 0x220);
                    MemoryLoc targetx2 = new MemoryLoc(eqproc, targetx1.GetInt32() + 0x34);
                    MemoryLoc targetx = new MemoryLoc(eqproc, targetx2.GetInt32() + 0x5B68);
                    MemoryLoc targety = new MemoryLoc(eqproc, targetx2.GetInt32() + 0x5B70);

                    MemoryLoc Ptname = new MemoryLoc(eqproc, addrbase + 0xDE29B0);
                    MemoryLoc Ptname1 = new MemoryLoc(eqproc, Ptname.GetInt32() + 0x558);
                    MemoryLoc Ptname2 = new MemoryLoc(eqproc, Ptname1.GetInt32() + 0x170);
                    MemoryLoc Ptname3 = new MemoryLoc(eqproc, Ptname2.GetInt32() + 0x10);
                    MemoryLoc Ptname4 = new MemoryLoc(eqproc, Ptname3.GetInt32() + 0x38);
                    MemoryLoc tname = new MemoryLoc(eqproc, Ptname4.GetInt32() + 0xC4);

                    System.Console.WriteLine("\nTarget: HP: " + thp.GetInt32() + "% MP: "+tmp.GetInt32()+"% Lvl: "+tlvl.GetInt32());
                    System.Console.WriteLine("         X: "+targetx.GetFloat() + " Y: " + targety.GetFloat());
                    System.Console.WriteLine(tname.getString(32, true));

                    //System.Threading.Thread.Sleep(5000000);
                    System.Threading.Thread.Sleep(200);
                    //System.Console.ReadLine();

                    System.Console.Clear();
                }
            }
        }
    }
}
