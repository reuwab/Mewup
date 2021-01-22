using System;
using System.Collections.Generic;
using System.Text;

namespace OperatingSystem.Core.PkgMan
{
    public static class globalvars
    {
        public static List<OperatingSystem.Core.Types.Program> Pkgs;
    }
    class Core
    {
        public static void BuildPkgDefs()
        {
            Types.Program TextEdit = new Programs.TextEdit();
            Types.Program MIV = new Programs.MIV();
            globalvars.Pkgs.Add(TextEdit);
            globalvars.Pkgs.Add(MIV);
        }
    }
}
