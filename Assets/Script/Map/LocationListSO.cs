using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationList", menuName = "Scriptable Objects/LocationListSO")]
public class LocationListSO : ScriptableObject
{
    //ค่อยแปลงเป็น Dict ทีหลัง
    public List<LocationSO> allLocation = new List<LocationSO>();

    public LocationSO GetLocation(string targetlocationName)
    {
        return allLocation.Find(location => location.locationName == targetlocationName);
    }
}
