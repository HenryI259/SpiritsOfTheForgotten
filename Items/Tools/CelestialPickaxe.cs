using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Tools
{
	public class CelestialPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Pickaxe");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.melee = true;
			item.width = 62;
			item.height = 58;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useAnimation = 10;
			item.pick = 225;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.tileBoost += 4;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ModContent.ItemType<CelestialShard>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();



		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dusttype = Main.rand.Next(new int[] { DustID.BlueCrystalShard, DustID.SolarFlare, DustID.PinkFlame, DustID.Vortex });
			var dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, dusttype);
			dust.noGravity = true;
			
		}
	}
}
