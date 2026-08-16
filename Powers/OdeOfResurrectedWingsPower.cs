using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Powers
{
    /// <summary>
    /// 空羽复归之诗 抽牌+1
    /// </summary>
    public class OdeOfResurrectedWingsPower : PowerModel, IZZZDrawCardHook
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public int ModifyDrawCard(PlayerChoiceContext choiceContext, decimal count, Player player, bool fromHandDraw)
        {
            if (player.Creature == Owner && !fromHandDraw)
            {
                return Amount;
            }
            return 0;
        }
    }
}
