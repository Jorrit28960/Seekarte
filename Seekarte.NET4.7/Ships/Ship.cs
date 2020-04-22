namespace Seekarte.NET4._7
{
    public abstract class Ship
    {
        public abstract string ShipType { get; }
        public int HitPoints { get; set; }
        public string Name { get; set; }
        public int Troops { get; set; }
        public abstract int MaxHitPoints { get; }
        public abstract int MaxTroops { get; }
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
