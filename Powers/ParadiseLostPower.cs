using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cards.Equips.Other;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 失乐园 触发异常获得力量
    /// </summary>
    public class ParadiseLostPower : PowerModel, IZZZTriggleAnomalyHook
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        int num = 0;

        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if (applier == Owner)
            {
                await PowerCmd.Apply<StrengthPower>(playerChoice,
                    Owner, Amount, Owner, null);
                num += Amount;
            }
        }

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (num != 0)
            {
                await PowerCmd.Apply<StrengthPower>(choiceContext,
                    Owner, -num, Owner, null);
                num = 0;
            }
        }
    }
}
