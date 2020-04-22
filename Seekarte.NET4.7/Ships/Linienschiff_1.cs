namespace Seekarte.NET4._7
{
    //public class Fleet
    //{
    //    public List<Ship> Ships { get; set; } = new List<Ship>();
    //}

    public class Linienschiff_1 : Ship
    {
        public override string ShipType { get { return "Linienschiff 1. Klasse"; } }
        public override int MaxHitPoints { get { return 40; } }
        public override int MaxTroops { get { return 4; } }
    }
}
