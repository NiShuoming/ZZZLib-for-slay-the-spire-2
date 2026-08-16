using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;
using ZZZLib.Model;

namespace ZZZLib.Cards.Curse
{
    public class Demoralized : ZZZCardModel
    {
        public Demoralized() : base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true, false)
        {
        }
        public override bool CanBeGeneratedByModifiers => false;

        public override int MaxUpgradeLevel => 0;

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Retain, CardKeyword.Unplayable];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.SecondEnergy)];

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner.Creature) &&
                Owner.PlayerCombatState.Hand.Cards.Contains(this))
            {
                await ZZZCmd.AddSecondEnergy(Owner, -1, Owner.Creature, this);

            }
        }


    }
}
