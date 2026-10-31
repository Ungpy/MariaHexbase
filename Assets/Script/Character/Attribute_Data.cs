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

public enum AttributeType { HP, MDamage, PDamage, Speed, Armour }