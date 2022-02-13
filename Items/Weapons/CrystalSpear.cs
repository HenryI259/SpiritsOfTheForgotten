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
	public class CrystalSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 50;
			item.shootSpeed = 3.7f;
			item.knockBack = 6.5f;
			item.width = 92;
			item.height = 92;
			item.scale = 1f;
			item.rare = ItemRarityID.Blue;
			item.value = 5000;
			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<CrystalSpearProjectile>();
		}

		
		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

		

	
			
        
    }
}