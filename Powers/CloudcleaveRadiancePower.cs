using Godot;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 云霓孤光 1 对脆弱的敌人额外造成3->5点伤害 完成
    /// </summary>
    public class CloudcleaveRadiancePower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource, CardPlay? cardPlay)
        {
            if (target != null && target != Owner && dealer == Owner && target.GetPower<FrailPower>() != null
                && props == ValueProp.Move)
            {
                return Amount;
            }
            return 0;
        }
    }

}
