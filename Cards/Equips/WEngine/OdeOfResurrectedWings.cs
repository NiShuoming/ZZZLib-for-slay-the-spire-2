using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Equips.WEngine
{
    /// <summary>
    /// 空羽复归之诗 2=1 多抽1
    /// </summary>
    public class OdeOfResurrectedWings : ZZZWEngineCardModel
    {
        public OdeOfResurrectedWings() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CardsVar(1)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<OdeOfResurrectedWingsPower>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
