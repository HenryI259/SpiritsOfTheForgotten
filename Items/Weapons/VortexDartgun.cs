using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using System;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class VortexDartgun: ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dart Blaster"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Fires a bonus lightning bolt if enemies are near");

		}


		public override void SetDefaults()
		{

			item.damage = 90;
			item.width = 40;
			item.height = 22;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 3;
			item.value = 70000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = true;
			item.shootSpeed = 20;
			item.ranged = true;
			item.noMelee = true;
			item.shoot = AmmoID.Dart;
			item.useAmmo = AmmoID.Dart;
			item.UseSound = SoundID.Item98;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .5f;
		}

		public override Vector2? HoldoutOffset()
					{
			return new Vector2(0, -3);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int NPC = Helper.NPCs.FindNearestNPC(Main.MouseWorld);
			if (NPC != -1) Projectile.NewProjectile(position, new Vector2(speedX, speedY), ModContent.ProjectileType<LunarLightning>(), damage, knockBack, player.whoAmI, NPC);
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(1.5f)), type, damage, knockBack, player.whoAmI);
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(-1.5f)), type, damage, knockBack, player.whoAmI);


			return false;

		}


		







		


	}
}