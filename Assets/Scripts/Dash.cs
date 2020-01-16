using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private int maxDashes;

    public static float dashingTime = 0.2f;
    public static float dashSpeed = 4.75f;
    public static int dashes;
    public static float dashRechargeTime;
    public static float dashTimer;

    public int Dashes { get { return dashes; } set { dashes = value; } }
    public float DashTimer { get { return dashTimer; } set { dashTimer = value; } }
    public float DashSpeed { get { return dashSpeed; } set { dashSpeed = value; } }
    public float DashingTime { get { return dashingTime; } set { dashingTime = value; } }
    public float DashRechargeTime { get { return dashRechargeTime; } set { dashRechargeTime = value; } }

    void Start()
    {
        dashes = 4;
        maxDashes = 4;
        dashRechargeTime = 2.5f;
        dashTimer = 0f;
    }

    void Update()
    {
        if (dashes < maxDashes)
        {
            dashTimer += Time.deltaTime;
        }

        if (dashTimer >= dashRechargeTime && dashes < maxDashes)
        {
            DashRecharge();
        }
    }

    private void DashRecharge()
    {
        dashes++;
        dashTimer = 0;
        AudioManager.instance.PlaySound(21);
    }

    public void ConsumeDash()
    {
        if (dashes > 0)
        {
            dashes--;
        }
    }
}
