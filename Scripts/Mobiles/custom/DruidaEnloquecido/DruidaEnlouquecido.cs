//made by cadenuo for the mystic circle shard not to be reproduced


using System;
using Server; 
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Fifth;
using Server.Spells.Seventh;

namespace Server.Mobiles
{
	public class DruidaEnlouquecido : BaseCreature
	{

        [Constructable]
        public DruidaEnlouquecido()
            
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.175, 0.350)
        {
            SpeechHue = Utility.RandomDyedHue();
            Title = "Druida Enlouquecido";
            Hue = Utility.RandomSkinHue();

            if (this.Female = Utility.RandomBool())
            {
                Body = 0x191;
                Name = NameList.RandomName("female");
                AddItem(new Skirt());

            }
            else
            {
                Body = 0x190;
                Name = NameList.RandomName("male");
                AddItem(new ShortPants());



            }


            SetStr(180, 200);
            SetDex(70, 80);
            SetInt(120, 160);

            SetHits(450, 600);
            SetMana(150);

            SetDamage(25, 35);

            SetSkill(SkillName.Briga, 75.0, 90.5);
            SetSkill(SkillName.Anatomia, 70.0, 90.5);

            SetSkill(SkillName.PoderMagico, 80.0, 90.5);
            SetSkill(SkillName.ResistenciaMagica, 60.0, 70.0);
            SetSkill(SkillName.Arcanismo, 80.0, 90.0);

            SetResistance(ResistanceType.Physical, 30);
            SetResistance(ResistanceType.Fire, 30);
            SetResistance(ResistanceType.Cold, 30);
            SetResistance(ResistanceType.Poison, 30);
            SetResistance(ResistanceType.Energy, 30);

            Fame = 1000;
            Karma = -1000;

            AddItem(new Boots());
            RobeOfTheDruid robe = new RobeOfTheDruid();
            robe.Movable = false;
            AddItem(robe);

            AddItem(new LeatherChest());
            AddItem(new LeatherGloves());
            AddItem(new LeatherGorget());
            AddItem(new LeatherArms());
            AddItem(new LeatherLegs());

            PackItem(new DryIce(Utility.RandomMinMax(0, 1)));
            switch (Utility.Random(3))
            {
                case 0: AddItem(new QuarterStaff()); break;
                case 1: AddItem(new BlackStaff()); break;
                case 2: AddItem(new GnarledStaff()); break;
            }

            Item hair = new Item(Utility.RandomList(0x203B, 0x2049, 0x2048, 0x204A));
            hair.Hue = Utility.RandomNondyedHue();
            hair.Layer = Layer.Hair;
            hair.Movable = false;
            AddItem(hair);
            PackItem(new BottledLightning(Utility.RandomMinMax(0, 1)));
        }
		

		public override void GenerateLoot()
		{
			
            AddLoot( LootPack.UltraRich,4 );
      
        }

        public override bool OnBeforeDeath()
        {
            this.Say("Não.......Sem meus aliados da natureza, eu não quero viver!");
            this.AddItem(new Gold(1000));
            this.AddItem(new DruidCloak());

                switch (Utility.Random(10))
                {
                    case 0: PackItem(new RingOfTheDruid()); break;
                    case 1: PackItem(new RobeOfTheDruid()); break;
                    case 2: PackItem(new HealingStoneScroll()); break;
                case 3: PackItem(new NetherBoltScroll()); break;
                case 4: PackItem(new PurgeMagicScroll()); break;
                case 5: PackItem(new EnchantScroll()); break;
                case 6: PackItem(new SleepScroll()); break;
                case 7: PackItem(new EagleStrikeScroll()); break;
                case 8: PackItem(new AnimatedWeaponScroll()); break;
                case 9: PackItem(new StoneFormScroll()); break;
                case 10: PackItem(new SpellTriggerScroll()); break;
             
            }

            return base.OnBeforeDeath();
        }

        public override bool Unprovokable { get { return true; } }
        public override bool Uncalmable { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override bool AlwaysMurderer { get { return true; } }

        public void Polymorph(Mobile m)
        {
            if (!m.CanBeginAction(typeof(PolymorphSpell)) || !m.CanBeginAction(typeof(IncognitoSpell)) || m.IsBodyMod)
                return;

            IMount mount = m.Mount;

            if (mount != null)
                mount.Rider = null;

            if (m.Mounted)
                return;

            if (m.BeginAction(typeof(PolymorphSpell)))
            {
                Item disarm = m.FindItemOnLayer(Layer.OneHanded);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);

                disarm = m.FindItemOnLayer(Layer.TwoHanded);

                if (disarm != null && disarm.Movable)
                    m.AddToBackpack(disarm);

                m.BodyMod = 205;
                m.HueMod = 0;
                m.SendMessage("Você se sente fofinho e quer ser abraçado");
                new ExpirePolymorphTimer(m).Start();
            }
        }

        private class ExpirePolymorphTimer : Timer
        {
            private Mobile m_Owner;

            public ExpirePolymorphTimer(Mobile owner)
                : base(TimeSpan.FromMinutes(2.0))
            {
                m_Owner = owner;

                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                if (!m_Owner.CanBeginAction(typeof(PolymorphSpell)))
                {
                    m_Owner.SendMessage("Você volta ao normal");
                    m_Owner.BodyMod = 0;
                    m_Owner.HueMod = -1;
                    m_Owner.EndAction(typeof(PolymorphSpell));
                }
            }
        }



        public void SpawnDragons(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newDragons = Utility.RandomMinMax(1, 2);

            for (int i = 0; i < newDragons; ++i)
            {
                Drake Drake = new Drake();

                Drake.Team = this.Team;
                Drake.FightMode = FightMode.Closest;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                Drake.MoveToWorld(loc, map);
                Drake.Combatant = target;
            }
        }

        public void DoSpecialAbility(Mobile target)
        {
            if (Utility.RandomDouble() <= 0.06)
            {
                this.Say("Olha que fofo... um coelhinho!");
                Polymorph(target);
            }
            else if (Utility.RandomDouble() <= 0.03)
            {
                this.Say("Eu cansei dos de brincar com você, Eu Invoco os Poderes da Natureza para me Proteger!");
                SpawnDragons(target);
            }
        }
        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            DoSpecialAbility(defender);

            defender.Damage(Utility.Random(2, 1), this);
            defender.Stam -= Utility.Random(2, 1);
            defender.Mana -= Utility.Random(2, 1);
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            DoSpecialAbility(attacker);

        }

        public override void OnDamagedBySpell(Mobile caster)
        {
            base.OnDamagedBySpell(caster);

            DoSpecialAbility(caster);

        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);

            // eats pet or summons
            if (from is BaseCreature && Utility.RandomDouble() < 1.0)
            {
                BaseCreature creature = (BaseCreature)from;

                if (creature.Controlled)
                {
                    AOS.Damage(creature, this, Utility.RandomMinMax(30, 40), 100, 0, 0, 0, 0);
                    this.Say("Você agora irá morrer pela sua desobediência!");
                    this.Hits += Utility.RandomMinMax(100, 150);
                    Effects.PlaySound(Location, Map, 0x574);
                }
            }

        }


        public DruidaEnlouquecido(Serial serial)
            : base(serial)
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
