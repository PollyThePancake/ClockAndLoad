using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class P_ShootingController : MonoBehaviour
{

    private int shotCount;
    private float shotSpeed, reloadTime, firerate, damage, shotAccuracy, shotSpread, spreadMod, accuracyMod, firerateMod, damageMod;

    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public TextMeshPro ammoText;

    private CinemachineImpulseSource impulseSource;
    private int ammoCount;
    private bool canFire = true;

    private void Awake()
    {
        impulseSource= GetComponent<CinemachineImpulseSource>();
    }
    public void InitGun(List<W_PartManager> partList, GameObject gripPoint, Sprite handSprite)
    {
        for (int i = 0; gripPoint.transform.childCount > i; i++)
        {
            Destroy(gripPoint.transform.GetChild(i).gameObject);
        }
        var gun = Instantiate(new GameObject(), gripPoint.transform, false);
        gun.AddComponent<W_Constructor>();
        gun.GetComponent<W_Constructor>().barrelStats = partList[0];
        gun.GetComponent<W_Constructor>().stockStats = partList[1];
        gun.GetComponent<W_Constructor>().triggerStats = partList[2];
        gun.GetComponent<W_Constructor>().muzzleStats = partList[3];
        gun.GetComponent<W_Constructor>().handSprite = handSprite;
        gun.GetComponent<W_Constructor>().ConstructGun();
        GenerateStats(partList);
    }
    public void GenerateStats(List<W_PartManager> parts)
    {
        shotCount = 0; shotSpeed = 0; reloadTime = 0; firerate = 0; damage = 0; shotAccuracy = 0; shotSpread = 0; spreadMod = 0; accuracyMod = 0; firerateMod = 0; damageMod = 0;
        ammoCount = 0;
        foreach (var part in parts) 
        {
            shotCount += part.shotCount;
            shotSpeed += part.shotSpeed;
            reloadTime += part.reloadTime;
            firerate += part.firerate;
            damage += part.damage;
            shotAccuracy += part.shotAccuracy;
            shotSpread += part.shotSpread;
            spreadMod += part.spreadMod;
            accuracyMod += part.accuracyMod;
            firerateMod += part.firerateMod;
            damageMod += part.damageMod;
        }
        shotAccuracy = (accuracyMod * shotAccuracy) + shotAccuracy;
        shotSpread = (shotSpread * spreadMod) - shotSpread;
        firerate = (firerateMod * firerate) + firerate;
        damage = (damageMod * damage) + damage;
        ammoCount = shotCount;
        UpdateCounter();
    }

    public void FireWeapon(Transform direction)
    {
        if (shotCount > 0 && ammoCount > 0 && canFire) 
        {
            for (int i = 0;i < shotCount; i++) 
            {
                var b = Instantiate(bulletPrefab, direction.position, Quaternion.identity);
                b.transform.right = direction.up;
                b.transform.Rotate(0f,0f, Random.Range(-shotSpread, shotSpread));
                b.GetComponent<W_BulletController>().shotSpeed = shotSpeed;
                b.GetComponent<W_BulletController>().Move();

            }
            var c = Instantiate(casingPrefab, direction.position, Quaternion.identity);
            c.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-500, 500), Random.Range(-500, 500)));
            c.transform.Rotate(0f,0f,Random.Range(0, 360));
            impulseSource.GenerateImpulseWithForce(0.1f);
            ammoCount--;
            UpdateCounter();
            canFire = false;
            StartCoroutine(FirerateLimiter());

        }
        
    }
    public void UpdateCounter()
    {
        ammoText.text = $"{ammoCount} / {shotCount}";
    }
    public void ReloadWeapon()
    {
        ammoCount = shotCount;
        UpdateCounter();
    }

    IEnumerator FirerateLimiter()
    {
        canFire = false;
        yield return new WaitForSeconds(firerate);
        canFire = true;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 0, 150, 40), $"Shot Count   : {shotCount}");
        GUI.Label(new Rect(10, 20, 150, 40), $"Shot Speed   : {shotSpeed}");
        GUI.Label(new Rect(10, 40, 150, 40), $"Shot Spread  : {shotSpread}");
        GUI.Label(new Rect(10, 60, 150, 40), $"Reload Time : {reloadTime}");
        GUI.Label(new Rect(10, 80, 150, 40), $"Firerate    : {firerate}");
        GUI.Label(new Rect(10, 100, 150, 40), $"Damage      : {damage}");
        GUI.Label(new Rect(10, 120, 150, 40), $"Shot Acc    : {shotAccuracy}");
        GUI.Label(new Rect(10, 140, 150, 40), $"Spread Mod  : {spreadMod}");
        GUI.Label(new Rect(10, 160, 150, 40), $"Acc Mod     : {accuracyMod}");
        GUI.Label(new Rect(10, 180, 150, 40), $"Firerate Mod: {firerateMod}");
        GUI.Label(new Rect(10, 200, 150, 40), $"Damage Mod  : {damageMod}");

    }
}
