using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartData", menuName = "CAL/NewPartData")]

public class W_PartManager : ScriptableObject
{
    public enum PartType
    {
        Default,
        Barrel,
        Muzzle,
        Stock,
        Trigger,
        Magazine,
        Sight,
        Tactical,
    }

    
    public Vector2 posOffset;

    public PartType partType;

    public Sprite partSprite, ammoIcon, projectileSprite;
    public GameObject barrelPrefab, bulletPrefab, casingPrefab;
    // ## Barrel
    //[ShowWhen("partType",PartType.Barrel)]
    public int shotCount, ammoCount;
    //[ShowWhen("partType", PartType.Barrel)]
    public float shotSpeed, reloadTime, firerate, damage;

    // 0-10, 10 being the most accurate. The inital accuracy done before spread is calculated
    //[ShowWhen("partType", PartType.Barrel)]
    public float shotAccuracy;

    // 0-180 Angle of the spread that the bullets will take
    //[ShowWhen("partType", PartType.Barrel)]
    public float shotSpread;

    //[ShowWhen("partType", PartType.Barrel)]
    

    // ## Stock &
    // ## Muzzle
    // 0-1, gets additivly multiplied with the base stats
    // i.e., base spread of 4, spread mod of 0.5 = (0.5*4)+4 = 6) 6 total spread
    //[ShowWhen("partType", PartType.Muzzle)]
    public float spreadMod;

    //[ShowWhen("partType", PartType.Stock)]
    public float accuracyMod;

    // ## Trigger
    //[ShowWhen("partType", PartType.Trigger)]
    public float firerateMod;

    // ## Magazine
    //[ShowWhen("partType", PartType.Magazine)]
    
    public float damageMod;


}

