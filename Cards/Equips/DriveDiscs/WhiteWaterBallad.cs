using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// 沧浪行歌 1 开启【以太帷幕】时，获得的层数+1  =>开启一层 完成
    /// </summary>
    public class WhiteWaterBallad : ZZZDriveDiscsCardModel
    {
        public WhiteWaterBallad() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromPower<EtherVeil>()];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WhiteWaterBalladPower>(choiceContext, Owner.Creature, 1,
                Owner.Creature, this);
            if (IsUpgraded) await PowerCmd.Apply<EtherVeil>(choiceContext, Owner.Creature, 1,
                Owner.Creature, this);
        }
    }
}

