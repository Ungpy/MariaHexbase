using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] TMP_Text HPtext;
    [SerializeField] TMP_Text PDMGtext;
    [SerializeField] TMP_Text MDMGtext;
    [SerializeField] TMP_Text Speedtext;
    [SerializeField] TMP_Text Armourtext;

    [SerializeField] CharacterData characterActor;
    [SerializeField] Attribute attr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPtext.text = "HP: " + attr.Get(AttributeType.HP).ToString();
        PDMGtext.text = "Physic Damage: " + attr.Get(AttributeType.PDamage).ToString();
        MDMGtext.text = "Magic Damage: " + attr.Get(AttributeType.MDamage).ToString();
        Speedtext.text = "Speed: " + attr.Get(AttributeType.Speed).ToString();
        Armourtext.text = "Armour: " + attr.Get(AttributeType.Armour).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {

    }

    public void MyInitialize(CharacterData refCharActor)
    {
        this.characterActor = refCharActor;
        this.attr = characterActor.GetAttribute();
    }
}
