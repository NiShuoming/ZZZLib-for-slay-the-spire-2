using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Equips.DriveDiscs
{
    /// <summary>
    /// 谶语之誓 1 多段攻击数+1
    /// </summary>
    public class FeatheredFate : ZZZDriveDiscsCardModel
    {
        public FeatheredFate() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<FeatheredFatePower>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
