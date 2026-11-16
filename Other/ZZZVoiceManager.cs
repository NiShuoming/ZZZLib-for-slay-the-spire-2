using BaseLib.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Other
{
    public static class ZZZVoiceManager
    {
        private static readonly Random _rng = new();

        static int GetRandomN(int n)
        {
            return _rng.Next(n);
        }

        static string GetLanguage()
        {
            return ZZZConfig.VoiceLanguage == ZZZConfig.Language.Chinese ? "中" : "日";
        }

        static string Format(string text, bool language)
        {
            string temp = language ? GetLanguage() : "";
            return "res://ZZZLib/voice/" + text + temp + ".mp3";
        }

        public static void PlayVoice(string voice, int n = 0, bool isVoice = true, bool language =false, int volumeAdd = 5)
        {
            if (!ZZZConfig.CharacterVoice) return;
            string num = "";
            if(n>0)num = (GetRandomN(n)+1).ToString();
            ModAudio.PlaySound(new ModSound(
                Format(voice + num, language), ModAudio.SoundType.Sfx), volumeAdd);
        }
    }
}
