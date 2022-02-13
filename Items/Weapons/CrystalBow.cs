using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;


namespace SpiritsOfTheForgotten.Items.Weapons
{
    class CrystalBow:ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Bow");
            Tooltip.SetDefault("It sparkles with every shot");
        }

        public override void SetDefaults()
        {
			item.crit = 3;
			item.damage = 15;
			item.ranged = true;
			item.width = 34;
			item.height = 54;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.noMelee = true;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.useAmmo = AmmoID.Arrow;
			item.shootSpeed = 40;
			item.shoot = AmmoID.Arrow;
			item.autoReuse = true;
			item.value = 5000;

		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.NextBool(3))
				Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5)), ModContent.ProjectileType<CrystalArrow>(), damage * 2, knockBack, player.whoAmI, 0, 0);
			return true;

		}

	}
}
