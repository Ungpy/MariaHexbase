using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private EMenuBtn role = EMenuBtn.None;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBTN()
    {
        UIManager.Instance.MenuToggle(role);
    }
}
