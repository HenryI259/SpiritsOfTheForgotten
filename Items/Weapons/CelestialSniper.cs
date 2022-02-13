using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;
using static Terraria.ModLoader.ModContent;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialSniper : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Shoots a powerful luminite bullet");
		}

		public override void SetDefaults() 
		{
			item.damage = 1000;
			item.ranged = true;
			item.width = 128;
			item.height = 64;
			item.useTime = 50;
			item.useAnimation = 50;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 15;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item40;
			item.autoReuse = true;
			item.shootSpeed = 100;
			item.useAmmo = AmmoID.Bullet;
			item.noMelee = true;
			item.crit = 30;
			item.shoot = ProjectileID.Bullet;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SniperRifle);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileID.Bullet) 
			{
				type = ProjectileID.MoonlordBullet; 
			}
			return true; // return true to allow tmodloader to call Projectile.NewProjectile as normal
		}
	}
	
}