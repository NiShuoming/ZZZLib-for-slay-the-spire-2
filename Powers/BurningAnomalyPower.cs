using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    internal class BurningAnomalyPower : PowerModel, IHealthBarForecastSource
    {
        public override PowerType Type => PowerType.Debuff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public IEnumerable<HealthBarForecastSegment> GetHealthBarForecastSegments(HealthBarForecastContext context)
        {
            HealthBarForecastSegment healthBar = new HealthBarForecastSegment
                (((int)GetAmount()), new Godot.Color(1, 0, 0), HealthBarForecastDirection.FromLeft);
            return [healthBar];
        }

        public decimal GetAmount()
        {
            return Hook.ModifyDamage(Owner.CombatState.RunState, Owner.CombatState,
                Owner, Applier, Amount, ZZZValueProp.Anomaly | ValueProp.Unpowered,
                 null,null, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
        }

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(base.Owner))
            {
                await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount,
                    ValueProp.Unblockable | ValueProp.Unpowered | ZZZValueProp.Anomaly,
                    Applier, null,null);
                if (base.Owner.IsAlive)
                {
                    await PowerCmd.Decrement(this);
                }
                else
                {
                    await Cmd.CustomScaledWait(0.1f, 0.25f);
                }
            }
        }
        public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier, CardModel? cardSource)
        {
            if (power == this && applier != null)
            {
                Applier = applier;
            }
            return Task.CompletedTask;
        }
    }
}
