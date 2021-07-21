using Server;
using Server.Items;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
    public class DoorTrapInstaller : Item
    {
        
        private Mobile _owner;
        private DoorTrapType _trapType;
        private int _initialUses;

        public Mobile Owner { get { return _owner; } }
        public DoorTrapType TrapType { get { return _trapType; } }
        public int InitialUses { get { return _initialUses; } }


        [Constructable]
        public DoorTrapInstaller( Mobile owner, DoorTrapType type, int initialUses )
            : base(0x1EBA)
        {
            
            _owner = owner;
            _trapType = type;
            _initialUses = initialUses;

            Name = "Ferramenta de Instalação de Armadilhas";
        }

        [Constructable]
        public DoorTrapInstaller( Serial serial ) : base(serial) { }

        public virtual bool Install( Mobile m, BaseDoor door, out string message )
        {
            if( door.CanInstallTrap(m) )
            {
                if( door.HasTrap() && this.TrapType == door.TrapType && door.DoorTrap.Refillable )
                {
                    door.DoorTrap.Recharge(this.InitialUses);
                    message = "Uma armadilha do mesmo tipo já foi instalada nesta porta, então você simplesmente reabastece sua munição.";
                    return true;
                }

                if (door.AttachTrap(BaseDoorTrap.CreateTrapByType(_trapType, _owner, _initialUses)))
                {
                    message = "Você instalou a armadilha com sucesso.";
                    return true;
                }

                if (door.HasTrap() && this.TrapType != door.TrapType)
                {
                    message = "Esta porta já parece já ter uma armadilha.";
                    return false;
                }
            }

            message = "Você não consegue instalar a armadilha.";
            return false;
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties(list);

            list.Add(1049644, _trapType.ToString()); //[~stuff~]
        }

        public override void OnDoubleClick( Mobile from )
        {
            if( _owner == null )
                _owner = from;

            from.BeginTarget(1, false, TargetFlags.None, new TargetStateCallback(this_doorSelected), this);
            from.SendMessage("Selecione a porta para instalar esta armadilha.");
        }

        private void this_doorSelected( Mobile from, object target, object state )
        {
            BaseDoor door = target as BaseDoor;
            DoorTrapInstaller inst = state as DoorTrapInstaller;
            string message = "";

            if( door == null )
            {
                from.BeginTarget(1, false, TargetFlags.None, new TargetStateCallback(this_doorSelected), inst);
                message = "Isso não é uma porta. Tente novamente.";
            }
            else if( inst != null && inst.Install(from, door, out message) )
            {
                inst.Delete();
            }

            from.SendMessage(message);
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(_owner);
            writer.Write((int)_trapType);
            writer.Write(_initialUses);
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            _owner = reader.ReadMobile();
            _trapType = (DoorTrapType)reader.ReadInt();
            _initialUses = reader.ReadInt();
        }
    }
}
