using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	class CrystalTome : ModItem
	{
		public override void SetDefaults()
		{
			item.crit = 5;
			item.damage = 26;
			item.magic = true;
			item.width = 34;
			item.height = 40;
			item.value = 5000;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.noMelee = true;
			item.rare = ItemRarityID.Blue;
			item.autoReuse = false;
			item.UseSound = SoundID.Item43;
			item.mana = 8;
			item.shootSpeed = 12;
			item.shoot = ModContent.ProjectileType<CrystalWave>();
			item.autoReuse = true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			int shootSpeed = Main.rand.Next(10, 17);
			item.shootSpeed = shootSpeed;
			return true;
        }
    }
}