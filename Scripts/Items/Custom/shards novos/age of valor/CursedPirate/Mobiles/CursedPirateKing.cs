/**********************************************************
RunUO 2.0 AoV C# script file
Official Age of Valor Script :: www.uovalor.com
Last modified by 
Filepath: scripts\_custom\engines\CursedPirate\Mobiles\CursedPirateKing.cs
Lines of code: 147
***********************************************************/


using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("corpse of the cursed Pirate King")]
    public class CursedPirateKing : BaseCreature
    {
        public override bool ShowFameTitle { get { return false; } }

        public override WeaponAbility GetWeaponAbility()
        {
            switch (Utility.Random(3))
            {
                default:
                case 0: return WeaponAbility.DoubleStrike;
                case 1: return WeaponAbility.WhirlwindAttack;
                case 2: return WeaponAbility.CrushingBlow;
            }
        }

        [Constructable]
        public CursedPirateKing() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Bart Roberts";
            Title = "The Cursed Pirate King";
            Body = 0x190;
            Hue = Utility.RandomMinMax(0x8596, 0x8599);
            BaseSoundID = 0x165;

            Cutlass cutlass = new Cutlass();
            cutlass.Movable = false;
            cutlass.Hue = 0x497;
            cutlass.Skill = SkillName.Cortante;
            AddItem(cutlass);

            FancyShirt shirt = new FancyShirt();
            shirt.Movable = false;
            shirt.Hue = 1153;
            AddItem(shirt);

            LongPants pants = new LongPants();
            pants.Movable = false;
            pants.Hue = 0x497;
            AddItem(pants);

            ThighBoots boots = new ThighBoots();
            boots.Movable = false;
            boots.Hue = 0x497;
            AddItem(boots);

            GoldEarrings earrings = new GoldEarrings();
            earrings.Movable = false;
            earrings.Hue = 0x35;
            AddItem(earrings);

            TricorneHat hat = new TricorneHat();
            hat.Movable = false;
            hat.Hue = 0x497;
            AddItem(hat);

            SetStr(400, 560);
            SetDex(90, 120);
            SetInt(300, 460);

            SetHits(6000, 12000);
            SetMana(5000);

            SetDamage(16, 34);

            SetDamageType(ResistanceType.Physical, 75);
            SetDamageType(ResistanceType.Poison, 25);

            SetResistance(ResistanceType.Physical, 40, 60);
            SetResistance(ResistanceType.Fire, 60, 80);
            SetResistance(ResistanceType.Cold, 40, 60);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 40, 65);

            SetSkill(SkillName.Anatomia, 120.0);
            SetSkill(SkillName.PoderMagico, 100.0);
            SetSkill(SkillName.Arcanismo, 100.0);
            SetSkill(SkillName.Feiticaria, 120.0);
            SetSkill(SkillName.ResistenciaMagica, 100.0);
            SetSkill(SkillName.Anatomia, 100.0);
            SetSkill(SkillName.Briga, 120.0);
            SetSkill(SkillName.Cortante, 160.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 60;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich, 2);
        }

        public override bool OnBeforeDeath()
        {
            this.Say("Arrr, I'll be seein ye in Davey Jones' Locker... Arrr!");

            this.AddItem(new Gold(2500));
			/* switch (Utility.Random(50))
            {
                case 0: PackItem(new CursedPirateRing()); break;
                case 1: PackItem(new CursedPirateEarrings()); break;
                case 2: PackItem(new CursedPirateCutlass()); break;
                case 3: PackItem(new CursedPirateTricorne()); break;
                case 4: PackItem(new CursedPirateShirt()); break;
                case 5: PackItem(new CursedPirateBoots()); break;
                case 6: PackItem(new CursedPiratePants()); break;
            }*/

            return base.OnBeforeDeath();

        }

        public override bool AlwaysMurderer { get { return true; } }
        public override bool CanRummageCorpses { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 5; } }

        private static bool m_InHere;
        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (from != null && from != this && !m_InHere)
            {
                m_InHere = true;
                AOS.Damage(from, this, Utility.RandomMinMax(8, 20), 100, 0, 0, 0, 0);

                MovingEffect(from, 0xECA, 10, 0, false, false, 0, 0);
                PlaySound(0x491);

                if (0.025 > Utility.RandomDouble())
                    Timer.DelayCall(TimeSpan.FromSeconds(1.0), new TimerStateCallback(CreateBottles_Callback), from);

                m_InHere = false;
            }
        }

        public virtual void CreateBottles_Callback(object state)
        {
            Mobile from = (Mobile)state;
            Map map = from.Map;

            if (map == null)
                return;

            int count = Utility.RandomMinMax(1, 1);

            for (int i = 0; i < count; ++i)
            {
                int x = from.X + Utility.RandomMinMax(-1, 1);
                int y = from.Y + Utility.RandomMinMax(-1, 1);
                int z = from.Z;

                if (!map.CanFit(x, y, z, 16, false, true))
                {
                    z = map.GetAverageZ(x, y);

                    if (z == from.Z || !map.CanFit(x, y, z, 16, false, true))
                        continue;
                }

                CursedRumBottles bottles = new CursedRumBottles();

                bottles.Hue = 0;
                bottles.Name = "Cursed bottles of rum";
                bottles.ItemID = Utility.Random(0x99D, 2);

                bottles.MoveToWorld(new Point3D(x, y, z), map);
            }
        }

        public CursedPirateKing(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}
