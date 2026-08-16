using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 以太帷幕 增加伤害 完成
    /// </summary>
    public class EtherVeil : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("DamageA", 35m)];

        public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource, CardPlay? cardPlay)
        {
            if (dealer != Owner)
            {
                return 1m;
            }
            if (!props.IsPoweredAttack())
            {
                return 1m;
            }
            decimal num = 1 + base.DynamicVars["DamageA"].BaseValue / 100m;
            JadeSoulFrozenHeartPower jade = Owner.GetPower<JadeSoulFrozenHeartPower>();
            if (jade != null)
            {
                num = jade.ModifyVulnerableMultiplier(target, num, props, dealer, cardSource);
            }
            return num;
        }

        //减少层数
        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy)
            {
                await PowerCmd.TickDownDuration(this);
            }
        }
    }

}
