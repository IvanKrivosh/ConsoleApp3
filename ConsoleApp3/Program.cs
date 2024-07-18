using System;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Weapon weapon = new Weapon(1, 2);
            Bot bot = new Bot(weapon);
            Player player = new Player(-50);

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
            Utils.CheckIntRangeVariable(damage, nameof(damage));
            Utils.CheckIntRangeVariable(bullets, nameof(bullets));
            _damage = damage;
            _bullets = bullets;
        }

        public void Fire(ITarget target)
        {            
            target.SetDamage(_damage);
            _bullets -= 1;
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
            Utils.CheckIntRangeVariable(health, nameof(health));

            Health = health;
        }

        public int Health { get; private set; }

        public void SetDamage(int damage)
        {
            Utils.CheckIntRangeVariable(damage, nameof(damage));

            if (Health > 0)
                Health -= damage;
        }
    }    

    class Bot
    {
        private Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            _weapon.Fire(player);
        }
    }

    static class Utils
    {
        static public void CheckIntRangeVariable(int value, string name)
        {
            if (value <= 0) 
                throw new ArgumentOutOfRangeException(name);
        }
    }
}
