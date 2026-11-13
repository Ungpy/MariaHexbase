using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainCharacter : MonoBehaviour, IPointerEnterHandler
{
    CharacterData charData = new CharacterData(-1, "MisaMMM", ClassType.magic, new AttributeData(10f, 10f, 10f, 10f, 10f));

    public event Action<CharacterData> OnStatChange;

    private void Start()
    {
        
    }

    //private void OnMouseEnter()
    //{
    //    UIControl.UIController.StatsChange(charData);
    //    Debug.Log("MouseENter");
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnStatChange != null)
        {
            OnStatChange(charData);
        }
        //UIManager.Instance.StatsChange(charData);
        Debug.Log("MouseENter");
    }
}
