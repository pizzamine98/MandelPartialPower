using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO;
using System.Text;

namespace MandelPartialPower
{
    class RunHandler
    {
        public string root,root2,foldhold,foldhold3,foldhold4,path3,path4;
        public string[] folders0;
        public RunData data;
        public int runid,whichfold;
        public bool hasit0;
        public void SetupStuff()
        {
            root2 = root + "\\rundata";
            folders0 = Directory.GetDirectories(root2);
            hasit0 = false;
            if(folders0.Length == 0)
            {
                runid = 0;
                Directory.CreateDirectory(root2 + "\\runs_" + data.gsettings.poly.id);
                foldhold = root2 + "\\runs_" + data.gsettings.poly.id + "\\" + "run_" + runid;
                Directory.CreateDirectory(
                    root2 + "\\runs_" + data.gsettings.poly.id + "\\" + "run_" + runid);
                Directory.CreateDirectory(foldhold + "\\runinfos");
                Directory.CreateDirectory(foldhold + "\\runplots");
                foldhold3 = foldhold + "\\runinfos";
                foldhold4 = foldhold + "\\runplots";
            } else
            {
                whichfold = -1;
                for(int iis = 0; iis < folders0.Length; iis++)
                {
                    if(folders0[iis].Contains("runs_" + data.gsettings.poly.id))
                    {
                        hasit0 = true;
                        whichfold = iis;
                    }
                }
                if (!hasit0)
                {
                    runid = 0;
                    Directory.CreateDirectory(root2 + "\\runs_" + data.gsettings.poly.id);
                    foldhold = root2 + "\\runs_" + data.gsettings.poly.id + "\\" + "run_" + runid;
                    Directory.CreateDirectory(
                        root2 + "\\runs_" + data.gsettings.poly.id + "\\" + "run_" + runid);
                    Directory.CreateDirectory(foldhold + "\\runinfos");
                    Directory.CreateDirectory(foldhold + "\\runplots");
                    foldhold3 = foldhold + "\\runinfos";
                    foldhold4 = foldhold + "\\runplots";
                } else
                {
                    string foldhold2 = root2 + "\\runs_" + data.gsettings.poly.id;
                    string[] folders1 = Directory.GetDirectories(foldhold2);
                    
                    if(data.gsettings.zoomnum == 0)
                    {
                        runid = folders1.Length;
                        foldhold = root2 + "\\runs_" + data.gsettings.poly.id + "\\" + "run_" + runid;
                        
                        Directory.CreateDirectory(foldhold);
                        Directory.CreateDirectory(foldhold + "\\runinfos");
                        Directory.CreateDirectory(foldhold + "\\runplots");
                        foldhold3 = foldhold + "\\runinfos";
                        foldhold4 = foldhold + "\\runplots";
                    } else
                    {
                        runid = folders1.Length - 1;
                        foldhold3 = foldhold + "\\runinfos";
                        foldhold4 = foldhold + "\\runplots";
                    }
                }
            }
            path3 = root2 + "\\runs_" + data.gsettings.poly.id + "\\run_" + runid + "\\runinfos\\run_" + data.gsettings.poly.id + "_" + runid + "_" + data.gsettings.zoomnum + ".json";
            path4 = root2 + "\\runs_" + data.gsettings.poly.id +  "\\run_" + runid + "\\runplots\\run_" + data.gsettings.poly.id + "_" + runid + "_" + data.gsettings.zoomnum + ".bmp";
        }
        public ImageHandler handle;
        public void WriteRun(byte[] coldatain)
        {
            System.IO.File.WriteAllText(path3, JsonConvert.SerializeObject(data));
            handle = new ImageHandler();
            Bitmap mappy = handle.GetDataPicture(data.gsettings.width,
                data.gsettings.height, coldatain);
            mappy.Save(path4, System.Drawing.Imaging.ImageFormat.Bmp);
        }
    }
}
