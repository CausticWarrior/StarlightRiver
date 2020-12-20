﻿using Terraria;

using StarlightRiver.Core;

namespace StarlightRiver.Food.Content.Seasoning
{
    internal class SimpleSalt : Ingredient
    {
        public override string Texture => "StarlightRiver/Assets/Items/Food/SimpleSalt";

        public SimpleSalt() : base("Food buffs are 5% more effective", 2400, IngredientType.Seasoning) { }

        public override void BuffEffects(Player player, float multiplier)
        {
            player.GetModPlayer<FoodBuffHandler>().Multiplier += 0.05f;
        }
    }
}