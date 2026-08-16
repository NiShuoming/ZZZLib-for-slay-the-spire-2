using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace ZZZLib.Cmds
{
    public static class ZZZCmd
    {
        /// <summary>
        /// 添加第二能量
        /// </summary>
        public static async Task AddSecondEnergy(Player player, int amount, Creature giver, CardModel cardModel)
        {
            ZZZSecondEnergy secondEnergy = player.PlayerCombatState.GetSecondEnergy(player);
            amount = ZZZHook.ModifySecondEnergyGain(player, amount, giver, cardModel);
            int before = secondEnergy.Energy2;
            secondEnergy.Energy2 += amount;
            await ZZZHook.AfterSecondEnergyChanged(player, before, secondEnergy.Energy2, cardModel);
        }
        public static ZZZSecondEnergy GetSecondEnergy(Player player)
        {
            return player.PlayerCombatState.GetSecondEnergy(player);
        }
        /// <summary>
        /// 触发风化 造成4群体【属性异常】伤害，每有1种debuff，多造成2伤害
        /// </summary>
        public static async Task<IEnumerable<DamageResult>> Windbloom(PlayerChoiceContext playerChoice, Creature target, Creature applier)
        {
            IEnumerable<Creature> targets;
            if (applier.IsPlayer) targets = applier.CombatState.HittableEnemies;
            else targets = applier.CombatState.PlayerCreatures;

            decimal damage = ZZZGlobalData.WindbloomDamage + (target.IsAlive ?
                target.Powers.Count(a => (a.Type == PowerType.Debuff)) * ZZZGlobalData.AddWindbloomDamage : 0);
            await Cmd.CustomScaledWait(0.1f, 0.2f);

            IEnumerable<DamageResult> results = await CreatureCmd.Damage(
                playerChoice, targets, damage, ValueProp.Unpowered | ZZZValueProp.Anomaly, applier, null, null);
            await ZZZHook.TriggleAnomaly(playerChoice, ZZZKeyWord.Windbloom, target, applier, results);
            return results;
        }
        /// <summary>
        /// 触发异放 每有1种负面效果，造成1次4【属性异常】伤害
        /// </summary>
        public static async Task<IEnumerable<DamageResult>> Abloom(PlayerChoiceContext choiceContext, Creature target, Creature applier)
        {
            IEnumerable<DamageResult> results = [];
            if (target == null || !target.IsAlive) return results;
            int n = target.Powers.Count(a => a.Type == PowerType.Debuff);
            for (int i = 0; i < n; i++)
            {
                await Cmd.CustomScaledWait(0.1f, 0.2f);

                results = results.Concat(await CreatureCmd.Damage(choiceContext,
                    target, ZZZGlobalData.AbloomDamage,
                    ValueProp.Unpowered | ZZZValueProp.Anomaly, applier, null, null));
                if (target.IsDead) break;

            }
            await ZZZHook.TriggleAnomaly(choiceContext, ZZZKeyWord.Abloom, target, applier, results);
            return results;
        }
        /// <summary>
        /// 紊乱 造成8伤
        /// </summary>
        public static async Task<IEnumerable<DamageResult>> Disorder(PlayerChoiceContext choiceContext, Creature target, Creature applier)
        {
            IEnumerable<DamageResult> results = [];
            if (target == null || !target.IsAlive) return results;
            await Cmd.CustomScaledWait(0.1f, 0.2f);

            results = results.Concat(await CreatureCmd.Damage(choiceContext,
                    target, ZZZGlobalData.DisorderDamage,
                    ValueProp.Unpowered | ZZZValueProp.Anomaly, applier, null, null));
            await ZZZHook.TriggleAnomaly(choiceContext, ZZZKeyWord.Disorder, target, applier, results);
            return results;
        }

    }
}
