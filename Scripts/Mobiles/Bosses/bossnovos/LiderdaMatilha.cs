using Server.Items;
using System;

namespace Server.Mobiles
{
    [CorpseName("a dragon wolf corpse")]
    public class LiderdaMatilha : BaseCreature
    {
        public override bool ReduceSpeedWithDamage { get { return false; } }

        public LiderdaMatilha()
            : base(AIType.AI_Ninja, FightMode.Closest, 10, 1, 0.05, 0.2)
        {
            Name = "Líder da Matilha";
            Body = 719;
            BaseSoundID = 0x5ED;

            SetStr(200, 200);
            SetDex(400, 400);
            SetStam(400, 400);

            SetInt(50, 55);

            SetHits(5000, 5000);

            SetDamage(3, 35);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 45, 55);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 30, 40);
            SetResistance(ResistanceType.Poison, 40, 50);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.Anatomia, 60.0, 70.0);
            SetSkill(SkillName.ResistenciaMagica, 0, 0);
            SetSkill(SkillName.Briga, 90.0, 105.0);
            SetSkill(SkillName.Percepcao, 60.0);
            //SetSkill()
            SetSkill(SkillName.Arcanismo, 100, 100);
            SetSkill(SkillName.PoderMagico, 100, 100);

            Fame = 8500;
            Karma = -8500;

            VirtualArmor = 60;

            Tamable = false;
            ControlSlots = 4;
            MinTameSkill = 102.0;

          //  PackItem(new SkillBook());
            //PackItem(new LivroAntigo());
            PackItem(Decos.RandomDeco());
            SetWeaponAbility(Habilidade.BleedAttack);
            SetWeaponAbility(Habilidade.WhirlwindAttack);

            last = DateTime.UtcNow;
        }

        public override void OnDamagedBySpell(Mobile from)
        {
            base.OnDamagedBySpell(from);
            if (from != this)
            {
                var rnd = Utility.RandomDouble();
                if (from != this && rnd < 0.8)
                {
                    this.Combatant = from;
                    this.OverheadMessage("* awwrrrr *");
                    this.PlaySound(this.GetAngerSound());
                }
            }
        }

        DateTime last;

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            last = DateTime.UtcNow;
            base.OnGaveMeleeAttack(defender);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            var diff = DateTime.UtcNow - last;
            if(diff.TotalSeconds > 20)
            {
                this.MoveToWorld(from.Location, from.Map);
                this.OverheadMessage("* pulou *");
            }

            this.SetStam(400);
            base.OnDamage(amount, from, willKill);

            var rnd = Utility.RandomDouble();

            if (from != this && rnd < 0.8)
            {
                this.Combatant = from;
                this.OverheadMessage("* awwrrrr *");
                this.PlaySound(this.GetAngerSound());
            }

            if (rnd < 0.06)
            {
                var lobim = new SavagePackWolfy();
                lobim.MoveToWorld(from.Location, from.Map);
                if (from != this)
                    lobim.Combatant = from;
                lobim.OverheadMessage("* grrr *");
                from.PlaySound(lobim.GetAngerSound());
                from.SendMessage("Um lobo sai da tocaia lhe atacando");
            }
        }

        //public static Item GetRandomPS(int skill)
        //{
        //  switch (Utility.Random(17))
        // {
        //   case 0: return new PowerScroll(SkillName.Mining, skill);
        // case 1: return new PowerScroll(SkillName.Blacksmith, skill);
        // case 2: return new PowerScroll(SkillName.Tinkering, skill);
        // case 3: return new PowerScroll(SkillName.Cooking, skill);
        // case 4: return new PowerScroll(SkillName.Carpentry, skill);
        //case 5: return new PowerScroll(SkillName.Fletching, skill);
        //case 6: return new PowerScroll(SkillName.Tailoring, skill);
        //case 7: return new PowerScroll(SkillName.Jewelcrafting, skill);
        //case 8: return new PowerScroll(SkillName.Peacemaking, skill);
        //case 9: return new PowerScroll(SkillName.Provocation, skill);
        //case 10: return new PowerScroll(SkillName.Discordance, skill);
        //case 11: return new PowerScroll(SkillName.AnimalTaming, skill);
        //case 12: return new PowerScroll(SkillName.Lumberjacking, skill);
        //case 13: return new PowerScroll(SkillName.Alchemy, skill);
        //case 14: return new PowerScroll(SkillName.Fishing, skill);
        //case 15: return new PowerScroll(SkillName.Imbuing, skill);
        //  case 16: return new PowerScroll(SkillName.Cartography, skill);
        //  }
        //return null;
        //}

        // public override bool OnBeforeDeath()
        //{
        //  if (NoKillAwards)
        //    return base.OnBeforeDeath();

        //            foreach (var e in GetLootingRights())
        //          {
        //            if (e.m_HasRight && e.m_Mobile != null)
        //          {
        //            var ps = GetRandomPS(105);
        //          if (ps != null)
        //        {
        //          e.m_Mobile.AddToBackpack(Decos.RandomDeco());
        //        e.m_Mobile.AddToBackpack(ps);
        //      e.m_Mobile.SendMessage(78, "Voce ganhou recompensas por ajudar a matar Lider da Matilha");
        //    e.m_Mobile.SendMessage(78, "As recompensas foram colocadas em sua mochila");
        // }
        // }
        // }
        // GolemMecanico.JorraOuro(this.Location, this.Map, 150);
        //return base.OnBeforeDeath();
        //}

        public LiderdaMatilha(Serial serial)
            : base(serial)
        {
        }

        public override bool DrainsLife { get { return true; } }
        public override double DrainsLifeChance { get { return 0.25; } }
        public override int DrainAmount { get { return Utility.RandomMinMax(10, 30); } }

        public override bool AlwaysMurderer { get { return true; } }
        public override int Meat { get { return 80; } }
        public override int Hides { get { return 80; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Canine; } }
        public override HideType HideType { get { return HideType.Horned; } }

        public override void GenerateLoot()
        {
            ;
            AddLoot(LootPack.FilthyRich, 3);
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
