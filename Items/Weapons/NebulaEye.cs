using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;
using System;
using System.Collections.Generic;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class NebulaEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Suspicious looking clump"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault(Helper.Text.ColorText("Ancient Artifact", Color.SteelBlue) + "\n" +
			   "The Nebula Creatures were the second species of creatures to ever appear in the universe\n" +
			   "Due to being formed of fairly unstable materials and surrounded by abundant materials they\n" +
			   "evoled quite quickly. They were some of the first creatures to evole into beings that looked\n" +
			   "totally different from each other and were possibly what led to the universe being as diverse\n" +
			   "as it is today");


		}


		public override void SetDefaults()
		{

			item.damage = 145;
			item.width = 32;
			item.height = 28;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = 70000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = false;
			item.shootSpeed = 10;
			item.magic = true;
			item.noMelee = true;
			item.shoot = ProjectileID.NebulaBolt;
			item.channel = true;
			item.mana = 12;
		}

		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-6, 0);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				for (int i = 0; i < 360; i++)
                {
					Vector2 velo = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i)) * 10;
					int a = Dust.NewDust(player.Center, player.width, player.height, DustID.PinkFlame, velo.X, velo.Y);
					Main.dust[a].noGravity = true;
                }
				player.Center += new Vector2(Main.rand.NextFloat(-450, 450), Main.rand.NextFloat(-450, 450));
				for (int i = 0; i < 360; i++)
				{
					Vector2 velo = Vector2.UnitY.RotatedBy(MathHelper.ToRadians(i)) * 10;
					int a = Dust.NewDust(player.Center, player.width, player.height, DustID.PinkFlame, velo.X, velo.Y);
					Main.dust[a].noGravity = true;
				}
				return false;
			}
			
			return true;

		}

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
				if (!player.HasBuff(BuffID.ChaosState))
                {
					item.useStyle = ItemUseStyleID.HoldingUp;
					player.AddBuff(BuffID.ChaosState, 180);
					return true;
				}
				return false;
            }
			else
            {
				item.useStyle = ItemUseStyleID.HoldingOut;
				return true;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line in tooltips)
			{
				if (line.Name == "ItemName")
				{
					line.overrideColor = Helper.Colors.LoreItem();
				}
			}
		}









	}
}