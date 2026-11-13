using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterActor : MonoBehaviour
{
    private CharacterData charData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //charData = new CharacterData(-1, "Misa", ClassType.magic, new Attribute(20f, 2f, 4f, 2f, 1f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        UIManager.Instance.CreateEquipmentUI(charData);
    }

    private void OnMouseExit()
    {
        UIManager.Instance.DeleteEquipmentUI();
    }
}
