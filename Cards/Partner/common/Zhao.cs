using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 照 1 【伙伴】开启2=3层【以太帷幕】，获得3-5活力 完成
    /// </summary>
    public class Zhao : ZZZPartnerCardModel
    {
        public Zhao() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<Powers.EtherVeil>(2), new PowerVar<VigorPower>(3)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<Powers.EtherVeil>(),
            HoverTipFactory.FromPower<VigorPower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<Powers.EtherVeil>(choiceContext, Owner.Creature, DynamicVars.Power<Powers.EtherVeil>().IntValue,
                Owner.Creature, this);
            await PowerCmd.Apply<VigorPower>(choiceContext, Owner.Creature, DynamicVars.Power<VigorPower>().IntValue,
                Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Power<Powers.EtherVeil>().UpgradeValueBy(1);
            DynamicVars.Power<VigorPower>().UpgradeValueBy(2);
        }
    }

}
