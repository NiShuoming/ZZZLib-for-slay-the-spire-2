using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 双生泣星 p 施加异常伤害时，获得1精通
    /// </summary>
    public class WeepingGeminiPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        int num = 0;

        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if(props == ZZZValueProp.Anomaly && dealer!=null && dealer == Owner)
            {
                if(num <= Amount)
                {
                    num++;
                    await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner, 1, Owner, null);
                }
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if(side == CombatSide.Player && num>0)
            {
                await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner, -num, Owner, null);
                num = 0;
            }
        }
    }
}
