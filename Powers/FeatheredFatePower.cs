using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZZZLib.Powers
{
    /// <summary>
    /// 谶语之誓 多段攻击+1
    /// </summary>
    public class FeatheredFatePower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override int ModifyAttackHitCount(AttackCommand attack, int hitCount)
        {
            if (attack.Attacker == Owner && hitCount > 1)
            {
                return hitCount + Amount;
            }
            else return hitCount;
        }
    }
}