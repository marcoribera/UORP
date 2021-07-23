using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	
	public class MagicSandals : BaseShoes
	{

		[Constructable]
		public MagicSandals() : base( 0x170d )
		{
                        Name = "Magical Sandals";
			Weight = 1.0;


                       int val = Utility.RandomList(1,2,3,4,5,6,7,8,9,10);
                       switch ( Utility.Random( 45 ) )   
			{

				case 0: SkillBonuses.SetValues(0, SkillName.Alquimia, val); break;
				case 1: SkillBonuses.SetValues(0, SkillName.Anatomia, val); break;
				case 3: SkillBonuses.SetValues(0, SkillName.ConhecimentoArmaduras, val); break;
				case 4: SkillBonuses.SetValues(0, SkillName.ConhecimentoArmas, val); break;
				case 5: SkillBonuses.SetValues(0, SkillName.Bloqueio, val); break;
				case 6: SkillBonuses.SetValues(0, SkillName.Carisma, val); break;
				case 7: SkillBonuses.SetValues(0, SkillName.Ferraria, val); break;
				case 8: SkillBonuses.SetValues(0, SkillName.Pacificar, val); break;
				case 9: SkillBonuses.SetValues(0, SkillName.Sobrevivencia, val); break;
				case 10: SkillBonuses.SetValues(0, SkillName.Carpintaria, val); break;
				case 11: SkillBonuses.SetValues(0, SkillName.Culinaria, val); break;
				case 12: SkillBonuses.SetValues(0, SkillName.Percepcao, val); break;
				case 13: SkillBonuses.SetValues(0, SkillName.Caos, val); break;
				case 14: SkillBonuses.SetValues(0, SkillName.PoderMagico, val); break;
				case 15: SkillBonuses.SetValues(0, SkillName.DuasMaos, val); break;
				case 16: SkillBonuses.SetValues(0, SkillName.Erudicao, val); break;
				case 17: SkillBonuses.SetValues(0, SkillName.UmaMao, val); break;
				case 18: SkillBonuses.SetValues(0, SkillName.Furtividade, val); break;
				case 19: SkillBonuses.SetValues(0, SkillName.Provocacao, val); break;
				case 20: SkillBonuses.SetValues(0, SkillName.Prestidigitacao, val); break;
				case 21: SkillBonuses.SetValues(0, SkillName.Arcanismo, val); break;
				case 22: SkillBonuses.SetValues(0, SkillName.ResistenciaMagica, val); break;
				case 23: SkillBonuses.SetValues(0, SkillName.Tocar, val); break;
				case 24: SkillBonuses.SetValues(0, SkillName.Envenenamento, val); break;
				case 25: SkillBonuses.SetValues(0, SkillName.Atirar, val); break;
				case 26: SkillBonuses.SetValues(0, SkillName.Necromancia, val); break;
				case 27: SkillBonuses.SetValues(0, SkillName.Costura, val); break;
				case 28: SkillBonuses.SetValues(0, SkillName.Adestramento, val); break;
				case 29: SkillBonuses.SetValues(0, SkillName.Agricultura, val); break;
				case 30: SkillBonuses.SetValues(0, SkillName.Adestramento, val); break;
				case 31: SkillBonuses.SetValues(0, SkillName.Veterinaria, val); break;
				case 32: SkillBonuses.SetValues(0, SkillName.Cortante, val); break;
				case 33: SkillBonuses.SetValues(0, SkillName.Contusivo, val); break;
				case 34: SkillBonuses.SetValues(0, SkillName.Perfurante, val); break;
				case 35: SkillBonuses.SetValues(0, SkillName.Briga, val); break;
				case 36: SkillBonuses.SetValues(0, SkillName.Extracao, val); break;
				case 37: SkillBonuses.SetValues(0, SkillName.PreparoFisico, val); break;
				case 38: SkillBonuses.SetValues(0, SkillName.Ordem, val); break;
				case 39: SkillBonuses.SetValues(0, SkillName.Bushido, val); break;
				case 40: SkillBonuses.SetValues(0, SkillName.Ninjitsu, val); break;
				case 41: SkillBonuses.SetValues(0, SkillName.Mecanica, val); break;
				case 42: SkillBonuses.SetValues(0, SkillName.Feiticaria, val); break;
				case 43: SkillBonuses.SetValues(0, SkillName.ImbuirMagica, val); break;
				case 44: SkillBonuses.SetValues(0, SkillName.Misticismo, val); break;


			}

		}

		public MagicSandals( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}