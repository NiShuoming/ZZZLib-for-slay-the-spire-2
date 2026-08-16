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
    /// 摇摆爵士p 每2回合获得1能量
    /// </summary>
    public class SwingJazzPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override int DisplayAmount => base.DynamicVars["Turn"].IntValue;

        public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

        protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("Turn", 2m)];

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (!participants.Contains(base.Owner) || !Owner.IsPlayer)
            {
                return;
            }
            base.DynamicVars["Turn"].BaseValue--;
            InvokeDisplayAmountChanged();
            if (base.DynamicVars["Turn"].IntValue <= 0)
            {
                await PlayerCmd.GainEnergy(1, Owner.Player);
                base.DynamicVars["Turn"].BaseValue = 2m;
                InvokeDisplayAmountChanged();
            }
        }
            
    }
}
