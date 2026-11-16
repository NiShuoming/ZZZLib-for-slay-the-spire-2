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
    public class ZZZConfig : SimpleModConfig
    {
        public static bool OpenEvent { get; set; } = true;

        public static bool ModifiedFrail { get; set; } = true;

        public static bool CharacterVoice { get; set; } = true;

        public enum Language
        {
            Chinese, Japanese
        }

        public static Language VoiceLanguage { get; set; } = Language.Japanese;
    }
}
