using System;
using Server.Engines.Distillation;
using Server.Items;
using Server.Network;


namespace Server.Spells.Bardo
{
    public class SomDeFestaSpell : BardoSpell
    {
        private static readonly SpellInfo m_Info = new SpellInfo(
            "Som de Festa", "Hora da festa!",
            224,
            9011,
            Reagent.Garlic,
            Reagent.Ginseng,
            Reagent.MandrakeRoot);
        private static readonly FoodInfo[] m_Food = new FoodInfo[]
        {
            new FoodInfo(typeof(BottleOfAle), "uma garrafa de cerveja"),
            new FoodInfo(typeof(BottleOfMead), "uma garrafa de hidromel"),
            new FoodInfo(typeof(JugOfCider), "uma garrafa de cidra"),
            new FoodInfo(typeof(BottleOfLiquor), "uma garrafa de licor"),
            new FoodInfo(typeof(BottleOfWine), "uma garrafa de vinho"),
         
        };
        public SomDeFestaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override int EficienciaMagica(Mobile caster) { return 3; } //Servirá para calcular o modificador na eficiência das magias

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }
public override bool CheckCast()
        {
            // Check for a musical instrument in the player's backpack
            if (!CheckInstrument())
            {
                Caster.SendMessage("Você precisa ter um instrumento musical na sua mochila para canalizar essa magia.");
                return false;
            }


            return base.CheckCast();
        }


 private bool CheckInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) != null;
        }


        private BaseInstrument GetInstrument()
        {
            return Caster.Backpack.FindItemByType(typeof(BaseInstrument)) as BaseInstrument;
        }

        public override double RequiredSkill
        {
            get
            {
                return 10.0;
            }
        }
        public override void OnCast()
        {
            if (this.CheckSequence())
            {
                FoodInfo foodInfo = m_Food[Utility.Random(m_Food.Length)];
                Item food = foodInfo.Create();

                if (food != null)
                {
                    this.Caster.AddToBackpack(food);

                    // You magically create food in your backpack:
                    this.Caster.SendLocalizedMessage(1042695, true, " " + foodInfo.Name);

                    this.Caster.FixedParticles(0, 10, 5, 2003, EffectLayer.RightHand);
                    this.Caster.PlaySound(0x1E2);
                }
            }

            this.FinishSequence();
        }
    }

    public class FoodInfo
    {
        private Type m_Type;
        private string m_Name;
        public FoodInfo(Type type, string name)
        {
            this.m_Type = type;
            this.m_Name = name;
        }

        public Type Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }
        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }
        public Item Create()
        {
            Item item;

            try
            {
                item = (Item)Activator.CreateInstance(this.m_Type);
            }
            catch
            {
                item = null;
            }

            return item;
        }
    }
}
