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
	public class CelestialSentryStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Gizmo");
			Tooltip.SetDefault("Summons a prototype core that draws in stars to orbit it");

		}

		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 38;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.autoReuse = false;
			item.noMelee = true;
			item.sentry = true;
			item.summon = true;
			item.shootSpeed = 10;
			item.useAnimation = 12;
			item.useTime = 12;
			item.damage = 300;
			item.shoot = ModContent.ProjectileType<CelestialSentry>();
			
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