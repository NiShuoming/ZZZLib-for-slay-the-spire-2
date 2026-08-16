using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Model
{
    /// <summary>
    /// 如果要用ZZZHook,不要继承这个类，请用IZZZModel接口
    /// </summary>
    public abstract class ZZZCardModel(int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true, bool auto = true) :
        CustomCardModel(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary, auto)

    {
        protected virtual string PortraitName => Id.Entry.RemovePrefix().ToSnakeCase();

        public override string CustomPortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);

        public override string PortraitPath =>
            PathHelper.GetCardImageBigPath(PortraitName);

    }
}
