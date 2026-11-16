using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 卡吕冬之子 1  【伙伴】获得3(4)覆甲、2（4）【异常精通】
    /// </summary>
    public class SonsOfCalydon:ZZZPartnerCardModel
    {
        public SonsOfCalydon() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<PlatingPower>(3), new PowerVar<AnomalyProficiency>(2)];
        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<PlatingPower>(), 
            HoverTipFactory.FromPower<AnomalyProficiency>()];
        protected override void OnUpgrade()
        {
            DynamicVars.Power<PlatingPower>().UpgradeValueBy(1);
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<PlatingPower>(choiceContext, Owner.Creature,
                DynamicVars.Power<PlatingPower>().BaseValue, Owner.Creature,
                this);

            await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner.Creature,
                DynamicVars.Power<AnomalyProficiency>().BaseValue, Owner.Creature,
                this);
        }
    }
}
