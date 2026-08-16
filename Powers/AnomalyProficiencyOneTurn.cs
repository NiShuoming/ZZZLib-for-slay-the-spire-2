using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
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
    /// 每回合精通
    /// </summary>
    public class AnomalyProficiencyOneTurn : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (power == this && amount!=0)
            {
                await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner,
                    amount, Owner, cardSource);
            }
        }
        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner))
            {
                await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner,
                    -Amount, Owner, null);
                await PowerCmd.Remove(this);
            }
        }

        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner))
            {
                await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner,
                    -Amount, Owner, null);
                await PowerCmd.Remove(this);
            }
        }
    }
}
