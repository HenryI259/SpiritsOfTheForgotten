//Yes ik all the fish code is really scuffed it was like my first weapon but it works so Im not messing with it

using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class TheFishMk4 : ModItem
	{
		public override void SetStaticDefaults() 
		{
			
			Tooltip.SetDefault("DAAAAAAAAAAAAAAAAAA x4");
		}

		
		public override void SetDefaults() 
		{
			
			
			item.width = 28;
			item.height = 20;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = 690000;
			item.rare = ItemRarityID.Expert;
			item.autoReuse = true;
			item.shootSpeed = 10;
			item.shoot = ProjectileID.MiniSharkron;
		}


		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse != 2)
			{
				if (Main.rand.Next(0, 10) == 0)
                {
					type = ProjectileID.LunarFlare;
					damage = 700;
                }
				else
				{
					type = ProjectileID.MiniSharkron;
					damage = 350;
				}
				if (Main.rand.Next(0, 10) == 0)
				{


					int numberProjectiles = 150; 
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(360)); // 360 degree spread.
																														 
						float scale = 1f - (Main.rand.NextFloat() * .3f);
						perturbedSpeed *= scale; 
						int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 3f, perturbedSpeed.Y * 3f, type, damage, knockBack, player.whoAmI);
						Main.projectile[a].minion = false;
						Main.projectile[a].melee = false;
						Main.projectile[a].ranged = false;
						Main.projectile[a].magic = false;
					}
					
				}
				
				else
				{
					
					
					int numberProjectiles = 6 + Main.rand.Next(2); 
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
																														 
						float scale = 1f - (Main.rand.NextFloat() * .3f);
						perturbedSpeed *= scale; 
						int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 3f, perturbedSpeed.Y * 3f, type, damage, knockBack, player.whoAmI);
						Main.projectile[a].minion = false;
						Main.projectile[a].melee = false;
						Main.projectile[a].ranged = false;
						Main.projectile[a].magic = false;
					}
					
				}
				
			}
			else
            {
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				
				int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.Typhoon, 1100, knockBack, player.whoAmI);
				Main.projectile[a].minion = false;
				Main.projectile[a].melee = false;
				Main.projectile[a].ranged = false;
				Main.projectile[a].magic = false;
				

			}
			return false;
		}
		

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TheFishMk3>(), 1);
			recipe.AddIngredient(ItemID.Bass, 50);
			recipe.AddIngredient(ItemID.SuspiciousLookingTentacle, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.AddTile(TileID.FishBowl);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				
				item.useTime = 15;
				item.useAnimation = 15;
				
				item.UseSound = SoundID.Item84;
				
			}
			else
			{
				
				item.useTime = 10;
				item.useAnimation = 10;
				
			}
			return base.CanUseItem(player);
		
			
			
		}


	}
}