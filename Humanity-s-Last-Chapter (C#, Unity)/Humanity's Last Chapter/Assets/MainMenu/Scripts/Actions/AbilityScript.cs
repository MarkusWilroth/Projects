using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript : MonoBehaviour
{
    public GameObject Turret;
    public GameObject AttachedSlot;
    public AbilityType abilityType;
    public GameObject ForMouse;
    public GameObject Projectile;
    bool active = false, canBeActivated = true;
    GameObject drawUnderMouse;
    Vector3 mousePos;
    float grenadeRange = 6;
    public enum AbilityType
    {
        turret,
        grenade,
        drugz,
        bandages
    }
    // Start is called before the first frame update
    void Start()
    {
        drawUnderMouse = Instantiate(ForMouse);
        drawUnderMouse.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (active) {
            Debug.Log("Active: " + active);
            switch (abilityType)
            {
                case AbilityType.turret:
                    TurretActivate();
                    break;
                case AbilityType.grenade:
                    GrenadeActivate();
                    break;
                default:
                    break;
            }
        }
    }

    public void Activate()
    {
        drawUnderMouse = Instantiate(ForMouse);
        drawUnderMouse.SetActive(false);
        //if (!canBeActivated)
        //    return;
        switch (abilityType)
        {
            case AbilityType.turret:
                active = true;
                drawUnderMouse.SetActive(true);
                mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                drawUnderMouse.transform.localScale = new Vector3(1,1,1);
                break;
            case AbilityType.grenade:
                active = true;
                drawUnderMouse.SetActive(true);
                mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                drawUnderMouse.transform.localScale = new Vector3(15, 15, 1);
                break;
            default:
                break;
        }
    }
    public void GrenadeActivate()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        drawUnderMouse.transform.position = mousePos;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            active = false;
            drawUnderMouse.SetActive(false);
            canBeActivated = false;
            GrenadeThrow();
        }
    }
    public void GrenadeThrow()
    {
        GameObject go = Instantiate(Projectile);
        go.GetComponent<GrenadeScript>().TargetPos = mousePos;
    }

    public void TurretActivate()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        drawUnderMouse.transform.position = mousePos;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            active = false;
            drawUnderMouse.SetActive(false);
            canBeActivated = false;
            SpawnTurret();
        }
    }
    public void SpawnTurret()
    {
        GameObject go = Instantiate(Turret);
        go.transform.position = mousePos;
    }
}
