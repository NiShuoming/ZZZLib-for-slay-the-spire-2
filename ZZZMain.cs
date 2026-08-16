using BaseLib.Config;
using BaseLib.Patches.Localization;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models.CardPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cards.Curse;
using ZZZLib.Other;

namespace ZZZLib
{
    [ModInitializer(nameof(Initialize))]
    public static class ZZZLib
    {
        /// <summary>
        /// v0.110
        /// </summary>
        public static void Initialize()
        {
            GD.Print("ZZZLib - 加载中!");
            Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
            var harmony = new Harmony("ZZZLib");
            harmony.PatchAll();
            ModHelper.AddModelToPool(typeof(CurseCardPool), typeof(Demoralized));
            ModConfigRegistry.Register("ZZZLib", new ZZZConfig());
            GD.Print("ZZZLib - 加载成功!");

        }
    }
}
