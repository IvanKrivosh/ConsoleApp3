using System;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Weapon weapon = new Weapon(1, 2);
            Bot bot = new Bot(weapon);
            Player player = new Player(50);

            bot.OnSeePlayer(player);

            Console.ReadKey();
        }
    }

    class Weapon
    {
        private int _damage;
        private int _bullets;

        public Weapon(int damage, int bullets)
        {            
            Utils.CompareIntGreaterZero(damage, nameof(damage));
            Utils.CompareIntGreaterZero(bullets, nameof(bullets));
            _damage = damage;
            _bullets = bullets;
        }

        public void Fire(ITarget target)
        {
            Utils.CompareObjectIsNotNull(target, nameof(target));

            if (_bullets == 0)
              throw new InvalidOperationException("There are not enough bullets");

            target.SetDamage(_damage);
            _bullets--;
        }
    }

    interface ITarget
    {
        int Health { get; }

        void SetDamage(int damage);
    }

    class Player: ITarget
    {
        public Player(int health)
        {
            Utils.CompareIntGreaterZero(health, nameof(health));
            Health = health;
        }

        public int Health { get; private set; }

        public void SetDamage(int damage)
        {
            Utils.CompareIntGreaterZero(damage, nameof(damage));

            if (Health > 0)
                Health -= damage;
        }
    }    

    class Bot
    {
        private readonly Weapon _weapon;

        public Bot(Weapon weapon)
        {            
            _weapon = weapon ?? throw new ArgumentNullException();
        }

        public void OnSeePlayer(Player player)
        {
            Utils.CompareObjectIsNotNull(player, nameof(player));
            _weapon.Fire(player);
        }
    }

    static class Utils
    {
        public static void CompareIntGreaterValue(int value, string name, int target)
        {
            if (value <= target) 
                throw new ArgumentOutOfRangeException(name);
        }

        public static void CompareIntGreaterZero(int value, string name)
        {
            CompareIntGreaterValue(value, name, 0);
        }

        public static void CompareObjectIsNotNull(object instance, string name)
        {
            ArgumentNullException.ThrowIfNull(instance);
        }
    }
}
