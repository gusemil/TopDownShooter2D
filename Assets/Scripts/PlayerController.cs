using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 7.5f;
    //private AudioSource gunSound;
    //private float dashTime = 0.2f; //0.2f
    //private float dashSpeed = 5f; //4f
    //private float dashInvulnerabilityDelay = 0.5f;

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject shield;
    public GameObject bulletPreFab;
    public GameObject bombEffect;
    public GameObject hexDamagePreFab;
    private GameObject hexDamageEffect;
    public GameObject infiniteDashPreFab;
    private GameObject infiniteDashEffect;
    public GameObject infiniteAmmoPreFab;
    private GameObject infiniteAmmoEffect;
    public GameObject godModePreFab;
    private GameObject godModeEffect;

    public Rigidbody2D rb2D;
    public Camera _camera; //variable name 'camera' is not available

    Vector2 movement;
    Vector2 mousePosition;

    private Color originalColor;
   // private Color hexDamageColor = new Color(1, 0.5f, 0.5f, 1);
    private Color dashColor = new Color(1, 1, 0, 1);
   // private Color infiniteAmmoColor = new Color(0.5f, 1, 0.5f, 1);
   // private Color godModeColor = new Color(0.06f,0.92f,0.9f,1);
   // private Color previousColor;
    private UIManager uiManager;

    public Color OriginalColor { get { return originalColor; } }
    //public Color HexDamageColor { get { return hexDamageColor; } }
    //public Color InfiniteAmmoColor { get { return infiniteAmmoColor; } }
    //public Color PreviousColor {  get { return previousColor; }}
    //public Color GodModeColor { get { return godModeColor; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    private WeaponSystem weapon;
    private Dash dash;
    private Pause pause;
    private PlayerStats stats;

    private Vector3 minScreenSize;
    private Vector3 maxScreenSize;
    private Vector3 playerSize;

    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        weapon = GameManager.instance.WeaponSystem;
        dash = GameManager.instance.Dash;
        pause = GameManager.instance.Pause;
        stats = GameManager.instance.PlayerStats;

        playerSize = transform.localScale;
        TurnOffShieldGraphic();

        minScreenSize = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenSize = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateWeaponText(weapon.CurrentWeapon);
        uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        uiManager.UpdateBombText(weapon);

        //gunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + rb2D.velocity.x;
        movement.y = Input.GetAxisRaw("Vertical") + rb2D.velocity.y;

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1") && !pause.IsPause && !stats.IsRespawning) //mouse1
        {
            if(weapon.ShotTimer >= weapon.CurrentWeapon.FireCoolDown)
            {
                Shoot();
                weapon.ShotTimer = 0;
            }
        }

        if (Input.GetButtonUp("Fire2") && !pause.IsPause && !stats.IsRespawning) //mouse2
        {
            if (weapon.BombCount > 0)
            {
                GameObject effect = Instantiate(bombEffect, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
                Destroy(effect, 0.1f); //hit effect tuhoutuu 0.1sek
            }

            weapon.Bomb();
            uiManager.UpdateBombText(weapon);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !pause.IsPause && !stats.IsRespawning)
        {
            if(dash.Dashes > 0 || stats.IsInfiniteDashUp || stats.IsGodModeUp)
            {
                AudioManager.instance.PlaySound(20);

                if (!stats.IsInfiniteDashUp && !stats.IsGodModeUp)
                {
                    dash.ConsumeDash();
                    //uiManager.UpdateDashes(dash);
                }

                StartCoroutine(Dash());
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            weapon.ChangeWeapon(0);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            weapon.ChangeWeapon(1);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            weapon.ChangeWeapon(2);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }


            if (Input.GetKeyUp(KeyCode.Alpha4) && LevelManager.instance.CurrentLevel >= 2)
        {
            weapon.ChangeWeapon(3);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5) && LevelManager.instance.CurrentLevel >= 3)
        {
            weapon.ChangeWeapon(4);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (stats.IsHexDamageUp)
        {
            hexDamageEffect.transform.position = transform.position;
        }

        if (stats.IsInfiniteDashUp)
        {
            infiniteDashEffect.transform.position = transform.position;
        }

        if (stats.IsInfiniteAmmoUp)
        {
            infiniteAmmoEffect.transform.position = transform.position;
        }

        if (stats.IsGodModeUp)
        {
            godModeEffect.transform.position = transform.position;
        }
    }



    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);

        //prevent player moving outside of screen/camera
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenSize.x + playerSize.x, maxScreenSize.x - playerSize.x), Mathf.Clamp(transform.position.y, minScreenSize.y + playerSize.y, maxScreenSize.y - playerSize.y), transform.position.z);

        Vector2 lookDir = mousePosition - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f; //rotaatiosetit: Miten kierretään hahmoa että osoitetaan mousepositioon
        rb2D.rotation = angle;
    }

    private void Shoot()
    {
        if(weapon.CurrentWeapon.Ammo > 0 || stats.IsInfiniteAmmoUp || stats.IsGodModeUp)
        {
            weapon.PlayWeaponSound(weapon.CurrentWeapon.WeaponName);
            //AudioManager.instance.PlaySound(1);

            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

            if (stats.IsHexDamageUp)
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            }

            if (!stats.IsInfiniteAmmoUp && !stats.IsGodModeUp)
            {
                weapon.LoseAmmo();
                uiManager.UpdateWeaponText(weapon.CurrentWeapon);
                uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
            }

            if(weapon.CurrentWeapon.WeaponName == "Shotgun")
            {
                GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.AddForce(firePoint2.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation);
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.AddForce(firePoint3.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                if (stats.IsHexDamageUp)
                {
                    bullet2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                    bullet3.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                }
            }

        }
        /*
        else if (weapon.CurrentWeapon.Ammo <= 0 && !stats.IsInfiniteAmmoUp && !stats.IsGodModeUp)
        {
            weapon.PlayOutOfAmmoSound();
        }
        */

    }


    public void ChangePlayerColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void TurnOnHexDamageEffect()
    {
            hexDamageEffect = Instantiate(hexDamagePreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffHexDamageEffect()
    {
        Destroy(hexDamageEffect);
    }


    public void TurnOnInfiniteDashEffect()
    {
        infiniteDashEffect = Instantiate(infiniteDashPreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffInfiniteDashEffect()
    {
        Destroy(infiniteDashEffect);
    }

    public void TurnOnInfiniteAmmoEffect()
    {
        infiniteAmmoEffect = Instantiate(infiniteAmmoPreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffInfiniteAmmoEffect()
    {
        Destroy(infiniteAmmoEffect);
    }

    public void TurnOnGodModeEffect()
    {
        godModeEffect = Instantiate(godModePreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffGodModeEffect()
    {
        Destroy(godModeEffect);
    }

    public void SetPreviousColor()
    {
        PlayerStats player = GameManager.instance.PlayerStats;

        GetComponent<SpriteRenderer>().color = originalColor;
    }

    public void TurnOnShieldGraphic()
    {
            shield.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void TurnOffShieldGraphic()
    {
        shield.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    private IEnumerator Dash()
    {
        PlayerStats stats = GameManager.instance.PlayerStats;

        stats.IsDashing = true;
        while (stats.IsDashing)
        {
            SetPreviousColor();
            ChangePlayerColor(dashColor);
            
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
            yield return new WaitForSeconds(dash.DashingTime);
            this.gameObject.layer = 10; //Player Layer
            //GetComponent<BoxCollider2D>().enabled = true;
            rb2D.velocity = Vector2.zero;
            stats.IsDashing = false;
            stats.IsInvulnerable = true;
            yield return new WaitForSeconds(stats.InvulnerabilityTime);
            stats.IsInvulnerable = false;
            ChangePlayerColor(originalColor);
        }

    }

    public IEnumerator InvulnerabilityTimer()
    {
        PlayerStats stats = GameManager.instance.PlayerStats;
        stats.IsInvulnerable = true;
        yield return new WaitForSeconds(stats.InvulnerabilityTime);
        stats.IsInvulnerable = false;
    }
    

}
