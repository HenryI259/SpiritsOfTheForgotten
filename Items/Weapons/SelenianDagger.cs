using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten;
using System;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class SelenianDagger : ModItem
	{
		public override void SetStaticDefaults()
		{

            Tooltip.SetDefault(Helper.Text.ColorText("Ancient Artifact", Color.SteelBlue) + "\n" +
				"A powerful dagger wielded only by the strongest solar beings, the selenians.\n " +
				"These daggers allow the user to dash in any direction reflecting all projectiles\n " +
				"in its path. However only the selenians can handle wielding two daggers at once\n " +
				"allowing them to be unharmed by any projectiles they reflect");



		}

		public override void SetDefaults()
		{
			item.width = 38;
			item.height = 38;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 5;
			item.value = 150000;
			item.rare = ItemRarityID.Cyan;
			item.autoReuse = true;
			item.noMelee = false;
			item.melee = true;
			item.useAnimation = 7;
			item.useTime = 7;
			item.damage = 150;
			item.useTurn = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
			

		public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
				if (!player.HasBuff(BuffID.OnFire))
				{
					item.noUseGraphic = true;
					item.noMelee = true;
					Vector2 velo = Main.MouseWorld - player.Center;
					velo.Normalize();
					velo *= 20f;
					player.velocity = velo;
					player.AddBuff(BuffID.OnFire, (Main.expertMode ? 90 : 180));
					player.GetModPlayer<SotfPlayer>().SolarTimer = 40;
					return true;
				}
				return false;
			}
			else
            {
				item.noUseGraphic = false;
				item.noMelee = false;
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