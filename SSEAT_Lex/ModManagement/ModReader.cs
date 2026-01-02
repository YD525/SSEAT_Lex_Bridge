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
        public struct ModItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public bool HavePex { get; set; }
            public List<string> Files{ get; set; }
        }
        public bool IsMod(string Path,ref bool HavePex,ref List<string> Paths)
        {
            string[] Extensions = new string[] { "*.esp", "*.pex", "*.esm", "*.esl", "*.bsa" };
            var Files = Extensions.SelectMany(ext => Directory.GetFiles(Path, ext, SearchOption.AllDirectories)).ToArray();
            if (Files.Length > 0)
            {
                if (Files.Any(file => file.EndsWith(".pex", StringComparison.OrdinalIgnoreCase)))
                {
                    Paths = Files.ToList();
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
                List<string> Files = new List<string>();

                if (IsMod(GetDir,ref HavePex,ref Files))
                {
                    ModItem NewModItem = new ModItem();
                    NewModItem.Name = new FileInfo(GetDir).Name;
                    NewModItem.Path = GetDir;
                    NewModItem.HavePex = HavePex;
                    NewModItem.Files = Files;
                    Mods.Add(NewModItem);
                }
            }
            return Mods;
        }
    }
}
