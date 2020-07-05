namespace WindowsFormsApp1.com
{
    public class Car
    {
        public Damage damageType { get; set; }

        public Car(Damage damageType)
        {
            this.damageType = damageType;
        }
    }

    public enum Damage
    {
        LAKIER, MECHANICZNE, DIAGNOSTYKA
    }

}
