using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject itemUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject EquipmentUI_Prefab;
    private GameObject EquipmentUI;
    private MainCharacter mainCharacter;

    private bool isitemUI;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //mainCharacter.OnStatChange += ;
    }

    //public event Action<CharacterData> OnStatsChange;
    //public void StatsChange(CharacterData chardata)
    //{
    //    //ถ้ามีฟังก์ชั่นที่ฟังอยู่
    //    if (OnStatsChange != null)
    //    {
    //        OnStatsChange(chardata);
    //    }
    //}




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
