using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using SpiritsOfTheForgotten.Projectiles.WeaponProj;
using SpiritsOfTheForgotten;

namespace SpiritsOfTheForgotten.Items.Weapons
{
	public class CelestialStaff : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Celestial Judgement"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Launches different celestial pillars depending on attack mode\nRight click to change attack mode");
	
		}

		

		public override void SetDefaults() 
		{
			item.damage = 150;			
			item.width = 58;
			item.height = 58;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 10;
			item.value = 150000;
			item.rare = ItemRarityID.Expert;
			item.UseSound = SoundID.Item109;
			item.autoReuse = true;
			item.noMelee = true;
			Item.staff[item.type] = true;
			item.shootSpeed = 30;			
			item.shoot = ModContent.ProjectileType<MiniSolar>();
		}



		


		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public int CelestialAttack = 1;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            
        
			if (player.altFunctionUse == 2)
			{
				if (CelestialAttack == 4)
					CelestialAttack = 1;
				else
					CelestialAttack += 1;

				item.useStyle = ItemUseStyleID.SwingThrow;
				
				return false;
			}
			else
            {
				
				if (CelestialAttack == 1)
				{
					type = ModContent.ProjectileType<MiniSolar>();
					
				}

				else if (CelestialAttack == 2)
				{
					type = ModContent.ProjectileType<MiniVortex>();
					
				}
				else if (CelestialAttack == 3)
				{
					type = ModContent.ProjectileType<MiniNebula>();
					
				}

				else if (CelestialAttack == 4)
				{
					type = ModContent.ProjectileType<MiniStardust>();
					
				}
				item.useStyle = ItemUseStyleID.HoldingOut;
				Item.staff[item.type] = true;
				
				return true;
			}
			
			
        }

		

		
	}
}