namespace Assets.Scripts
{
    public interface IHitable
    {
        public int HitPoints { get; }
        public void Hit(int damage);
    }
}