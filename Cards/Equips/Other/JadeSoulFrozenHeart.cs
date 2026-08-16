using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
    /// 玉魄冰心  1 【以太帷幕】额外增加18%->35%伤害 完成
    /// </summary>
    public class JadeSoulFrozenHeart : ZZZEquipCardModel
    {
        public JadeSoulFrozenHeart() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<EtherVeil>(18m)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<EtherVeil>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<JadeSoulFrozenHeartPower>(choiceContext, Owner.Creature,
                DynamicVars.Power<EtherVeil>().BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Power<EtherVeil>().UpgradeValueBy(17m);
        }
    }
}
