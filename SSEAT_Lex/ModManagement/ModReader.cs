using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoenixEngine.ConvertManager;

namespace SSEAT_Lex.ModManagement
{
    public class ModReader
    {
        public class ModItem
        {
            public string Name { get; set; }
            public string Path { get; set; }

            public ModItem(string Path)
            { 
               this.Path = Path;
               this.Name = new FileInfo(Path).Name;
            }
        }
        public bool IsMod(string Path)
        {
            string[] Extensions = new string[] { "*.esp", "*.pex", "*.esm", "*.esl", "*.bsa" };
            var Files = Extensions.SelectMany(ext => Directory.GetFiles(Path, ext, SearchOption.AllDirectories)).ToArray();
            if (Files.Length > 0)
            {
                return true;
            }
            return false;
        }
        public List<ModItem> TestSCanModPath(string Path)
        {
            List<ModItem> Mods = new List<ModItem>();
            foreach (var GetDir in Directory.GetDirectories(Path))
            {
                if (IsMod(GetDir))
                {
                    Mods.Add(new ModItem(GetDir));
                }
            }
            return Mods;
        }
    }
}
