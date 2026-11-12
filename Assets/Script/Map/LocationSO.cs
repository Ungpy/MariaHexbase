using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Location", menuName = "Scriptable Objects/LocationSO")]
public class LocationSO : ScriptableObject
{
    public string locationName;
    public Sprite bgImage;
    public GameObject Btn_Set;
}