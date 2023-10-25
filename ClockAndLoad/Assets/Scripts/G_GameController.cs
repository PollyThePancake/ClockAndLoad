using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class G_GameController : MonoBehaviour
{
    public List<W_PartManager> partList;
    public GameObject dropDownSelection;

    [HideInInspector]
    public List<W_PartManager> barrelParts, stockParts, muzzleParts, triggerParts;
    private GameObject barrelDropdown, stockDropdown, muzzleDropdown, triggerDropdown;

    
    private void Awake()
    {
        barrelDropdown = dropDownSelection.transform.Find("BarrelSelect").gameObject;
        stockDropdown = dropDownSelection.transform.Find("StockSelect").gameObject;
        triggerDropdown = dropDownSelection.transform.Find("TriggerSelect").gameObject;
        muzzleDropdown = dropDownSelection.transform.Find("MuzzleSelect").gameObject;
        foreach (var part in partList)
        {
            var a = new TMP_Dropdown.OptionData();
            a.text = part.name;
            a.image = part.partSprite;
            switch (part.partType) 
            {
                case W_PartManager.PartType.Barrel:
                    barrelDropdown.GetComponent<TMP_Dropdown>().options.Add(a);
                    barrelParts.Add(part);
                    break;

                case W_PartManager.PartType.Stock:
                    stockDropdown.GetComponent<TMP_Dropdown>().options.Add(a);
                    stockParts.Add(part);
                    break;

                case W_PartManager.PartType.Muzzle:
                    muzzleDropdown.GetComponent<TMP_Dropdown>().options.Add(a);
                    muzzleParts.Add(part);
                    break;

                case W_PartManager.PartType.Trigger:
                    triggerDropdown.GetComponent<TMP_Dropdown>().options.Add(a);
                    triggerParts.Add(part);
                    break;
            }
        }


        
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            dropDownSelection.SetActive(!dropDownSelection.activeSelf);
        }
    }
    public  GameObject gunConstructor;
}
