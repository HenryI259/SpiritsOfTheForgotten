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
	public class ShockwaveStaff: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shockwave Staff");

		}

		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 64;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = 150000;
			item.rare = ItemRarityID.Red;
			item.autoReuse = false;
			item.noMelee = true;
			item.shootSpeed = 10;
			item.useAnimation = 12;
			item.useTime = 12;
			item.damage = 300;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Projectile explosion = Projectile.NewProjectileDirect(position, Vector2.Zero, ModContent.ProjectileType<Shockwave>(), damage, knockBack, player.whoAmI);// 1, 500);
			explosion.netUpdate = true;
			return false;
		}

		
	}
}