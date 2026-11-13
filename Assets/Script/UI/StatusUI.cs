using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] TMP_Text HPText;
    [SerializeField] TMP_Text PDMGText;
    [SerializeField] TMP_Text MDMGText;
    [SerializeField] TMP_Text SpeedText;
    [SerializeField] TMP_Text ArmourText;

    [SerializeField] CharacterData characterActor;
    [SerializeField] AttributeData attr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HPtext.text = "HP: " + attr.Get(AttributeType.HP).ToString();
        //PDMGtext.text = "P DMG: " + attr.Get(AttributeType.PDamage).ToString();
        //MDMGtext.text = "M DMG: " + attr.Get(AttributeType.MDamage).ToString();
        //Speedtext.text = "Speed: " + attr.Get(AttributeType.Speed).ToString();
        //Armourtext.text = "Armour: " + attr.Get(AttributeType.Armour).ToString();
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
        //this.characterActor = refCharActor;
        //this.attr = characterActor.GetAttribute();
    }
}
