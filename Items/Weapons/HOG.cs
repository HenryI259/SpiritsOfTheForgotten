using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class HOG : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("H.O.G."); 
			Tooltip.SetDefault("Fires a spread of bullets\nHas a chance to belch a blast of lunar flames");

		}


		public override void SetDefaults()
		{

			item.damage = 77;
			item.crit = 14;
			item.width = 70;
			item.height = 32;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 12;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Red;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .33f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			
			if (Main.rand.Next(0, 11) == 0)
			{
				
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BelchedLunarFlame>(), damage, knockBack, player.whoAmI);
				return false;
			}
			else
			{
			
				int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7)); // 7 degree spread.
																												   
					float scale = 1f - (Main.rand.NextFloat() * .3f);
					perturbedSpeed = perturbedSpeed * scale;
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			
			return false;
			}
			
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			
			recipe.AddIngredient(ModContent.ItemType<POG>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialShard>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}










	}
}