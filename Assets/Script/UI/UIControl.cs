using System;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public static UIControl UIController;
    public GameObject itemUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject EquipmentUI_Prefab;
    private GameObject EquipmentUI;

    private bool isitemUI;

    private void Awake()
    {
        UIController = this;
    }

    public event Action<CharacterData> OnStatsChange;
    public void StatsChange(CharacterData chardata)
    {
        if (OnStatsChange != null)
        {
            OnStatsChange(chardata);
        }
    }




    public void CreateEquipmentUI(CharacterData charData)
    {
        EquipmentUI = Instantiate(EquipmentUI_Prefab);
        EquipmentUI.GetComponent<StatusUI>().MyInitialize(charData);
    }
    public void DeleteEquipmentUI()
    {
        if (EquipmentUI)
        {
            Destroy(EquipmentUI);
        }
    }

    public void ItemUI_Toggle()
    {
        if (isitemUI)
        {
            itemUI.SetActive(false);
            isitemUI = false;
        }
        else
        {
            itemUI.SetActive(true);
            isitemUI = true;
        }
    }

    public void UI_Toggle(GameObject targetUI)
    {
        if (targetUI == null)
        {
            Debug.Log("target UI is null");
            return;
        }

        if (targetUI.activeSelf == true)
        {
            itemUI.SetActive(false);
            isitemUI = false;
        }
        else
        {
            itemUI.SetActive(true);
            isitemUI = true;
        }
        return;
    }
}
