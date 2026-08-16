using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    [Pool(typeof(ZZZEquipPool))]
    public abstract class ZZZDriveDiscsCardModel
    (int canonicalEnergyCost, CardType type, CardRarity rarity, TargetType targetType, bool shouldShowInCardLibrary = true) :
    ZZZCardModel(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
        protected override HashSet<CardTag> CanonicalTags => [ZZZTag.Equip,ZZZTag.DriveDiscs];
    }
}
