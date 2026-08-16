using BaseLib;
using BaseLib.Config;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Other
{
    [ConfigHoverTipsByDefault]
    internal class ZZZConfig : SimpleModConfig
    {
        public static bool OpenEvent { get; set; } = true;

        public static bool ModifiedFrail { get; set; } = true;

    }
}
