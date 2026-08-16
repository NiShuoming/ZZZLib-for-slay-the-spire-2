using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace ZZZLib.Patch
{
    /// <summary>
    /// .110"DrawInternal"
    /// </summary>
    [HarmonyPatch(typeof(CardPileCmd), "DrawInternal")]
    public static class DrawInternalPatch
    {
        // Prefix在方法执行前运行，可以修改参数
        [HarmonyPrefix]
        public static void Prefix(PlayerChoiceContext choiceContext,
            ref decimal count, Player player, bool fromHandDraw = false)
        {
            count += ZZZHook.ModifyDrawCard(choiceContext, count, player, fromHandDraw);
        }
    }
}
