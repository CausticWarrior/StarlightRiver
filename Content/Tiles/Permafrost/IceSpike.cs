﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Buffs;
using StarlightRiver.Items;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

using StarlightRiver.Core;

namespace StarlightRiver.Content.Tiles.Permafrost
{
    internal class IceSpike : ModTile
    {
        public override bool Dangersense(int i, int j, Player player) => true;

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => false;

        public override void SetDefaults()
        {
            QuickBlock.QuickSet(this, 0, DustType<Dusts.Glass3>(), SoundID.Shatter, new Color(200, 220, 255), -1);
            Main.tileLighted[Type] = false;
            TileID.Sets.DrawsWalls[Type] = true;
        }

        public override void FloorVisuals(Player player)
        {
            if (!player.HasBuff(BuffType<SpikeImmuneBuff>()))
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " got distracted by pretty colors..."), 15, 0);
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            float off = (float)Math.Sin((i + j) * 0.2f) * 300 + (float)Math.Cos(j * 0.15f) * 200;

            float sin = 0.3f + (float)Math.Sin(StarlightWorld.rottime + off * 0.008f * 0.2f) * 0.7f;
            float cos = 0.3f + (float)Math.Cos(StarlightWorld.rottime + off * 0.008f) * 0.7f;
            Color color = new Color(100 * (1 + sin) / 255f, 140 * (1 + cos) / 255f, 180 / 255f);

            spriteBatch.Draw(Main.tileTexture[tile.type], (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(tile.frameX, tile.frameY, 16, 16), color * 0.35f);
            spriteBatch.Draw(GetTexture("StarlightRiver/Tiles/Permafrost/IceSpikeGlow"), (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(tile.frameX, tile.frameY, 16, 16), Color.White * 0.1f);
            Lighting.AddLight(new Vector2(i, j) * 16, color.ToVector3() * 0.2f);
        }
    }

    internal class IceSpikeItem : QuickTileItem
    {
        public IceSpikeItem() : base("Permafrost Ice Shards", "", TileType<IceSpike>(), ItemRarityID.White) { }
    }
}
