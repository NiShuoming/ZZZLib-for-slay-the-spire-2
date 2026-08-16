using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// 双生泣星 1 每触发一次属性异常，本回合获得1精通，最高4=6
    /// </summary>
    public class WeepingGemini:ZZZWEngineCardModel
    {
        public WeepingGemini():base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(1), new DynamicVar("Max", 4)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>(), 
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner.Creature,
                DynamicVars["Max"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Max"].UpgradeValueBy(2);
        }
    }
}
