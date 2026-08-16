using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Cmds
{
    /// <summary>
    /// 伙伴受伤抽牌
    /// </summary>
    public class ZZZPartnerDrawModel : CustomSingletonModel
    {
        public ZZZPartnerDrawModel() : base(HookType.Combat)
        {
        }

        public override bool ShouldReceiveCombatHooks => true;

        public override async Task AfterDamageReceivedLate(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (target == null || !target.IsPlayer || target.IsDead || result.UnblockedDamage <= 0) return;
            CardModel card = target.Player.PlayerCombatState.DrawPile.Cards.Where(
                a => a.Keywords.Contains(ZZZKeyWord.Partner)).FirstOrDefault();

            if (card != null)
            {
                await CardPileCmd.Add(card, PileType.Hand);
            }
        }
    }
}
