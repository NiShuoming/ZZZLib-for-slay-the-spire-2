using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Cmds
{
    public class ZZZStanceModel : CustomSingletonModel
    {
        public ZZZStanceModel() : base(HookType.Combat)
        {
            CardUpgrade = new Dictionary<CardModel, int>();

        }

        public override bool ShouldReceiveCombatHooks => true;

        public Dictionary<CardModel, int> CardUpgrade = new Dictionary<CardModel, int>();

        public override Task BeforeCombatStart()
        {
            CardUpgrade.Clear();
            return base.BeforeCombatStart();
        }

        /// <summary>
        /// 打出牌时，升级或降级1张牌
        /// </summary>
        /// <param name="choiceContext"></param>
        /// <param name="cardPlay"></param>
        /// <returns></returns>
        public override Task AfterCardPlayedLate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = PileType.Hand.GetPile(cardPlay.Player).Cards.ToList();

            foreach(var card in cards)
            {
                if (card.Keywords.Contains(ZZZKeyWord.Stance))
                {
                    int n = TryUpgrade(card);
                    if(n!=0)
                    {
                        CardUpgrade[card] = CardUpgrade.GetValueOrDefault(card) + n;
                    }
                }
            }
            return Task.CompletedTask;
        }

        private int TryUpgrade(CardModel card)
        {
            if (CombatManager.Instance.IsEnding)
                return 0;

            if (card.IsUpgradable)
            {
                CardCmd.Upgrade(card);
                return 1;
            }
            else
            {
                return Downgrade(card);
                
            }
        }

        private int Downgrade(CardModel card, int d = 1)
        {
            if (d <= 0) return 0;
            if (CombatManager.Instance.IsEnding) return 0;

            int n = card.CurrentUpgradeLevel;
            if (n <= 0) return 0;

            int target = Math.Max(0, n - d);

            CardCmd.Downgrade(card);

            for (int i = 0; i < target; i++)
            {
                CardCmd.Upgrade(card, CardPreviewStyle.None);
            }
            return target - n;
        }

        public override Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            var toRestore = CardUpgrade
                .Where(kv => participants.Contains(kv.Key.Owner.Creature))
                .ToList();
            if (CombatManager.Instance.IsEnding) return Task.CompletedTask;
            foreach (var kv in toRestore)
            {
                CardModel card = kv.Key;

                // 还原到原始等级：用 delta 的相反方向
                if (kv.Value < 0)
                {
                    // 记录是负 → 之前降过级 → 现在升级还原
                    int times = - kv.Value;
                    for (int i = 0; i < times; i++)
                        CardCmd.Upgrade(card, CardPreviewStyle.None);
                }
                else if (kv.Value > 0)
                {
                    // 记录是正 → 之前升过级 → 现在降级还原
                    Downgrade(card, kv.Value);
                }

                CardUpgrade.Remove(card);
            }
            return Task.CompletedTask;
        }
    }
}
