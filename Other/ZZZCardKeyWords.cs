using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib
{
    public class ZZZKeyWord
    {
        /// <summary>
        /// 伙伴 受伤时抽一张伙伴牌到手牌
        /// </summary>
        [CustomEnum("Partner")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Partner;

        /// <summary>
        /// 派生 根据牌序效果不同
        /// </summary>
        [CustomEnum("Derive")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Derive;

        /// <summary>
        /// 架势 打出架势牌时切换效果
        /// </summary>
        [CustomEnum("Stance")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Stance;

        /// <summary>
        /// 第二能量
        /// </summary>
        [CustomEnum("Second Energy")]
        public static CardKeyword SecondEnergy;

        /// <summary>
        /// 终结技 某些情况下会触发最后一个效果
        /// </summary>
        [CustomEnum("Ultimate")]
        [KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Ultimate;

        /// <summary>
        /// 属性异常 受元素精通影响的伤害
        /// </summary>
        [CustomEnum("Anomaly")]
        public static CardKeyword Anomaly;

        /// <summary>
        /// 紊乱 
        /// </summary>
        [CustomEnum("Disorder")]
        public static CardKeyword Disorder;

        /// <summary>
        /// 异放 
        /// </summary>
        [CustomEnum("Abloom")]
        public static CardKeyword Abloom;

        /// <summary>
        /// 风化
        /// </summary>
        [CustomEnum("Windbloom")]
        public static CardKeyword Windbloom;
    }

    public class ZZZTag
    {
        [CustomEnum("Equip")]
        public static CardTag Equip;
        [CustomEnum("W_Engine")]
        public static CardTag W_Engine;
        [CustomEnum("Drive_Discs")]
        public static CardTag DriveDiscs;
    }

    public class ZZZValueProp
    {
        /// <summary>
        /// 异常伤害
        /// </summary>
        [CustomEnum("Anomaly")]
        public static ValueProp Anomaly;
    }
}
