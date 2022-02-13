using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;

namespace SpiritsOfTheForgotten.Items
{
	public class CelestialShard : ModItem
	{
		public override void SetStaticDefaults() {

			DisplayName.SetDefault("Celestial Catalyst");
			Tooltip.SetDefault("A powerful material originally used by the kingdom of Caelesti to power their machines");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 20));
		}

		public override void SetDefaults() {
			item.width = 48;
			item.height = 46;
			item.maxStack = 999;
			item.value = 30000;
			item.rare = ItemRarityID.Red;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 5);
			recipe.AddIngredient(ItemID.FragmentStardust, 5);
			recipe.AddIngredient(ItemID.FragmentSolar, 5);
			recipe.AddIngredient(ItemID.FragmentVortex, 5);
			recipe.AddIngredient(ItemID.FragmentNebula, 5);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();



		}
	}
}
