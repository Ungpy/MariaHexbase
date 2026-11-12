using UnityEngine;

public class LocationManager : MonoBehaviour
{
    //public static Sprite GameObject sceneBGImage
    [SerializeField] private LocationListSO locationList;
    [SerializeField] private GameObject sceneBGImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LocationSO targetLocation = locationList.GetLocation("Train");
        ChangeBackground(targetLocation.bgImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeBackground(Sprite sprite)
    {
        sceneBGImage.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
