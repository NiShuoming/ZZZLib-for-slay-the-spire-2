using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;
using ZZZLib.Powers;

namespace ZZZLib.Cards.Equips.Other
{
    /// <summary>
    /// 失乐园 1 触发【属性异常】是，获得1（2）力量
    /// </summary>
    public class ParadiseLost : ZZZEquipCardModel
    {
        public ParadiseLost() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<StrengthPower>(1)];
        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<StrengthPower>()];
        protected override void OnUpgrade()
        {
            DynamicVars.Power<StrengthPower>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ParadiseLostPower>(choiceContext,
                Owner.Creature, DynamicVars.Power<StrengthPower>().BaseValue,
                Owner.Creature, this);
        }
    }
}
