using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("a stone harpy corpse")]
    public class StoneHarpy : BaseCreature
    {
        [Constructable]
        public StoneHarpy()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "harpia de pedra";
            this.Body = 73;
            this.BaseSoundID = 402;

            this.SetStr(296, 320);
            this.SetDex(86, 110);
            this.SetInt(51, 75);

            this.SetHits(178, 192);
            this.SetMana(0);

            this.SetDamage(8, 16);

            this.SetDamageType(ResistanceType.Physical, 75);
            this.SetDamageType(ResistanceType.Poison, 25);

            this.SetResistance(ResistanceType.Physical, 45, 55);
            this.SetResistance(ResistanceType.Fire, 20, 30);
            this.SetResistance(ResistanceType.Cold, 10, 20);
            this.SetResistance(ResistanceType.Poison, 30, 40);
            this.SetResistance(ResistanceType.Energy, 30, 40);

            this.SetSkill(SkillName.PoderMagico, 200, 300);
            this.SetSkill(SkillName.Anatomia, 70.1, 100.0);
            this.SetSkill(SkillName.Briga, 70.1, 100.0);

            this.Fame = 4500;
            this.Karma = -4500;

            this.VirtualArmor = 100;
        }

        public StoneHarpy(Serial serial)
            : base(serial)
        {
        }

        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override int Feathers
        {
            get
            {
                return 50;
            }
        }
        public override bool CanFly
        {
            get
            {
                return true;
            }
        }
        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Average, 2);
            this.AddLoot(LootPack.Gems, 2);
        }

        public override int GetAttackSound()
        {
            return 916;
        }

        public override int GetAngerSound()
        {
            return 916;
        }

        public override int GetDeathSound()
        {
            return 917;
        }

        public override int GetHurtSound()
        {
            return 919;
        }

        public override int GetIdleSound()
        {
            return 918;
        }

        public override void OnBeforeDamage(Mobile from, ref int totalDamage, DamageType type)
        {
            if (from == null)
                return;

            if (!this.Alive)
                return;

            if (type != DamageType.Melee)
                return;

            bool bonus = false;
            base.OnBeforeDamage(from, ref totalDamage, type);
            var arma = from.FindItemOnLayer(Layer.OneHanded);
            if (arma != null && arma is BaseBashing)
            {
                totalDamage += 10;
                bonus = true;
            }

            arma = from.FindItemOnLayer(Layer.TwoHanded);
            if (arma != null && arma is BaseBashing)
            {
                bonus = true;
                totalDamage += 10;
            }

            if (!bonus)
            {
                totalDamage /= 10;
                if (!from.IsCooldown("dicabas2"))
                {
                    from.SetCooldown("dicabas2", TimeSpan.FromMinutes(10));
                    from.SendMessage("Seu ataque nao foi muito efetivo");

                }
            }
            else if (bonus)
            {
                if(Utility.RandomDouble() > 0.25)
                {
                    var ore = new IronOre();
                    ore.MoveToWorld(this.Location);
                }
                if(!from.IsCooldown("dicabas"))
                {
                    from.SetCooldown("dicabas", TimeSpan.FromMinutes(10));
                    from.SendMessage("Seu ataque foi muito efetivo contra " + this.Name);
                }    
            }
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
