﻿using Microsoft.Xna.Framework;
using StarlightRiver.Core;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items.Food
{
	internal class JumboShrimp : Ingredient
	{
		public JumboShrimp() : base("+10% damage and movement speed when underwater", 200, IngredientType.Side) { }

		public override void SafeSetDefaults()
		{
			Item.rare = ItemRarityID.Blue;
		}

		public override void BuffEffects(Player Player, float multiplier)
		{
			if (Player.wet)//unsure if this is for being in water
			{
				Player.GetDamage(DamageClass.Generic) += 0.1f * multiplier;
				Player.moveSpeed += Player.moveSpeed * (0.1f * multiplier);
			}
		}
	}
}