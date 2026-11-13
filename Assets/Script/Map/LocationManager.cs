using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance { get; private set; }
    //public static Sprite GameObject sceneBGImage
    [SerializeField] private LocationListSO AlllocationList;
    [SerializeField] private GameObject sceneBGImage;

    [SerializeField] private LocationJumper prefab_LocationJumper;
    [SerializeField] private GameObject jumperContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SetupJumper(new List<LocationSO> { AlllocationList.GetLocation("Train"), AlllocationList.GetLocation("Mountain") });

        //LocationSO targetLocation = AlllocationList.GetLocation("Train");
        //ChangeBackground(targetLocation.bgImage);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupJumper(List<LocationSO> LocationList)
    {
        foreach (var item in LocationList)
        {
            LocationJumper jumper = Instantiate(prefab_LocationJumper, jumperContainer.transform);
            jumper.Setup(item);
        }
    }
    public void ChangeLocation(LocationSO targetLocation)
    {
        ChangeBackground(targetLocation.bgImage);
    }
    public void ChangeBackground(Sprite sprite)
    {
        sceneBGImage.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
