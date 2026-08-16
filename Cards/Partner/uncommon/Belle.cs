using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;
using ZZZLib.Model;
using ZZZLib.Other;
using static Godot.XmlParser;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 铃 0 【伙伴】【消耗】=》不消耗 消耗1【青溟剑势】在消耗牌堆中选择1张牌加入手牌 完成
    /// </summary>
    public class Belle : ZZZPartnerCardModel
    {
        public Belle() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(ZZZKeyWord.SecondEnergy)];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, CardKeyword.Exhaust];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int num = Owner.PlayerCombatState.GetSecondEnergy(Owner).Energy2;
            if (num > 0)
            {
                await ZZZCmd.AddSecondEnergy(Owner, -1, Owner.Creature,this);
                CardSelectorPrefs prefs = new(SelectionScreenPrompt, DynamicVars.Cards.IntValue);
                CardPile pile = PileType.Exhaust.GetPile(Owner);
                CardModel cardModel = (await CardSelectCmd.FromSimpleGrid(choiceContext, pile.Cards, Owner, prefs)).FirstOrDefault();
                if (cardModel != null)
                {
                    await CardPileCmd.Add(cardModel, PileType.Hand);
                }
            }
        }

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Exhaust);
        }
    }

}
