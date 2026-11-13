using UnityEngine;
using UnityEngine.UI;

public class LocationJumper : MonoBehaviour
{
    LocationSO location;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnBtnClick()
    {
        LocationManager.Instance.ChangeLocation(location);
    }

    public void Setup(LocationSO targetlocation)
    {
        if(targetlocation != null)
        { 
            location = targetlocation;
            Debug.Log("Setup Jumper " + location.locationName);
        }
        else
        {
            Debug.LogWarning("targetlocation Is null");
        }

        ChangeBG(location.bgImage);
    }

    public void ChangeBG(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

}
