using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Model;

namespace ZZZLib.Cards.Partner
{
    /// <summary>
    /// 叶释渊 2 【伙伴】【消耗】清除自身3->5层负面buff
    /// </summary>
    public class YeShiyuan : ZZZPartnerCardModel
    {
        public YeShiyuan() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DynamicVar("Count", 3m)];

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner, CardKeyword.Exhaust];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            IEnumerable<PowerModel> list = Owner.Creature.Powers;
            foreach (PowerModel p in list.ToList())
            {
                if (p.Type == PowerType.Debuff)
                    await PowerCmd.ModifyAmount(choiceContext, p,
                        -Math.Min(DynamicVars["Count"].BaseValue, p.Amount),
                        Owner.Creature, this);
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Count"].UpgradeValueBy(2m);
        }

    }
}