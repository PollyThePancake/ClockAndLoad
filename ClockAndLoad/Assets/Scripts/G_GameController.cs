using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_GameController : MonoBehaviour
{
    public List<W_PartManager> partList;

    [HideInInspector]
    public List<W_PartManager> barrelParts, stockParts, muzzleParts, triggerParts;

    
    private void Awake()
    {
        foreach(var part in partList)
        {
            switch(part.partType) 
            {
                case W_PartManager.PartType.Barrel:
                    barrelParts.Add(part);
                    break;

                case W_PartManager.PartType.Stock:
                    stockParts.Add(part);
                    break;

                case W_PartManager.PartType.Muzzle:
                    muzzleParts.Add(part);
                    break;

                case W_PartManager.PartType.Trigger:
                    triggerParts.Add(part);
                    break;
            }
        }       
    }


    public  GameObject gunConstructor;
}
