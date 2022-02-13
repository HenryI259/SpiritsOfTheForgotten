using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class POG: ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("P.O.G."); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Fires a spread of bullets\nPoggers");

		}


		public override void SetDefaults()
		{

			item.damage = 18;
			item.width = 70;
			item.height = 28;
			item.useTime = 14;
			item.useAnimation = 14;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = 70000;
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item36;
			item.autoReuse = true;
			item.shootSpeed = 10;
			item.ranged = true;
			item.noMelee = true;
			item.rare = ItemRarityID.Pink;
			item.shoot = ProjectileID.Bullet;
			item.useAmmo = AmmoID.Bullet;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .25f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 4 + Main.rand.Next(2); // 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 velo = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(7)); // 7 degree spread.
				float scale = 1f - (Main.rand.NextFloat() * .3f); // varies the velocity of each shot
				velo *= scale; 
				Projectile.NewProjectile(position.X, position.Y, velo.X, velo.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ItemID.Bacon, 5);
			recipe.AddIngredient(ItemID.PiggyBank);
			recipe.AddIngredient(ItemID.Shotgun);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}







		


	}
}