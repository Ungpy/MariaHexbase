using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacterUI : MonoBehaviour
{
    [SerializeField] TMP_Text NameTxt;
    [SerializeField] TMP_Text HPTxt;
    [SerializeField] TMP_Text MPTxt;
    [SerializeField] TMP_Text StaminaTxt;
    [SerializeField] TMP_Text MoodTxt;
    [SerializeField] TMP_Text LevelTxt;
    [SerializeField] TMP_Text ExpTxt;

    //[SerializeField] CharacterData characterActor;
    //[SerializeField] Attribute attr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ฟังว่าถ้ามี OnStatsChange มาก็ให้ทำ RefreshText
        UIControl.UIController.OnStatsChange += RefreshText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {

    }

    private void RefreshText(CharacterData charData)
    {
        NameTxt.text = charData.Getname();
        //HPTxt.text = "Health  " + charData.Get(AttributeType.HP).ToString();
        //MPTxt.text = "Mana   " + attr.Get(AttributeType.PDamage).ToString();
        //StaminaTxt.text = "Stamina " + attr.Get(AttributeType.MDamage).ToString();
        //MoodTxt.text = "Mood   " + attr.Get(AttributeType.Speed).ToString();
        //LevelTxt.text = "Level " + attr.Get(AttributeType.Armour).ToString();
        //ExpTxt.text = "Exp " + attr.Get(AttributeType.Armour).ToString();
    }

    public void MyInitialize(CharacterData refCharActor)
    {
        //this.characterActor = refCharActor;
        //this.attr = characterActor.GetAttribute();
    }
}
