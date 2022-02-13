using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CrystalFlail : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 56;
			item.height = 52;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.Blue;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 4f;
			item.damage = 9;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<CrystalFlailProjectile>();
			item.shootSpeed = 15.1f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.crit = 9;
			item.channel = true;
		}

	}
}
