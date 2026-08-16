using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZZLib
{
    public static class PathHelper
    {
        public static string GetCardImagePath(string name)
        {
            return "res://images/atlases/card_atlas.sprites/ZZZLib/" + name + ".tres";
        }
        public static string GetCardImageBigPath(string name)
        {
            return "res://images/packed/card_portraits/ZZZLib/" + name + ".png";
        }
        public static string GetRelicIconPath(string name)
        {
            return "res://images/atlases/relic_atlas.sprites/" + name + ".tres";

        }
        public static string GetRelicOutlinePath(string name)
        {
            return "res://images/relics/" + name + "_outline.tres";
        }

        public static string GetRelicBigIconPath(string name)
        {
            return "res://images/relics/" + name + ".png";
        }
    }
}
