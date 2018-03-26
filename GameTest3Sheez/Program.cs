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
            

            //Dispose check
            Console.WriteLine(myWarrior.currentHealth);

            Console.ReadKey();
            
        }
    }

    //----> TODO: Create hierarchy (folders).
    //----> TODO: Dictionary + IStorable interface for adding swords to an inventory.
    //----> TODO: Allow for charachter saving and loading using serialization to XML.
    //----> TODO: Implement IDisposable.
    //----> TODO: Set up database to store items (and connection).


    /// <summary>
    /// Below all Player(and inherited) classes.
    /// </summary>
    #region Abstract class: Player
    /// <summary>
    /// This class serves as the blueprint for every playable class.
    /// </summary>
    abstract class Player //: IDisposable <-- stuck on this
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
        abstract public void SetLevel(double _level);
        abstract public void SetHealth(double _health);
        abstract public void SetStrenght(int _strenght);
        abstract public void SetConstitution(int _constitution);
    }
    #endregion

    #region Class Warrior : Player
    /// <summary>
    /// Playable class Warrior that derrives from Player class.
    /// </summary>
    class Warrior : Player
    {
            //Private properties available to Warrior class only.
        private int baseStrenght = 10;
        private double baseHealth = 100;
        private int baseConstitution = 10;
        private double baseLevel = 1;

            //Default constructor
        public Warrior()
        {

        }
            //Constructor that takes a string used for creation of a new Warrior object and sets name and stats.
        public Warrior(string name)
        {
            SetName(name);
            SetLevel(baseLevel);
            SetHealth(baseHealth);
            currentHealth = maxHealth;
            SetStrenght(baseStrenght);
            SetConstitution(baseConstitution);
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
        public override void SetLevel(double _level)
        {
            
            level = _level;
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
    }
    #endregion

    /// <summary>
    /// Below all Item(and inherited) classes.
    /// </summary>

    #region Abstract class : Items

    public abstract class Items //Needs interfaces: IEquippable, IWeapon, IPartOfQuest, IDamagable
    {
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
        public int damage { get; private set; }
        public double durability { get; private set; }

    }
    #endregion

    #region Class Sword : Weapons
    public class Sword : Weapons , IStorable
    {
        public Dictionary<string, object> dictionairy = new Dictionary<string, object>();//Want this in the interface declaration

        public Sword()
        {
            
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
        public void Add2Dictionary(Sword _sword) // <<<==== working on this
        {
            dictionairy.Add("sword1", _sword);
        }
    }

    #endregion

    /// <summary>
    /// Below all interfaces
    /// </summary>

    #region Interface IStorable
    public interface IStorable
    {
        void Add2Dictionary(Sword _sword);
    }

    #endregion

    #region Interface IDamagable
    public interface IDamagable
    {
        void SellItem();
    }
    #endregion
}
