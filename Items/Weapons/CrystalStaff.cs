using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;


namespace SpiritsOfTheForgotten.Items.Weapons
{
	class CrystalStaff : ModItem
	{

		

		public override void SetDefaults()
		{
			item.crit = 5;
			item.damage = 26;
			item.magic = true;
			item.width = 58;
			item.height = 58;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.noMelee = true;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item43;
			item.autoReuse = false;
			item.mana = 8;
			item.shootSpeed = 12;
			item.shoot = ModContent.ProjectileType<ShardBolt>();
			item.autoReuse = true;
			Item.staff[item.type] = true;
			item.value = 5000;
		}



	}
}
