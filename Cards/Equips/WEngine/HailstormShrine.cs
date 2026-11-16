using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// 霰落星殿 2 【】=【保留】群体伤害会额外攻击1次
    /// </summary>
    public class HailstormShrine : ZZZWEngineCardModel
    {
        public HailstormShrine() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<HailstormShrineP>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
