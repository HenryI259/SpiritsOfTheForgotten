using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten;
using System;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialSword2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heavens Edge");
			Tooltip.SetDefault("Fires a sword that homes on the mouse cursor and splits");

		}

		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 64;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.autoReuse = true;
			item.noMelee = false;
			item.melee = true;
			item.shootSpeed = 10;
			item.useAnimation = 12;
			item.useTime = 12;
			item.damage = 300;
			item.shoot = ModContent.ProjectileType<CelestialSwordBeam>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.EnchantedSword, 1);
			recipe.AddIngredient(ItemID.Seedler, 1);
			recipe.AddIngredient(ItemID.DD2SquireBetsySword, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}