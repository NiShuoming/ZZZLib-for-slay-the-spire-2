using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 3力量，x回合
    /// </summary>
    public class CheerOn : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<StrengthPower>(3)];

        public override async Task BeforeApplied(Creature target, decimal amount, Creature? applier, CardModel? cardSource)
        {
            await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(),
                target, DynamicVars.Power<StrengthPower>().BaseValue, target, cardSource);
        }

        public override async Task AfterRemoved(Creature oldOwner)
        {
            await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(),
                oldOwner, -DynamicVars.Power<StrengthPower>().BaseValue, oldOwner, null);

        }
        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (side == CombatSide.Enemy)
            {
                await PowerCmd.TickDownDuration(this);
            }
        }

    }
}
