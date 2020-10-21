namespace Seekarte.NET4._7
{
    public class Ship
    {
        public virtual string ShipType { get; }
        public int HitPoints { get; set; }
        public string Name { get; set; }
        public int Troops { get; set; }
        public virtual int MaxHitPoints { get; }
        public virtual int MaxTroops { get; }
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
