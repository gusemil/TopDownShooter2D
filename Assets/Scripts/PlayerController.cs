using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 7.5f;

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject shield;
    public GameObject bulletPreFab;
    public GameObject bombEffect;
    public GameObject hexDamagePreFab;
    public GameObject infiniteDashPreFab;
    public GameObject infiniteAmmoPreFab;
    public GameObject godModePreFab;
    public Rigidbody2D rb;
    public Camera _camera; //variable name 'camera' is not available

    private GameObject hexDamageEffect;
    private GameObject infiniteDashEffect;
    private GameObject infiniteAmmoEffect;
    private GameObject godModeEffect;
    private Color originalColor;
    private Color invulnerabilityColor = new Color(0, 1, 0, 1);
    private UIManager uiManager;
    private WeaponSystem weapon;
    private Dash dash;
    private Pause pause;
    private PlayerStats stats;
    private Vector2 movement;
    private Vector2 mousePosition;


    private Vector3 minScreenSize;
    private Vector3 maxScreenSize;
    private Vector3 playerSize;

    public Color OriginalColor { get { return originalColor; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

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
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + rb.velocity.x;
        movement.y = Input.GetAxisRaw("Vertical") + rb.velocity.y;

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1") && !pause.IsPause && !stats.IsRespawning) //mouse1
        {
            if (weapon.ShotTimer >= weapon.CurrentWeapon.FireCoolDown)
            {
                Shoot();
                weapon.ShotTimer = 0;
            }
        }

        if (Input.GetButtonUp("Fire2") && !pause.IsPause && !stats.IsRespawning) //mouse2
        {
            if (weapon.BombCount > 0)
            {
                GameObject effect = Instantiate(bombEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.1f);
            }

            weapon.Bomb();
            uiManager.UpdateBombText(weapon);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0) && !pause.IsPause && !stats.IsRespawning)
        {
            if (dash.Dashes > 0 || stats.IsInfiniteDashUp || stats.IsGodModeUp)
            {
                AudioManager.instance.PlaySound(20);

                if (!stats.IsInfiniteDashUp && !stats.IsGodModeUp)
                {
                    dash.ConsumeDash();
                }

                StartCoroutine(Dash());
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) //pistol
        {
            weapon.ChangeWeapon(0);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) //machinegun
        {
            weapon.ChangeWeapon(1);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) //shotgun
        {
            weapon.ChangeWeapon(2);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }


        if (Input.GetKeyUp(KeyCode.Alpha4) && LevelManager.instance.CurrentLevel >= 2) //rocketlauncher
        {
            weapon.ChangeWeapon(3);
            uiManager.UpdateWeaponText(weapon.CurrentWeapon);
            uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5) && LevelManager.instance.CurrentLevel >= 3) //flamethrower
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
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //prevent player moving outside of the screen
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenSize.x + playerSize.x, maxScreenSize.x - playerSize.x), Mathf.Clamp(transform.position.y, minScreenSize.y + playerSize.y, maxScreenSize.y - playerSize.y), transform.position.z);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; //-90 degrees to match the player sprite
        rb.rotation = angle;
    }

    private void Shoot()
    {
        if (weapon.CurrentWeapon.Ammo > 0 || stats.IsInfiniteAmmoUp || stats.IsGodModeUp)
        {
            weapon.PlayWeaponSound(weapon.CurrentWeapon.WeaponName);

            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

            if (stats.IsHexDamageUp || stats.IsGodModeUp)
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            }

            if (!stats.IsInfiniteAmmoUp && !stats.IsGodModeUp)
            {
                weapon.LoseAmmo();
                uiManager.UpdateWeaponText(weapon.CurrentWeapon);
                uiManager.UpdateWeaponImage(weapon.CurrentWeapon);
            }

            if (weapon.CurrentWeapon.WeaponName == "Shotgun")
            {
                GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.AddForce(firePoint2.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation);
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.AddForce(firePoint3.up * weapon.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                if (stats.IsHexDamageUp || stats.IsGodModeUp)
                {
                    bullet2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                    bullet3.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                }
            }

        }
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

    public void TurnOnShieldGraphic()
    {
        shield.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void TurnOffShieldGraphic()
    {
        shield.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void SetOriginalColor()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }

    public IEnumerator InvulnerabilityTimer(float invulnerabilityTime)
    {
        stats.IsInvulnerable = true;
        ChangePlayerColor(invulnerabilityColor);
        yield return new WaitForSeconds(invulnerabilityTime);
        ChangePlayerColor(originalColor);
        stats.IsInvulnerable = false;
    }

    private IEnumerator Dash()
    {
        stats.IsDashing = true;
        ChangePlayerColor(invulnerabilityColor);
        stats.IsInvulnerable = true;

        if (Input.GetAxisRaw("Horizontal") > 0)
            rb.velocity = Vector2.right * dash.DashSpeed;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            rb.velocity = Vector2.left * dash.DashSpeed;

        if (Input.GetAxisRaw("Vertical") > 0)
            rb.velocity = Vector2.up * dash.DashSpeed;
        else if (Input.GetAxisRaw("Vertical") < 0)
            rb.velocity = Vector2.down * dash.DashSpeed;

        this.gameObject.layer = 11; //Dash Layer
        yield return new WaitForSeconds(dash.DashingTime);
        this.gameObject.layer = 10; //Player Layer
        rb.velocity = Vector2.zero;
        stats.IsDashing = false;

        yield return new WaitForSeconds(0.5f);
        stats.IsInvulnerable = false;
        ChangePlayerColor(originalColor);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && (!stats.IsInvulnerable && !stats.IsGodModeUp))
        {
            DamagePlayerOnCollision(other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && (!stats.IsInvulnerable && !stats.IsGodModeUp))
        {
            DamagePlayerOnCollision(other);
        }
    }


    public void DamagePlayerOnCollision(Collision2D other)
    {
        if (stats.IsShieldUp)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(enemy.hp, true);
            TurnOffShieldGraphic();
            AudioManager.instance.PlaySound(17); //shield break sound
            stats.IsShieldUp = false;
            StartCoroutine(InvulnerabilityTimer(1f));
        }
        else
        {
            TurnOffHexDamageEffect();
            TurnOffInfiniteDashEffect();
            TurnOffInfiniteAmmoEffect();
            stats.TakeDamage(1);
            StartCoroutine(InvulnerabilityTimer(0.5f));
        }
    }




}
