using Terraria.ID;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace SpiritsOfTheForgotten.Items.Weapons
{
    class CrystalSaber:ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Saber");
            Tooltip.SetDefault("It shimmers with every swing");
        }
        public override void SetDefaults()
        {
			item.crit = 3;
			item.damage = 15;
			item.melee = true;
			item.width = 58;
			item.value = 5000;
			item.height = 62;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.shootSpeed = 40;
			item.autoReuse = true;
			item.useTurn = true;
		}
		

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (crit)
            {
				for (int i = 0; i < 3; i++)
				{
					int angle = Main.rand.Next(1, 360);
					int a = Projectile.NewProjectile(target.Center.X, target.Center.Y, (float)Math.Cos(angle * Math.PI / 180) * 2, (float)Math.Sin(angle * Math.PI / 180) * 2, ProjectileID.CrystalStorm, damage, 0, player.whoAmI);
					Main.projectile[a].penetrate = -1;
					Main.projectile[a].usesIDStaticNPCImmunity = true;
					Main.projectile[a].idStaticNPCHitCooldown = 10;
				}
			}
        }

    }
}
