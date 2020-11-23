﻿using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;

namespace StarlightRiver.Tiles.Purified
{
    class WallStonePure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = DustType<Dusts.Purify>();
        }
    }
    class WallGrassPure : ModWall
    {
        public override void SetDefaults()
        {
            dustType = DustType<Dusts.Purify>();
        }
    }
}
