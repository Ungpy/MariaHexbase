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

    [SerializeField] MainCharacter mainCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ฟังว่าถ้ามี OnStatsChange มาก็ให้ทำ RefreshText
        mainCharacter.OnStatChange += RefreshText;
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
        HPTxt.text = "Health  " + charData.Health.Current.ToString() + "/" + charData.Health.Max.ToString();
        MPTxt.text = "Mana   " + charData.Mana.Current.ToString() + "/" + charData.Mana.Max.ToString();
        StaminaTxt.text = "Stamina " + charData.Stamina.Current.ToString() + "/" + charData.Stamina.Max.ToString();
        MoodTxt.text = "Mood   " + charData.Mood.Current.ToString() + "/" + charData.Mood.Max.ToString();
        //LevelTxt.text = "Level " + charData.leve
        //ExpTxt.text = "Exp " + charData.Health.Current.ToString();
    }

    public void MyInitialize(CharacterData refCharActor)
    {
        //this.characterActor = refCharActor;
        //this.attr = characterActor.GetAttribute();
    }
}
