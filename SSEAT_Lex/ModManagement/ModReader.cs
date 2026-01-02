using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
            public bool HavePex { get; set; }

            public ModItem(string Path,bool HavePex)
            { 
               this.Path = Path;
               this.Name = new FileInfo(Path).Name;
               this.HavePex = HavePex;
            }
        }
        public bool IsMod(string Path,ref bool HavePex)
        {
            string[] Extensions = new string[] { "*.esp", "*.pex", "*.esm", "*.esl", "*.bsa" };
            var Files = Extensions.SelectMany(ext => Directory.GetFiles(Path, ext, SearchOption.AllDirectories)).ToArray();
            if (Files.Length > 0)
            {
                if (Files.Any(file => file.EndsWith(".pex", StringComparison.OrdinalIgnoreCase)))
                {
                    HavePex = true;
                }

                return true;
            }
            return false;
        }
        public List<ModItem> TestSCanModPath(string Path)
        {
            List<ModItem> Mods = new List<ModItem>();
            foreach (var GetDir in Directory.GetDirectories(Path))
            {
                bool HavePex = false;
                if (IsMod(GetDir,ref HavePex))
                {
                    Mods.Add(new ModItem(GetDir,HavePex));
                }
            }
            return Mods;
        }
    }
}
