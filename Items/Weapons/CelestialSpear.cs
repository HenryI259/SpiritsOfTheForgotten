using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using SpiritsOfTheForgotten.Buffs;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Halibred");
			Tooltip.SetDefault("Fires a celestial beam");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 50;
			item.shootSpeed = 3.7f;
			item.knockBack = 6.5f;
			item.width = 92;
			item.height = 92;
			item.scale = 1f;
			item.rare = ItemRarityID.Red;
			item.value = 150000;

			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item45;
			item.shoot = ProjectileType<CelestialSpearProjectile>();
		}

		
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

		public int a = 0;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			/*
			float num12 = 12;
			int num14 = 30;
			Vector2 vector5 = new Vector2(speedX, speedY);
			vector5.Normalize();
			vector5 *= 80f;
			bool flag2 = Collision.CanHit(position, 0, 0, position + vector5, 0, 0);
			for (int num15 = 0; num15 < num14; num15++)
			{
				//float num16 = (float)num15 - ((float)num14 - 1f) / 2f;
				Vector2 vector6 = vector5.RotatedBy(MathHelper.ToRadians(num12 * num15));
				if (!flag2)
				{
					vector6 -= vector5;
				}
				int num17 = Projectile.NewProjectile(position.X + vector6.X, position.Y + vector6.Y, (Main.MouseWorld.X - position.X - vector6.X) * 1f, (Main.MouseWorld.Y - position.Y - vector6.Y) * 1f, ModContent.ProjectileType<CelestialDeathbeam>(), damage, knockBack, player.whoAmI);
				Main.projectile[num17].melee = true;
			}
			*/
			
			/*
			
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-15)), ModContent.ProjectileType<CelestialDeathbeam>(), damage, knockBack, player.whoAmI, MathHelper.ToRadians(1f));
			
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(15)), ModContent.ProjectileType<CelestialDeathbeam>(), damage, knockBack, player.whoAmI, MathHelper.ToRadians(-1f));
			*/
			///*
			Vector2 newSpeed = new Vector2(speedX, speedY);
			newSpeed.Normalize();
			newSpeed *= 30;
			position += newSpeed;

			Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<CelestialDeathbeam>(), damage, knockBack, player.whoAmI, MathHelper.ToRadians(0));

			for (int i = 0; i < 180; i++)
            {
				Vector2 velo = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i * 2)) * 7;
				var a = Dust.NewDustDirect(position, 1, 1, DustID.PlatinumCoin, velo.X, velo.Y, 50, default(Color), 2f);
				a.noGravity = true;
            }
			//*/

			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<CelestialShard>(), 1);
			recipe.AddIngredient(ItemID.NorthPole, 1);
			recipe.AddIngredient(ItemID.DarkLance, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
			
        
    }
}