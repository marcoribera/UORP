#region References
using System.Collections.Generic;

using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
#endregion

namespace Server.Targets
{
	public class AIControlMobileTarget : Target
	{
		private readonly List<BaseAI> m_List;
		private readonly OrderType m_Order;
		private readonly BaseCreature m_Mobile;

		public AIControlMobileTarget(BaseAI ai, OrderType order)
			: base(-1, false, (order == OrderType.Attack ? TargetFlags.Harmful : TargetFlags.None))
		{
			m_List = new List<BaseAI>();
			m_Order = order;

			AddAI(ai);
			m_Mobile = ai.m_Mobile;
		}

		public OrderType Order { get { return m_Order; } }

		public void AddAI(BaseAI ai)
		{
			if (!m_List.Contains(ai))
				m_List.Add(ai);
		}

		protected override void OnTarget(Mobile from, object o)
		{
			if (o is IDamageable)
			{
				var dam = o as IDamageable;

				for (var i = 0; i < m_List.Count; ++i)
					m_List[i].EndPickTarget(from, dam, m_Order);
			}
			else if (o is MoonglowDonationBox && m_Order == OrderType.Transfer && from is PlayerMobile)
			{
				var pm = (PlayerMobile)from;
				var box = (MoonglowDonationBox)o;

				pm.SendGump(new ConfirmTransferPetGump(box, from.Location, m_Mobile));
			}
		}
	}

    public class AILocationTarget : Target
    {
        private readonly List<BaseAI> m_List;
        private readonly OrderType m_Order;
        public Point3D p_local;

        public AILocationTarget(BaseAI ai, OrderType order)
            : base(-1, true, (order == OrderType.Attack ? TargetFlags.Harmful : TargetFlags.None))
        {
            m_List = new List<BaseAI>();
            m_Order = order;
            p_local = Point3D.Zero;

            AddAI(ai);
        }

        public OrderType Order { get { return m_Order; } }

        public void AddAI(BaseAI ai)
        {
            if (!m_List.Contains(ai))
                m_List.Add(ai);
        }

        protected override void OnTarget(Mobile from, object o)
        {
            if (o is Mobile)
            {
                p_local = ((Mobile)o).Location;
            }
            else if (o is Item)
            {
                Item alvo = (Item)o;
                p_local = ((Item)o).Location;
            }
            else if (o is StaticTarget)
            {
                StaticTarget alvo = (StaticTarget)o;
                p_local = ((StaticTarget)o).Location;
            }
            else if (o is LandTarget)
            {
                LandTarget alvo = (LandTarget)o;
                p_local = ((LandTarget)o).Location;
            }
            for (var i = 0; i < m_List.Count; ++i)
                m_List[i].EndPickTarget(from, this, m_Order);
        }
    }
}
