using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
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
    /// 啄木鸟电音 1 固有 施加易伤额外+1
    /// </summary>
    public class WoodpeckerElectro:ZZZDriveDiscsCardModel
    {
        public WoodpeckerElectro():base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
        {

        }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Count", 1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<VulnerablePower>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WoodpeckerElectroPower>(choiceContext,
                Owner.Creature, DynamicVars["Count"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }
    }
}
