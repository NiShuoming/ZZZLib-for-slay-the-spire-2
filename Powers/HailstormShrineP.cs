using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 群体伤害会额外攻击1次
    /// </summary>
    public class HailstormShrineP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override int ModifyAttackHitCount(AttackCommand attack, int hitCount)
        {
            if(attack.Attacker==Owner && !attack.IsSingleTargeted)
            {
                return hitCount + 1;
            }
            return hitCount;
        }
    }
}
