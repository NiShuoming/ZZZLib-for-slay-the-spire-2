using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace ZZZLib.Patch
{
    [HarmonyPatch(typeof(AbstractModel), "ModifyDamageAdditive")]
    public static class FrailPowerModifyDamagePatch
    {
        static bool Prefix(
            AbstractModel __instance,  // 保持 FrailPower
            Creature? target,
            decimal amount,
            ValueProp props,
            Creature? dealer,
            CardModel? cardSource,
            ref decimal __result)
        {
            if (ZZZConfig.ModifiedFrail &&
                __instance is FrailPower powerf && dealer != null &&
                !dealer.IsPlayer &&
                target != null &&
                target.IsPlayer &&
                props.IsPoweredAttack() && dealer == powerf.Owner)
            {
                __result = -2;
                return false;
            }
            return true;
        }
    }
}
