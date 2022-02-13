using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Items;

namespace SpiritsOfTheForgotten.Minions.AuroraStick
{
	public class AuroraStickStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aurora Stick Staff");
			Tooltip.SetDefault("Summons a small stick that sticks to your enemies");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.knockBack = 0f;
			item.mana = 8;
			item.width = 38;
			item.height = 54;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item44;
			item.autoReuse = true;

			// Minion things
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<AuroraStickBuff>();
			item.shoot = ProjectileType<AuroraStickMinion>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}

	}
}