using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private UIManager uiManager;
    public static float dashingTime = 0.2f; //0.2f
    public static float dashSpeed = 4.75f; //4f
    //private Color dashColor = new Color(0, 1, 0, 1);
    //public static float dashInvulnerabilityDelay = 0.5f;

    public static int dashes;
    private int maxDashes;
    public static float dashRechargeTime;
    public static float dashTimer;
    private PlayerStats stats;
    //private PlayerController pc;

    //public GameObject playerObject;


    public int Dashes { get { return dashes; } set { dashes = value; } }
    public float DashTimer { get { return dashTimer; } set { dashTimer = value; } }
    public float DashSpeed { get { return dashSpeed; } set { dashSpeed = value; } }
    public float DashingTime { get { return dashingTime; } set { dashingTime = value; } }
    //public float DashInvulnerabilityDelay { get { return dashInvulnerabilityDelay; } set { dashInvulnerabilityDelay = value; } }
    public float DashRechargeTime { get { return dashRechargeTime; } set { dashRechargeTime = value; } }

    // Start is called before the first frame update
    void Start()
    {
        dashes = 4;
        maxDashes = 4;
        dashRechargeTime = 2.5f;
        dashTimer = 0f;
        stats = GameManager.instance.PlayerStats;
        uiManager = FindObjectOfType<UIManager>();
    }

        // Update is called once per frame
        void Update()
    {
        if(dashes < maxDashes)
        {
            dashTimer += Time.deltaTime;
        }

        if(dashTimer >= dashRechargeTime && dashes < maxDashes)
        {
            DashRecharge();
        }
    }

    private void DashRecharge()
    {
        dashes++;
        dashTimer = 0;
        //uiManager.UpdateDashes(GameManager.instance.Dash);
    }

    public void ConsumeDash()
    {
        if(dashes > 0) 
        {
            dashes--;
        }
    }
}
