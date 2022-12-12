using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Spells.Cosmos
{
    public class MaoCosmicaSpell : CosmosSpell
	{
       private static readonly SpellInfo m_Info = new SpellInfo(
           "Mão Cósmica", "Manus Logos",
           203,
           0,
           Reagent.Bloodmoss,
           Reagent.Garlic);

        public override int EficienciaMagica(Mobile caster) { return 1; } //Servirá para calcular o modificador na eficiência das magias




        public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(0.5); } }

        public override SpellCircle Circle
        {
            get
            {
                return SpellCircle.First;
            }
        }

        public MaoCosmicaSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            this.Caster.Target = new InternalTarget(this);
        }

        public void Target(ITelekinesisable obj)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, obj);

                obj.OnTelekinesis(this.Caster);
            }

            this.FinishSequence();
        }

        public void Target(Container item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);

                object root = item.RootParent;

                if (!item.IsAccessibleTo(this.Caster))
                {
                    item.OnDoubleClickNotAccessible(this.Caster);
                }
                else if (!item.CheckItemUse(this.Caster, item))
                {
                }
                else if (root != null && root is Mobile && root != this.Caster)
                {
                    item.OnSnoop(this.Caster);
                }
                else if (item is Corpse && !((Corpse)item).CheckLoot(this.Caster, null))
                {
                }
                else if ( this.Caster.Region.OnDoubleClick(this.Caster, item) && CheckFizzle() )
                {
                    Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0xB41, 0, 5022, 0);
                    Effects.PlaySound(item.Location, item.Map, 0x1F5);

                    item.OnItemUsed(this.Caster, item);
                }
            }

            this.FinishSequence();
        }

#region Grab
        public void Target(Item item)
        {
            if (this.CheckSequence())
            {
                SpellHelper.Turn(this.Caster, item);
                object root = item.RootParent;

				if (item.Movable == false){ Caster.SendMessage( "Esse item parece não se mover." ); }
				else if (item.Amount > 1){ Caster.SendMessage("Há muitos itens empilhados aqui para mover."); }
				else if (item.Weight > (Caster.Int / 20)){ Caster.SendMessage( "Esse item é pesado demais." ); }
				else if (item.RootParentEntity != null){ Caster.SendMessage("Você não pode mover objetos que estão dentro de outros objetos ou sendo usados."); }
				else
				{
					Effects.SendLocationParticles(EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 0xB41, 0, 5022, 0);
					Effects.PlaySound(item.Location, item.Map, 0x1F5);
					Caster.AddToBackpack( item );
					Caster.SendMessage("Você move o objeto ao seu alcance e o coloca em sua mochila."); 
				}
			}
            this.FinishSequence();
        }
#endregion

        public class InternalTarget : Target
        {
            private readonly MaoCosmicaSpell m_Owner;
            public InternalTarget(MaoCosmicaSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.None)
            {
                this.m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is ITelekinesisable)
                    this.m_Owner.Target((ITelekinesisable)o);
                else if (o is Container)
                    this.m_Owner.Target((Container)o);
                    else if (o is Item)
                    this.m_Owner.Target((Item)o);
                else
					from.SendMessage("Este poder não funcionará nisso!"); 
            }

            protected override void OnTargetFinish(Mobile from)
            {
                this.m_Owner.FinishSequence();
            }
        }
    }
}
