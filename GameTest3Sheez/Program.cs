using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GameTest3Sheez
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("");

                //Character creation
            string x = "Woofles";
            Warrior myWarrior = new Warrior(x);

                //Value checking
            Console.WriteLine("Charachter name: " + myWarrior.playerName);
            string creation = string.Format("The level of " + myWarrior.playerName + " is " + myWarrior.level + " and has " + myWarrior.currentHealth + " health");
            string stats = string.Format(myWarrior.playerName + "'s Strenght is " + myWarrior.strenght + ", Constitition is " + myWarrior.constitution + " and " + myWarrior.intelligence + " intelligence.");
            Console.WriteLine(creation);
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Console.WriteLine(stats);
            Console.WriteLine("________________________________________________________________________________________________________________________");
            Sword sword = new Sword();
            Console.WriteLine(sword.itemName + " With the stats: " + sword.damage + " DMG, " + sword.durability + " Dura " + sword.goldValue + " Gold" + " Weighs " + sword.weight);
            object swordObject = sword as object;
            sword.AddToInventory(swordObject);

            Sword swordy = new Sword("woffleslayer");
            Console.WriteLine(swordy.itemName + " With the stats: " + swordy.damage + " DMG, " + swordy.durability + " Dura " + swordy.goldValue + " Gold" + " Weighs " + swordy.weight);
            object swordObjecty = sword as object;
            sword.AddToInventory(swordObjecty);

            //Combat test
            for (int i = 0; i<11;i++) // fights the instanced enemy 11 times.
            {
                Console.WriteLine("________________________________________________________________________________________________________________________");
                Enemy rat = new Enemy("rat");
                if(rat.ResolveCombat(myWarrior, sword))
                {
                    myWarrior.GetExperience(rat);
                    sword.DamageWeapon(rat.enemyDuraDamage);
                    Console.WriteLine("________________________________________________________________________________________________________________________");
                    Console.WriteLine(myWarrior.currentHealth + " Health left. " + sword.durability + " durability left on your " + sword.itemName + ". Current xp: " + myWarrior.level);
                    Console.WriteLine("________________________________________________________________________________________________________________________");
                }
            }
            Console.ReadKey();


        }
    }

    //----> TODO: Create enemy class.
    //----> TODO: Create hierarchy (folders).
    //----> TODO: Dictionary + IStorable interface for adding swords to an inventory.
    //----> TODO: Allow for charachter saving and loading using serialization to XML.
    //----> TODO: Implement IDisposable.
    //----> TODO: Set up database to store items (and connection).
    //----> TODO: Set up leveling method.


    /// <summary>
    /// Below all Player(and inherited) classes.
    /// </summary>
    #region Abstract class: Player
    /// <summary>
    /// This class serves as the blueprint for every playable class.
    /// </summary>
    public abstract class Player //: IDisposable <-- stuck on this
    {
            //Properties    
        public string playerName { get; set; }
        public double level { get; set; }
        public double maxHealth { get; set; }
        public double currentHealth { get; set; }
        public int strenght { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }

            //Abstract Methods
        abstract public void SetName(string _name);
        abstract public void GetExperience(Enemy _enemy);
        abstract public void SetHealth(double _health);
        abstract public void SetStrenght(int _strenght);
        abstract public void SetConstitution(int _constitution);
        abstract public void SetIntelligence(int _intelligence);
        abstract public bool SaveCharacter();

        //abstract public void LevelUp();
    }
    #endregion

    #region Class Warrior : Player
    /// <summary>
    /// Playable class Warrior that derrives from Player class.
    /// </summary>
    public class Warrior : Player
    {
            //Private properties available to Warrior class only.
        private int baseStrenght = 10;
        private double baseHealth = 100;
        private int baseConstitution = 10;
        private double baseLevel = 1;
        private int baseIntelligence = 2;

            //Default constructor
        public Warrior()
        {

        }
            //Constructor that takes a string used for creation of a new Warrior object and sets name and stats.
        public Warrior(string name)
        {
            SetName(name);
            level = baseLevel;
            SetHealth(baseHealth);
            currentHealth = maxHealth;
            SetStrenght(baseStrenght);
            SetConstitution(baseConstitution);
            SetIntelligence(baseIntelligence);
        }


        //Mandatory overrides declared in baseclass.
        public override void SetName(string _name)
        {
            if (playerName == null)
            {
                playerName = _name;
            }
            else
            {
                throw new ArgumentException("Playername was already set once.");
            }
        }
        public override void GetExperience(Enemy _enemy)
        {
            level += _enemy.experience;
        }
        public override void SetHealth(double _health)
        {
            maxHealth = _health;
        }
        public override void SetStrenght(int _strenght)
        {
            strenght = _strenght;
        }
        public override void SetConstitution(int _constitution)
        {
            constitution = _constitution;
        }
        public override void SetIntelligence(int _intelligence)
        {
            intelligence = _intelligence;
        }

        public override bool SaveCharacter()
        {
            return true;
        }
    }
    #endregion

    /// <summary>
    /// Below all Item(and inherited) classes.
    /// </summary>

    #region Abstract class : Items

    public abstract class Items //Needs interfaces: IEquippable, IWeapon, IPartOfQuest, IDamagable, IExclusiveToClass
    {
        public string itemName { get; set; }
        public double goldValue { get; set; }
        public double weight { get; set; }

        public abstract void SellItem();
        public abstract void BuyItem();
        public abstract void BuffItem();
        
    }
    #endregion

    #region Abstract class Weapons : Items
    public abstract class Weapons : Items
    {
        public int damage { get; internal set; }
        public double durability { get; internal set; }

    }
    #endregion

    #region Class Sword : Weapons
    public class Sword : Weapons , IStorable, IDamagable
    {
        public Dictionary<string, object> dictionairy = new Dictionary<string, object>();//Want this in the interface declaration

        public Sword()
        {
            itemName = "Sword";
            damage = 10;
            durability = 20;
            goldValue = 100;
            weight = 2;
        }

        public Sword(string _name)
        {
            itemName = _name;
            damage = 10;
            durability = 20;
            goldValue = 100;
            weight = 2;
        }

        public override void BuyItem()
        {
            throw new NotImplementedException();
        }

        public override void SellItem()
        {
            throw new NotImplementedException();
        }
        public override void BuffItem()
        {
            throw new NotImplementedException();
        }
        public void AddToInventory(object itemToAdd) //Do this with event handler??
        {
            Sword addThis = itemToAdd as Sword;
            for (int i = 0; i < dictionairy.Count; i++)
            {
                try
                {
                    dictionairy.Add("1", addThis);
                }
                catch
                {
                    return;
                }
            }
            Console.WriteLine(dictionairy.Count);
        }

        public void DamageWeapon(double _damage)
        {
            this.durability -= _damage;
        }
    }

    public class Inventory
    {
        public int inventorySize;
        
    }

    #endregion

    /// <summary>
    /// Below Enemy class.
    /// </summary>

    #region class Enemy
    public class Enemy
    {
        public string enemyName;
        public int enemyDamage;
        public double enemyDuraDamage;
        public int experience;
        public double enemyHealth;
        public string enemyDropTable;

        public Enemy(string _rat)
        {
            enemyName = "Rat";
            enemyDamage = 5;
            enemyDuraDamage = 1;
            enemyHealth = 20;
            experience = 10;
        }
        public bool ResolveCombat(Warrior _player,Sword  _weapon)
        {
            if (_player.currentHealth > 0)
            {
                do
                {
                    _player.currentHealth -= enemyDamage;
                    enemyHealth -= _weapon.damage;
                }
                while (_player.currentHealth > 0 && enemyHealth > 0);
                if (_player.currentHealth > 0 && enemyHealth <= 0)
                {

                    Console.WriteLine("You won the fight against a " + enemyName);
                    return true;
                }
                else
                {
                    Console.WriteLine("You lost the fight against a " + enemyName);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You are dead!");
                return false;
            }
        
        }
    }
    #endregion

    /// <summary>
    /// Below all interfaces want IEnemy?
    /// </summary>

    #region Interface IStorable
    public interface IStorable
    {
        void AddToInventory(object itemToAdd);
    }

    #endregion

    #region Interface IDamagable
    public interface IDamagable
    {
        void DamageWeapon(double _damage);
    }
    #endregion
}
