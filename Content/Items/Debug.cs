﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Content.GUI;
using StarlightRiver.Content.Tiles.Underground.EvasionShrineBullets;
using StarlightRiver.Core;
using StarlightRiver.Core.Loaders;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items
{
	class DebugStick : ModItem
    {
        public override string Texture => AssetDirectory.Assets+ "Items/DebugStick";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Stick");
            Tooltip.SetDefault("Dont use this if you're not me.");
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 38;
            Item.height = 40;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item18;
            Item.useTurn = true;
            Item.accessory = true;

            Item.createTile = ModContent.TileType<Tiles.Vitric.VitricDecor2x1>();
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
            player.GetModPlayer<ResourceReservationPlayer>().ReserveLife(200);

            Dust.NewDustPerfect(Main.MouseWorld, ModContent.DustType<Dusts.AuroraWater>(), Vector2.Zero, 0, new Color(200, 220, 255) * 0.4f, 1);
        }

		public override bool? UseItem(Player player)
        {
            player.GetModPlayer<Abilities.AbilityHandler>().InfusionLimit = 3;
            return true;

            StarlightWorld.FlipFlag(WorldFlags.VitricBossDowned);
            return true;

            player.GetModPlayer<Abilities.AbilityHandler>().Lock<Abilities.ForbiddenWinds.Dash>();
            return true;

            if (ZoomHandler.ExtraZoomTarget == 0.8f)
            {
                ZoomHandler.SetZoomAnimation(0.65f);
                Main.NewText("Zoom: 65%");
            }

            else if (ZoomHandler.ExtraZoomTarget == 0.65f)
            {
                ZoomHandler.SetZoomAnimation(0.5f);
                Main.NewText("Zoom: 50%");
            }

            else if (ZoomHandler.ExtraZoomTarget == 0.5f)
            {
                ZoomHandler.SetZoomAnimation(1);
                Main.NewText("Zoom: Default");
            }

            else
            {
                ZoomHandler.SetZoomAnimation(0.8f);
                Main.NewText("Zoom: 80%");
            }

            return true;

            for (int x = 0; x < Main.maxTilesX; x++)
                for (int y = 0; y < Main.maxTilesY; y++)
                    Main.tile[x, y].ClearEverything();

            return true;

            Player dummy = new Player();
            dummy.active = true;

            var Item = new Item();
            Item.SetDefaults(ModContent.ItemType<UndergroundTemple.TempleRune>());

            dummy.armor[5] = Item;
            dummy.Center = Main.LocalPlayer.Center;

            Main.player[1] = dummy;
            dummy.team = Main.LocalPlayer.team;
            dummy.name = "Johnathan Testicle";
            dummy.statLife = 400;
            dummy.statLifeMax = 500;

            return true;

            /*
            foreach (NPC NPC in Main.npc)
                NPC.active = false;

            NPC.NewNPC((StarlightWorld.VitricBiome.X) * 16, (StarlightWorld.VitricBiome.Center.Y + 10) * 16, ModContent.NPCType<Bosses.GlassMiniboss.GlassweaverWaiting>());
            Player.Center = new Vector2((StarlightWorld.VitricBiome.X) * 16, (StarlightWorld.VitricBiome.Center.Y + 10) * 16);

            return true;*/
        }

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color ItemColor, Vector2 origin, float scale)
		{
            return;

			var target = new Rectangle(50, 160, Main.screenWidth / 10, Main.screenHeight / 10);
            var target2 = new Rectangle(50, 160 + Main.screenHeight / 10 * 1 + 20, Main.screenWidth / 10, Main.screenHeight / 10);
            var target3 = new Rectangle(50, 160 + Main.screenHeight / 10 * 2 + 40, Main.screenWidth / 10, Main.screenHeight / 10);

            var targetO = new Rectangle(48, 158, Main.screenWidth / 10 + 4, Main.screenHeight / 10 + 4);
            var targetO2 = new Rectangle(48, 158 + Main.screenHeight / 10 * 1 + 20, Main.screenWidth / 10 + 4, Main.screenHeight / 10 + 4);
            var targetO3 = new Rectangle(48, 158 + Main.screenHeight / 10 * 2 + 40, Main.screenWidth / 10 + 4, Main.screenHeight / 10 + 4);

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetO, Color.Black);
            spriteBatch.Draw(Main.screenTarget, target, Color.White);

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetO2, Color.Black);
            spriteBatch.Draw(StarlightRiver.LightingBufferInstance.ScreenLightingTexture, target2, Color.White);

            spriteBatch.Draw(TextureAssets.MagicPixel.Value, targetO3, Color.Black);
            spriteBatch.Draw(StarlightRiver.LightingBufferInstance.TileLightingTexture, target3, Color.White);
        }
	}

    class DebugModerEnabler : ModItem
	{
        public override string Texture => AssetDirectory.Assets + "Items/DebugStick";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Debug Mode");
            Tooltip.SetDefault("Enables debug mode which does... stuff!\nHold Y to make bosses go at ludicrous speed.");
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }

        public override bool? UseItem(Player Player)
        {
            StarlightRiver.DebugMode = !StarlightRiver.DebugMode;
            return true;
        }
    }

    class Eraser : ModItem
	{
        public override string Texture => AssetDirectory.Assets + "Items/DebugStick";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eraser");
            Tooltip.SetDefault("Death");
        }

        public override void SetDefaults()
        {
            Item.damage = 10;
            Item.DamageType = DamageClass.Melee;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 2;
            Item.useAnimation = 2;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.value = 1000;
            Item.rare = ItemRarityID.LightRed;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item18;
            Item.useTurn = true;
            Item.accessory = true;
        }

        public override bool? UseItem(Player Player)
        {
            foreach (NPC NPC in Main.npc.Where(n => Vector2.Distance(n.Center, Main.MouseWorld) < 100))
                NPC.StrikeNPC(99999, 0, 0, false, false, false);
            return true;
        }
    }
}
