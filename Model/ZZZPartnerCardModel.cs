using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Pool;

namespace ZZZLib.Model
{
    /// <summary>
    /// 如果要用ZZZHook,不要继承这个类，请用IZZZModel接口
    /// </summary>
    [Pool(typeof(ZZZPartnerPool))]
    public abstract class ZZZPartnerCardModel
        (int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true) :
        ZZZCardModel(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
        protected static bool _draw = false;

        public override IEnumerable<CardKeyword> CanonicalKeywords =>
            [ZZZKeyWord.Partner];
        /*
        /// <summary>
        /// 受到伤害抽一张 伙伴
        /// </summary>
        /// <returns></returns>
        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
        {
            if (!_draw && target == Owner.Creature && result.UnblockedDamage > 0 &&
                Pile?.Type == PileType.Draw)
            {
                _draw = true;
                await CardPileCmd.Add(this, PileType.Hand);
            }
        }

        public override Task AfterDamageReceivedLate(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
        {
            _draw = false;
            return Task.CompletedTask;
        }*/
    }
}
