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
	public class CrystalSentryStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Northern Light Staff");

		}

		public override void SetDefaults()
		{
			item.width = 66;
			item.height = 66;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = 5000;
			item.rare = ItemRarityID.Red;
			item.autoReuse = false;
			item.noMelee = true;
			item.sentry = true;
			item.summon = true;
			item.shootSpeed = 10;
			item.useAnimation = 30;
			item.useTime = 30;
			item.damage = 0;
			item.shoot = ModContent.ProjectileType<CrystalSentry>();
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			position = Main.MouseWorld;
			speedX = 0f;
			speedY = 0f;
			
			return true;
        }

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}