using UnityEngine;

public enum ClassType { attack, defense, support, magic }

[System.Serializable]
public class CharacterData
{
    //[SerializeField] TMP_Text HPtext;
    [SerializeField] private int id = -1;
    [SerializeField] private string name = "Default name";
    [SerializeField] private ClassType classType = ClassType.attack;
    [SerializeField] private Attribute attr = new Attribute(10f, 10f, 10f, 10f, 10f);

    [SerializeField] public float currentHealth = 20f;
    [SerializeField] private float currentMana;
    [SerializeField] private float currentStamina;
    [SerializeField] private float currentMood;
    [SerializeField] private int currentLevel;
    [SerializeField] private float currentExp;
    [SerializeField] private float maxHealth = 40f;
    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;
    [SerializeField] private float CapExp;


    public CharacterData(int id, string name, ClassType classType, Attribute attr)
    {
        this.id = id;
        this.name = name;
        this.classType = classType;
        this.attr = attr;
    }

    public Attribute GetAttribute()
    {
        return attr;
    }

    public string Getname()
    {
        return name;
    }
    //    HPtext.text = "HP: " + attr.Get(AttributeType.HP).ToString();
    //    HPtext.text = "Physic Damage: " + attr.Get(AttributeType.PDamage).ToString();
    //    HPtext.text = "Magic Damage: " + attr.Get(AttributeType.MDamage).ToString();
    //    HPtext.text = "Speed: " + attr.Get(AttributeType.Speed).ToString();
    //    HPtext.text = "Armour: " + attr.Get(AttributeType.Armour).ToString();

}

public enum AttributeType { HP, MDamage, PDamage, Speed, Armour }

[System.Serializable]
public class Attribute
{
    public float health; 
    public float pDamage;
    public float mDamage;
    public float speed;
    public float armour;

        //ตรงนี้คือ constructor มันจะไป assign ค่าให้ struct อัตโนมันตอนเราสร้าง Stat(1,2,1,2,1) งี้
    public Attribute(float health, float PDamage, float MDamage, float Speed, float Armour)
    {
        this.health = health;
        this.pDamage = PDamage;
        this.mDamage = MDamage;
        this.speed = Speed;
        this.armour = Armour;
    }
    public Attribute(Attribute clone)
    {
        this.health = clone.health;
        this.pDamage = clone.pDamage;
        this.mDamage = clone.mDamage;
        this.speed = clone.speed;
        this.armour = clone.armour;
    }

    public float Get(AttributeType type)
    {
        return type switch
        {
            AttributeType.HP => health,
            AttributeType.PDamage => pDamage,
            AttributeType.MDamage => mDamage,
            AttributeType.Speed => speed,
            AttributeType.Armour => armour,
            _ => 0
        };
    }
    public void Set(AttributeType type, float value)
    {
        switch (type)
        {
            case AttributeType.HP: health = value; break;
            case AttributeType.PDamage: pDamage = value; break;
            case AttributeType.MDamage: mDamage = value; break;
            case AttributeType.Speed: speed = value; break;
            case AttributeType.Armour: armour = value; break;
        }
    }
}

