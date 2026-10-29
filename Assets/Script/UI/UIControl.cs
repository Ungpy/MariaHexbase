using UnityEngine;

public class UIControll : MonoBehaviour
{
    public GameObject itemUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isitemUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
