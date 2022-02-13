//Yes ik all the fish code is really scuffed it was like my first weapon but it works so Im not messing with it

using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class TheFish : ModItem
	{
		public override void SetStaticDefaults() 
		{
			
			Tooltip.SetDefault("DAAAAAAAAAAAAAAAAAA");
		}

		public override void SetDefaults() 
		{
			item.damage = 40;
			item.width = 28;
			item.height = 20;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = 6900;
			item.rare = ItemRarityID.Expert;
			item.autoReuse = true;
			item.shoot = ProjectileID.MiniSharkron;
			item.shootSpeed = 30;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // 10 degree spread.
																												
				float scale = 1f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				Main.projectile[a].minion = false;
				Main.projectile[a].melee = false;
				Main.projectile[a].ranged = false;
				Main.projectile[a].magic = false;
			}
			return false; // return false because we don't want tmodloader to shoot projectile
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FishCostumeMask, 1);
			recipe.AddIngredient(ItemID.Bass, 50);
			recipe.AddIngredient(ItemID.RoyalGel);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddTile(TileID.FishBowl);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}


	}
}