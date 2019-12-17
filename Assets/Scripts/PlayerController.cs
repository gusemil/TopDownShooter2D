using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    //private float dashTime = 0.2f; //0.2f
    //private float dashSpeed = 5f; //4f
    //private float dashInvulnerabilityDelay = 0.5f;

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject bulletPreFab;
    public GameObject bombEffect;
    
    public Rigidbody2D rb2D;
    public Camera _camera; //variable name 'camera' is not available

    Vector2 movement;
    Vector2 mousePosition;

    private Color originalColor;
    private Color powerUpColor = new Color(0, 1, 1, 1);
    private Color dashColor = new Color(0, 1, 0, 1);
    private Color previousColor;

    public Color OriginalColor { get { return originalColor; } }
    public Color PowerUpColor { get { return powerUpColor; } }
    public Color PreviousColor {  get { return previousColor; }}
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    private WeaponSystem weapon;
    private Dash dash;
    
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        weapon = GameManager.instance.WeaponSystem;
        dash = GameManager.instance.Dash;
        //dash = new Dash();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + rb2D.velocity.x;
        movement.y = Input.GetAxisRaw("Vertical") + rb2D.velocity.y;

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1")) //mouse1
        {
            if(weapon.ShotTimer >= weapon.CurrentWeapon.FireCoolDown)
            {
                Shoot();
                weapon.ShotTimer = 0;
            }
        }

        if (Input.GetButtonUp("Fire2")) //mouse2
        {
            if (weapon.BombCount > 0)
            {
                GameObject effect = Instantiate(bombEffect, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
                Destroy(effect, 0.1f); //hit effect tuhoutuu 0.1sek
            }

            weapon.Bomb();
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            GodMode();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(dash.Dashes > 0)
            {
                dash.ConsumeDash();
                StartCoroutine(Dash());
            }
        }

    }

    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //rotaatiosetit: Miten kierretään hahmoa että osoitetaan mousepositioon
        rb2D.rotation = angle;
    }

    private void Shoot()
    {
        Debug.Log(weapon.CurrentWeapon.WeaponName);

        if(weapon.CurrentWeapon.Ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

            weapon.LoseAmmo();

            if(weapon.CurrentWeapon.WeaponName == "shotgun")
            {
                GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.AddForce(firePoint2.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation);
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.AddForce(firePoint3.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);
            }

        }

    }

    private void GodMode()
    {
        PlayerStats player = GameManager.instance.PlayerStats;
        if (player.IsInvulnerable == false)
        {
            player.IsInvulnerable = true;
            moveSpeed = 20f;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1);
            this.gameObject.layer = 11;

            weapon.WeaponList[1].Ammo += 10000;
            weapon.WeaponList[2].Ammo += 10000;
            weapon.WeaponList[3].Ammo += 10000;
            weapon.WeaponList[4].Ammo += 10000;
        }
    }

    


    public void ChangePlayerColor(Color color)
    {
        PlayerStats player = GameManager.instance.PlayerStats;

        if (!player.IsDashing && !player.IsPoweredUp)
        {
            previousColor = originalColor;
            GetComponent<SpriteRenderer>().color = originalColor;

        } else
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    public void SetPreviousColor()
    {
        PlayerStats player = GameManager.instance.PlayerStats;

        if(!player.IsDashing && !player.IsPoweredUp)
        {
            previousColor = originalColor;
            GetComponent<SpriteRenderer>().color = originalColor;
        } else
        {
            previousColor = GetComponent<SpriteRenderer>().color;
        }
    }

    
    private IEnumerator Dash()
    {
        PlayerStats player = GameManager.instance.PlayerStats;

        player.IsDashing = true;
        while (player.IsDashing)
        {
            SetPreviousColor();
            //ChangePlayerColor(dashColor);
            
            if (Input.GetAxisRaw("Horizontal") > 0)
                //movement.x +=  0.1f;
                rb2D.velocity = Vector2.right * dash.DashSpeed;
            else if (Input.GetAxisRaw("Horizontal") < 0)
                //movement.x -= 0.1f;
                rb2D.velocity = Vector2.left * dash.DashSpeed;

            if (Input.GetAxisRaw("Vertical") > 0)
                //movement.y += 0.1f;
                rb2D.velocity = Vector2.up * dash.DashSpeed;
            else if (Input.GetAxisRaw("Vertical") < 0)
                //movement.y -= 0.1f;
                rb2D.velocity = Vector2.down * dash.DashSpeed;

            //GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.layer = 11; //Dash Layer
            yield return new WaitForSeconds(dash.DashTime);
            this.gameObject.layer = 10; //Player Layer
            //GetComponent<BoxCollider2D>().enabled = true;
            rb2D.velocity = Vector2.zero;
            player.IsDashing = false;
            player.IsInvulnerable = true;
            yield return new WaitForSeconds(dash.DashInvulnerabilityDelay);
            player.IsInvulnerable = false;
            //ChangePlayerColor(previousColor);
        }

    }
    

}
