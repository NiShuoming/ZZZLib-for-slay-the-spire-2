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

namespace ZZZLib.Cards.Equips.DriveDiscs
{
    /// <summary>
    /// 折枝剑歌 1 对debuff层数大于2（1）的敌人额外造成35%伤害。
    /// </summary>
    public class BranchBladeSong: ZZZDriveDiscsCardModel
    {
        public BranchBladeSong() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("debuff", 2), new DynamicVar("damageUp", 35)];

        protected override void OnUpgrade()
        {
            DynamicVars["debuff"].UpgradeValueBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<BranchBladeSongP>(choiceContext, Owner.Creature,
                DynamicVars["debuff"].BaseValue, Owner.Creature, this);


        }
    }
}
