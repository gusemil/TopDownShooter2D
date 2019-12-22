using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public static float dashTime = 0.2f; //0.2f
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
    public float DashTime { get { return dashTime; } set { dashTime = value; } }
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

       // pc = playerObject.GetComponent<PlayerController>();

        //Debug.Log("movespeed" + pc.MoveSpeed);
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
    }

    public void ConsumeDash()
    {
        if(dashes > 0) 
        {
            dashes--;
            //dashTimer = 0;
        }
    }

    /*
    public IEnumerator DashIENumerator()
    {
        PlayerStats player = GameManager.instance.PlayerStats;

        player.IsDashing = true;
        while (player.IsDashing)
        {
            //SetPreviousColor();
            //ChangePlayerColor(dashColor);

            if (Input.GetAxisRaw("Horizontal") > 0)
                //movement.x +=  0.1f;
                pc.rb2D.velocity = Vector2.right * dashSpeed;
            else if (Input.GetAxisRaw("Horizontal") < 0)
                //movement.x -= 0.1f;
                pc.rb2D.velocity = Vector2.left * dashSpeed;

            if (Input.GetAxisRaw("Vertical") > 0)
                //movement.y += 0.1f;
                pc.rb2D.velocity = Vector2.up * dashSpeed;
            else if (Input.GetAxisRaw("Vertical") < 0)
                //movement.y -= 0.1f;
                pc.rb2D.velocity = Vector2.down * dashSpeed;

            //GetComponent<BoxCollider2D>().enabled = false;
            pc.gameObject.layer = 11; //Dash Layer
            yield return new WaitForSeconds(dashTime);
            pc.gameObject.layer = 10; //Player Layer
            //GetComponent<BoxCollider2D>().enabled = true;
            pc.rb2D.velocity = Vector2.zero;
            player.IsDashing = false;
            player.IsInvulnerable = true;
            yield return new WaitForSeconds(dashInvulnerabilityDelay);
            player.IsInvulnerable = false;
            //ChangePlayerColor(previousColor);
        } //while
    } //method
    */
}
