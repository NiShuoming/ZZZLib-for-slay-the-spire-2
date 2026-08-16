using BaseLib.Abstracts;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib.Pool
{
    public class ZZZEquipPool : CustomCardPoolModel
    {
        public override string Title => "equip";

        public override Color DeckEntryCardColor => new Color("A3A3A3FF");

        public override bool IsColorless => false;

        public override string EnergyColorName => "colorless";

        public override string CardFrameMaterialPath => "card_frame_colorless";
    }
}
