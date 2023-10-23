using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class P_GeneralHandler : MonoBehaviour
{
    public GameObject GameController;
    public List<W_PartManager> partList;
    [HideInInspector]
    public List<W_PartManager> barrelParts, stockParts, muzzleParts, triggerParts;
    public Sprite handSprite;

    // Movement
    [Header("Movement")]
    public float moveSpeed = 5;
    public Rigidbody2D rb;
    private Vector2 movement;

    [Header("Grip Rotation")]
    public GameObject gripPivot;
    public GameObject gripPoint;
    public Camera mainCamera;


    private P_ShootingController shootingController;


    private void Start()
    {
        shootingController = transform.GetComponent<P_ShootingController>();
        var gc = GameController.GetComponent<G_GameController>();
        partList = gc.partList;
        barrelParts = gc.barrelParts;
        stockParts = gc.stockParts;
        muzzleParts = gc.muzzleParts;
        triggerParts = gc.triggerParts;
    }
    private void Update()
    {

        //debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shootingController.InitGun(barrelParts,stockParts,triggerParts,muzzleParts,gripPoint,handSprite);
        }

        //  Fire
        if (Input.GetMouseButton(0))
        {
            shootingController.FireWeapon(gripPoint.transform);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            shootingController.ReloadWeapon();
        }


        //  Movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        //  Movement
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        RotateToMouse();
    }

    // Grip Rotation
    public void RotateToMouse()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        gripPivot.transform.up = mousePos - new Vector2(transform.position.x, transform.position.y);
        if (gripPivot.transform.localEulerAngles.z > 180) { gripPivot.transform.localScale = new Vector3(1, 1, 1); }
        else { gripPivot.transform.localScale = new Vector3(-1,1,1);  }
    }
}
