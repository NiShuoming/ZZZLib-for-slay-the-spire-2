using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
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
    /// 云霓孤光 1 脆弱敌人4=6额外伤完成
    /// </summary>
    public class CloudcleaveRadiance : ZZZWEngineCardModel
    {
        public CloudcleaveRadiance() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DynamicVar("Count", 4m)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<FrailPower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<CloudcleaveRadiancePower>(choiceContext, Owner.Creature, DynamicVars["Count"].BaseValue,
                Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Count"].UpgradeValueBy(2m);
        }
    }
}
