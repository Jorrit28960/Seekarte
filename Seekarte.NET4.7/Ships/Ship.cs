namespace Seekarte.NET4._7
{
    public class Ship
    {
        public virtual string ShipType { get; set; }
        public int HitPoints { get; set; }
        public string Name { get; set; }
        public int Troops { get; set; }
        //public virtual int MaxHitPoints { get; }
        public virtual int MaxHitPoints { get; set; }
        //public virtual int MaxTroops { get; }
        public virtual int MaxTroops { get; set; }
        public int Geschwader { get; set; }

        public Ship()
        {
            HitPoints = MaxHitPoints;
            Troops = 0;
            Name = ShipType;
            Geschwader = 0;
        }
    }
}
