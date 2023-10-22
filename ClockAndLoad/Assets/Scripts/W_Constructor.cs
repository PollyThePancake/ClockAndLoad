using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_Constructor : MonoBehaviour
{ 
    public W_PartManager barrelStats = null;
    public W_PartManager muzzleStats = null;
    public W_PartManager stockStats = null;
    public W_PartManager triggerStats = null;
    public W_PartManager magazineStats = null;
    public W_PartManager sightStats = null;
    public W_PartManager tacticalStats = null;

    public Sprite handSprite;

    public void ConstructGun() 
    {
        if (barrelStats.barrelPrefab != null) 
        {
            var a = Instantiate(barrelStats.barrelPrefab, transform, false);
            if (a.transform.Find("BarrelPoint").GetComponent<SpriteRenderer>())  { a.transform.Find("BarrelPoint").GetComponent<SpriteRenderer>().sprite = barrelStats.partSprite; }
            if (a.transform.Find("StockPoint").GetComponent<SpriteRenderer>())   { a.transform.Find("StockPoint").GetComponent<SpriteRenderer>().sprite = stockStats.partSprite; }
            if (a.transform.Find("TriggerPoint").GetComponent<SpriteRenderer>()) { 
                a.transform.Find("TriggerPoint").GetComponent<SpriteRenderer>().sprite = triggerStats.partSprite;
                var hand = Instantiate(new GameObject(), a.transform.Find("TriggerPoint"),false);
                hand.transform.localPosition -= new Vector3(0.4f,0.1f,0);
                hand.AddComponent<SpriteRenderer>();
                hand.GetComponent<SpriteRenderer>().sprite = handSprite;
                hand.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
            }
            if (a.transform.Find("MuzzlePoint").GetComponent<SpriteRenderer>())  { a.transform.Find("MuzzlePoint").GetComponent<SpriteRenderer>().sprite = muzzleStats.partSprite; }
            

            a.transform.Rotate(0, 0, 90);
            a.transform.localScale = new Vector3(3f, 3f, 3f);
        }
    }
}
