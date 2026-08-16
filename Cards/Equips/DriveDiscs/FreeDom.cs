using BaseLib.Extensions;
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

namespace ZZZLib.Cards.Equips.DriveDiscs
{
    /// <summary>
    /// 自由蓝调 1 异常精通+2（3）
    /// </summary>
    public class FreeDom:ZZZDriveDiscsCardModel
    {
        public FreeDom() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext,
                Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(1);
        }
    }
}
