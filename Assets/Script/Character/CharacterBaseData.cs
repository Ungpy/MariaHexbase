using Unity.VisualScripting;
using UnityEngine;

public enum ClassType { attack, defense, support, magic }

[System.Serializable]
public class CharacterData
{
    [SerializeField] private int id = -1;
    [SerializeField] private string name = "Default name";
    [SerializeField] private ClassType classType = ClassType.attack;
    [SerializeField] private AttributeData attr = new AttributeData(10f, 10f, 10f, 10f, 10f);

    [SerializeField] private StatData health = new StatData(20f,40f);
    [SerializeField] private StatData mana = new StatData(30f,120f);
    [SerializeField] private StatData stamina = new StatData(10f,20f);
    [SerializeField] private StatData mood = new StatData(50f,60f);

    public StatData Health => health;
    public StatData Mana => mana;
    public StatData Stamina => stamina;
    public StatData Mood => mood;

    //------------------------------------------
    [SerializeField] private float currentExp;
    [SerializeField] private float capExp;
    [SerializeField] private int currentLevel;


    public CharacterData(int id, string name, ClassType classType, AttributeData attr)
    {
        this.id = id;
        this.name = name;
        this.classType = classType;
        this.attr = attr;
    }

    public AttributeData GetAttribute()
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
public class AttributeData
{
    public float health = 0f; 
    public float pDamage = 0f;
    public float mDamage = 0f;
    public float speed = 0f;
    public float armour = 0f;

        //ตรงนี้คือ constructor มันจะไป assign ค่าให้ struct อัตโนมันตอนเราสร้าง Stat(1,2,1,2,1) งี้
    public AttributeData(float health, float PDamage, float MDamage, float Speed, float Armour)
    {
        this.health = health;
        this.pDamage = PDamage;
        this.mDamage = MDamage;
        this.speed = Speed;
        this.armour = Armour;
    }
    public AttributeData(AttributeData clone)
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

[System.Serializable]
public class StatData
{
    [SerializeField] private float current;
    [SerializeField] private float max;

    public float Current => current;
    public float Max => max;

    public StatData(float current, float max)
    {
        this.current = current;
        this.max = max;
    }

    public void ModifyValue(float amount)
    {
        max += Mathf.Clamp(current + amount, 0f, max);
    }

    public void SetMaxValue(float amount)
    {
        max = Mathf.Max(0f, amount);
        if(current > max)
        {
            current = max;
        }
    }
}

