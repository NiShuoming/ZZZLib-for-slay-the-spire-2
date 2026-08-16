using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// 玉魄冰心power 完成 增加以太帷幕加伤
    /// </summary>
    public class JadeSoulFrozenHeartPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip>
        { HoverTipFactory.FromPower<EtherVeil>()};

        public decimal ModifyVulnerableMultiplier(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (dealer != base.Owner)
            {
                return amount;
            }
            if (!props.IsPoweredAttack())
            {
                return amount;
            }
            return amount + (decimal)base.Amount / 100m;
        }
    }
}
