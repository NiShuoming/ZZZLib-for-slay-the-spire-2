using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;
using static BaseLib.Utils.BetaMainCompatibility;

namespace ZZZLib.Cmds
{
    public static class ZZZHook
    {
        /// <summary>
        /// 获得实现IZZZInterface的model
        /// </summary>
        /// <param name="combatState"></param>
        /// <returns></returns>
        private static IEnumerable<T> IterateCombatHookListeners<T>(ICombatState combatState)
        {
            if (CombatManager.Instance.IsOverOrEnding && !CombatManager.Instance.IsStarting)
            {
                yield break;
            }
            foreach (AbstractModel item in combatState.IterateHookListeners())
            {
                if (item is T item2)
                    yield return item2;
            }
        }
        /// <summary>
        /// 修正第二能量获取
        /// </summary>
        /// <returns></returns>
        public static int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            foreach (var item in IterateCombatHookListeners<IZZZSecondEnergyHook>(player.Creature.CombatState))
            {
                amount = item.ModifySecondEnergyGain(player, amount, giver, cardModel);
            }
            return amount;
        }
        /// <summary>
        /// 第二能量改变之后
        /// </summary>
        /// <returns></returns>
        public static async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            foreach (var item in IterateCombatHookListeners<IZZZSecondEnergyHook>(player.Creature.CombatState))
            {
                await item.AfterSecondEnergyChanged(player, before, after, cardModel);
            }
        }
        /// <summary>
        /// 修正最大第二能量
        /// </summary>
        /// <returns></returns>
        public static int ModifyMaxSecondEnergy(Player player, int amount)
        {
            foreach (var item in IterateCombatHookListeners<IZZZSecondEnergyHook>(player.Creature.CombatState))
            {
                amount = item.ModifyMaxSecondEnergy(player, amount);
            }
            return amount;
        }
        /// <summary>
        /// 触发属性异常
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public static async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            foreach (var item in IterateCombatHookListeners<IZZZTriggleAnomalyHook>(applier.CombatState))
            {
                await item.TriggleAnomaly(playerChoice, keyWord, target, applier, results);
            }
        }

        public static int ModifyDrawCard(PlayerChoiceContext playerChoice, decimal count, Player player, bool isHandDraw)
        {
            int i = 0;
            foreach (var item in IterateCombatHookListeners<IZZZDrawCardHook>(player.Creature.CombatState))
            {
                i += item.ModifyDrawCard(playerChoice, count, player, isHandDraw);
            }
            return i;
        }
    }
}
