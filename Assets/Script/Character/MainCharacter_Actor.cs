using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacter_Actor : MonoBehaviour, IPointerEnterHandler
{
    CharacterData charData = new CharacterData(-1, "Misa", ClassType.magic, new Attribute(10f, 10f, 10f, 10f, 10f));
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseEnter()
    //{
    //    UIControl.UIController.StatsChange(charData);
    //    Debug.Log("MouseENter");
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIControl.UIController.StatsChange(charData);
        Debug.Log("MouseENter");
    }
}
