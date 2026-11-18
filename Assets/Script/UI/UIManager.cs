using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject itemUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject EquipmentUI_Prefab;
    private GameObject EquipmentUI;
    private GameObject MainUI;
    private MainCharacter mainCharacter;
    [SerializeField] private Dictionary<EMainUI, GameObject> MainUIDict = new Dictionary<EMainUI, GameObject>();

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
    //public void DeleteEquipmentUI()
    //{
    //    if (EquipmentUI)
    //    {
    //        Destroy(EquipmentUI);
    //    }
    //}
    public void Toggle_UI(EMainUI EtargetUI, bool active, float fadeoutDuration)
    {
        if (EtargetUI == EMainUI.None)
        {
            Debug.Log("No target UI");
            return;
        }

        GameObject targetUI = MainUIDict[EtargetUI];
        StartCoroutine(Fade(targetUI, fadeoutDuration, active));

        return;
    }
    


    public void Toggle_UI(EMainUI EtargetUI, bool active)
    {
        Toggle_UI(EtargetUI, active, 0f);
    }

    public void AutoToggle_UI(EMainUI EtargetUI)
    {
        GameObject targetUI = MainUIDict[EtargetUI];

        bool activeState = targetUI.activeSelf;
        Toggle_UI(EtargetUI, !activeState, 0f);
    }

    public IEnumerator Fade(GameObject target, float fadeoutDuration, bool isActive)
    {
        float elapsedTime = 0f;
        float start = 0f;
        float end = 1f;

        if (isActive)
        {
            start = 0f;
            end = 1f;
        }
        else
        {
            start = 1f;
            end = 0f;
        }

        //ถ้าอยากให้เปิดต้อง set active แล้วค่อยเพิ่ม opacity ---- ผมเสียเวลากับตรงนี้นานมากนะ TT 
        if (isActive)
        {
            target.SetActive(true);
        }

        while (elapsedTime < fadeoutDuration)
        {
            //Debug.Log("elapsedTime : " + elapsedTime + "fadeoutDuration :" + fadeoutDuration);
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / fadeoutDuration;
            float fade = Mathf.Lerp(start, end, progress);
            //Debug.Log("fade : " + fade + "start : " + start + "end :" + end + "progress :" + progress);
            target.GetComponent<CanvasGroup>().alpha = fade;

            yield return null;
        }

        //ถ้าปิดต้องค่อยๆลด opacity แล้วปิด set active
        if (!isActive)
        {
            target.SetActive(false);
        }

    }
}
