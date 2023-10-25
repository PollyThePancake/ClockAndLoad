using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class P_ShootingController : MonoBehaviour
{

    private int shotCount, ammoCount,currentAmmoCount;
    private float shotSpeed, reloadTime, firerate, damage, shotAccuracy, shotSpread, spreadMod, accuracyMod, firerateMod, damageMod;

    private GameObject bulletPrefab;
    private GameObject casingPrefab;
    public TextMeshProUGUI ammoText;
    public GameObject reloadIndicator;
    public Image ammoImage;
    private Sprite shotImage;

    private CinemachineImpulseSource impulseSource;
    private bool canFire = true;
    private bool canReload = true;

    private void Awake()
    {
        impulseSource= GetComponent<CinemachineImpulseSource>();
    }
    public void InitGun(W_PartManager barrelPart, W_PartManager stockPart, W_PartManager triggerPart, W_PartManager muzzlePart, GameObject gripPoint, Sprite handSprite)
    {
        for (int i = 0; gripPoint.transform.childCount > i; i++)
        {
            Destroy(gripPoint.transform.GetChild(i).gameObject);
        }
        var gun = Instantiate(new GameObject(), gripPoint.transform, false);
        gun.AddComponent<W_Constructor>();
        gun.GetComponent<W_Constructor>().barrelStats = barrelPart;
        gun.GetComponent<W_Constructor>().stockStats = stockPart;
        gun.GetComponent<W_Constructor>().triggerStats = triggerPart;
        gun.GetComponent<W_Constructor>().muzzleStats = muzzlePart;
        gun.GetComponent<W_Constructor>().handSprite = handSprite;
        gun.GetComponent<W_Constructor>().ConstructGun();
        var parts = new List<W_PartManager>
        {
            barrelPart,
            stockPart,
            triggerPart,
            muzzlePart
        };
        GenerateStats(parts);
    }
    public void GenerateStats(List<W_PartManager> parts)
    {
        shotCount = 0; shotSpeed = 0; reloadTime = 0; firerate = 0; damage = 0; shotAccuracy = 0; shotSpread = 0; spreadMod = 0; accuracyMod = 0; firerateMod = 0; damageMod = 0;
        ammoCount = 0;
        foreach (var part in parts) 
        {
            
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
            ammoCount += part.ammoCount;
            if (part.partType == W_PartManager.PartType.Barrel)
            {
                bulletPrefab = part.bulletPrefab;
                casingPrefab = part.casingPrefab;
                shotCount += part.shotCount;
                ammoImage.sprite = part.ammoIcon;
                shotImage = part.projectileSprite;
            }

        }
        shotAccuracy = (accuracyMod * shotAccuracy) + shotAccuracy;
        shotSpread = (shotSpread * spreadMod) - shotSpread;
        firerate = (firerateMod * firerate) + firerate;
        damage = (damageMod * damage) + damage;
        currentAmmoCount = ammoCount;
        UpdateCounter();
    }

    public void FireWeapon(Transform direction)
    {
        if (shotCount > 0 && currentAmmoCount > 0 && canFire) 
        {
            for (int i = 0;i < shotCount; i++) 
            {
                var b = Instantiate(bulletPrefab, direction.position, Quaternion.identity);
                b.transform.right = direction.up;
                b.GetComponent<SpriteRenderer>().sprite = shotImage;
                b.transform.Rotate(0f,0f, Random.Range(-shotSpread, shotSpread));
                b.GetComponent<W_BulletController>().shotSpeed = shotSpeed;
                b.GetComponent<W_BulletController>().Move();

            }
            var c = Instantiate(casingPrefab, direction.position, Quaternion.identity);
            c.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-500, 500), Random.Range(-500, 500)));
            c.transform.Rotate(0f,0f,Random.Range(0, 360));
            impulseSource.GenerateImpulseWithForce(0.1f);
            currentAmmoCount--;
            UpdateCounter();
            canFire = false;
            StartCoroutine(FirerateLimiter());

        }
        
    }
    public void UpdateCounter()
    {
        ammoText.text = $"{currentAmmoCount} / {ammoCount}";
    }
    public void ReloadWeapon()
    {
        reloadIndicator.SetActive(false);
        if (currentAmmoCount != ammoCount && canReload)
        {
            StartCoroutine(ReloadLimiter());
            StartCoroutine(InterpelateRoutine(reloadTime));
        }
        reloadIndicator.SetActive(true);
    }

    IEnumerator ReloadLimiter()
    {
        canReload = false;
        canFire = false;
        yield return new WaitForSeconds(reloadTime);
        currentAmmoCount = ammoCount;
        UpdateCounter() ;
        canFire = true;
        canReload = true;
    }
    IEnumerator FirerateLimiter()
    {
        canFire = false;
        yield return new WaitForSeconds(firerate);
        canFire = true;
    }
    IEnumerator InterpelateRoutine(float duration)
    {
        reloadIndicator.SetActive(true);
        float lerp = 0;
        while (duration > 0 && lerp < 1)
        {
            lerp = Mathf.MoveTowards(lerp, 1, Time.deltaTime / duration);
            reloadIndicator.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }
        reloadIndicator.GetComponent<Image>().fillAmount = 1;
        reloadIndicator.SetActive(false);
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
