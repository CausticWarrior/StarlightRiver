﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;
using StarlightRiver.Content.Items;

namespace StarlightRiver.Content.Tiles.Vitric
{
    /*internal class VitricSand : ModTile
    {
        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustType<Content.Dusts.Air>(), SoundID.Dig, new Color(172, 131, 105), mod.ItemType("VitricSandItem"));
            Main.tileMerge[Type][TileType<VitricSpike>()] = true;
            Main.tileMerge[Type][mod.TileType("AncientSandstone")] = true;
            Main.tileMerge[Type][mod.TileType("VitricSoftSand")] = true;
            Main.tileMerge[Type][TileType<VitricMoss>()] = true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Color light = Lighting.GetColor(i, j);
            Color airColor = new Color(190, 255, 245);
            if (Main.rand.Next(200) == 0 && light.R >= 10 && light.B >= 10 && light.G >= 10)
            {
                Dust.NewDustPerfect(new Vector2(i * 16, j * 16), DustType<Content.Dusts.AirGravity>(), Vector2.Zero, 0, airColor * ((light.R + light.G + light.B) / 765f));
            }
        }
    }
    internal class VitricSandItem : QuickTileItem { public VitricSandItem() : base("Glassy Sand", "", StarlightRiver.Instance.TileType("VitricSand"), 0) { } }*/

    internal class VitricSandWall : ModWall { public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustID.Copper, SoundID.Dig, ItemType<VitricSandWallItem>(), false, new Color(114, 78, 80)); }

    internal class VitricSandWallItem : QuickWallItem { public VitricSandWallItem() : base("Vitric Sand Wall", "", WallType<VitricSandWall>(), 0) { } }
}

