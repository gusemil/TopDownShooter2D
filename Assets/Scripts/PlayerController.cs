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
    private Color invulnerabilityColor = new Color(0, 1, 0, 1);
    private UIManager uiManager;
    private WeaponSystem weaponSystem;
    private Dash dash;
    private Pause pause;
    private PlayerStats playerStats;
    private Vector2 movement;
    private Vector2 mousePosition;
    private Color originalColor;


    private Vector3 minScreenSize;
    private Vector3 maxScreenSize;
    private Vector3 playerSize;

    public Color OriginalColor { get { return originalColor; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
        weaponSystem = GameManager.instance.WeaponSystem;
        dash = GameManager.instance.Dash;
        pause = GameManager.instance.Pause;
        playerStats = GameManager.instance.PlayerStats;

        playerSize = transform.localScale;
        shield.GetComponent<SpriteRenderer>().enabled = false;

        minScreenSize = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        maxScreenSize = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        uiManager = FindObjectOfType<UIManager>();
        UpdateWeaponUI();
        uiManager.UpdateBombText(weaponSystem);
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + rb.velocity.x;
        movement.y = Input.GetAxisRaw("Vertical") + rb.velocity.y;

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetButton("Fire1") && !pause.IsPause && !playerStats.IsRespawning) //mouse1
        {
            if (weaponSystem.ShotTimer >= weaponSystem.CurrentWeapon.FireCoolDown)
            {
                Shoot();
                weaponSystem.ShotTimer = 0;
            }
        }

        if (Input.GetButtonUp("Fire2") && !pause.IsPause && !playerStats.IsRespawning) //mouse2
        {
            if (weaponSystem.BombCount > 0)
            {
                GameObject effect = Instantiate(bombEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.1f);
            }

            weaponSystem.Bomb();
            uiManager.UpdateBombText(weaponSystem);
        }

        if (Input.GetKeyDown(KeyCode.Space) && (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0) && !pause.IsPause && !playerStats.IsRespawning)
        {
            if (dash.Dashes > 0 || playerStats.IsInfiniteDashUp || playerStats.IsGodModeUp)
            {
                AudioManager.instance.PlaySound(20);

                if (!playerStats.IsInfiniteDashUp && !playerStats.IsGodModeUp)
                {
                    dash.ConsumeDash();
                }

                StartCoroutine(Dash());
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) //pistol
        {
            weaponSystem.ChangeWeapon(0);
            UpdateWeaponUI();
        }

        if (Input.GetKeyUp(KeyCode.Alpha2)) //machinegun
        {
            weaponSystem.ChangeWeapon(1);
            UpdateWeaponUI();
        }

        if (Input.GetKeyUp(KeyCode.Alpha3)) //shotgun
        {
            weaponSystem.ChangeWeapon(2);
            UpdateWeaponUI();
        }


        if (Input.GetKeyUp(KeyCode.Alpha4) && LevelManager.instance.CurrentLevel >= 2) //rocketlauncher
        {
            weaponSystem.ChangeWeapon(3);
            UpdateWeaponUI();
        }

        if (Input.GetKeyUp(KeyCode.Alpha5) && LevelManager.instance.CurrentLevel >= 3) //flamethrower
        {
            weaponSystem.ChangeWeapon(4);
            UpdateWeaponUI();
        }

        if (playerStats.IsHexDamageUp)
        {
            hexDamageEffect.transform.position = transform.position;
        }

        if (playerStats.IsInfiniteDashUp)
        {
            infiniteDashEffect.transform.position = transform.position;
        }

        if (playerStats.IsInfiniteAmmoUp)
        {
            infiniteAmmoEffect.transform.position = transform.position;
        }

        if (playerStats.IsGodModeUp)
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

    private IEnumerator Dash()
    {
        playerStats.IsDashing = true;
        ChangePlayerColor(invulnerabilityColor);
        playerStats.IsInvulnerable = true;

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
        playerStats.IsDashing = false;

        yield return new WaitForSeconds(0.5f);
        playerStats.IsInvulnerable = false;
        ChangePlayerColor(originalColor);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && (!playerStats.IsInvulnerable && !playerStats.IsGodModeUp))
        {
            DamagePlayerOnCollision(other);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && (!playerStats.IsInvulnerable && !playerStats.IsGodModeUp))
        {
            DamagePlayerOnCollision(other);
        }
    }

    private void Shoot()
    {
        if (weaponSystem.CurrentWeapon.Ammo > 0 || playerStats.IsInfiniteAmmoUp || playerStats.IsGodModeUp)
        {
            weaponSystem.PlayWeaponSound(weaponSystem.CurrentWeapon.WeaponName);

            GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * weaponSystem.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

            if (playerStats.IsHexDamageUp || playerStats.IsGodModeUp)
            {
                bullet.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            }

            if (!playerStats.IsInfiniteAmmoUp && !playerStats.IsGodModeUp)
            {
                weaponSystem.LoseAmmo();
                UpdateWeaponUI();
            }

            if (weaponSystem.CurrentWeapon.WeaponName == "Shotgun")
            {
                GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation);
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.AddForce(firePoint2.up * weaponSystem.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation);
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.AddForce(firePoint3.up * weaponSystem.CurrentWeapon.BulletForce, ForceMode2D.Impulse);

                if (playerStats.IsHexDamageUp || playerStats.IsGodModeUp)
                {
                    bullet2.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                    bullet3.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
                }
            }

        }
    }

    private IEnumerator InvulnerabilityTimer(float invulnerabilityTime)
    {
        playerStats.IsInvulnerable = true;
        ChangePlayerColor(invulnerabilityColor);
        yield return new WaitForSeconds(invulnerabilityTime);
        ChangePlayerColor(originalColor);
        playerStats.IsInvulnerable = false;
    }

    private void DamagePlayerOnCollision(Collision2D other)
    {
        if (playerStats.IsShieldUp)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            enemy.TakeDamage(enemy.hp, true);
            TurnOffShield();
            StartCoroutine(InvulnerabilityTimer(1f));
        }
        else
        {
            TurnOffHexDamage();
            TurnOffInfiniteDashEffect();
            TurnOffInfiniteAmmoEffect();
            playerStats.TakeDamage(1);
            StartCoroutine(InvulnerabilityTimer(0.5f));
        }
    }
    
    private void UpdateWeaponUI()
    {
        uiManager.UpdateWeaponText(weaponSystem.CurrentWeapon);
        uiManager.UpdateWeaponImage(weaponSystem.CurrentWeapon);
    }

    public void ChangePlayerColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void TurnOnHexDamage()
    {
        AudioManager.instance.PlaySound(9);
        AudioManager.instance.PlaySound(29, 1.5f);
        playerStats.IsHexDamageUp = true;
        StartCoroutine(uiManager.ShowPowerUpText("HEX DAMAGE"));
        hexDamageEffect = Instantiate(hexDamagePreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffHexDamage()
    {
        if (!playerStats.IsGodModeUp)
        {
            playerStats.DamageMultiplier = 1;
        }
        if (playerStats.IsHexDamageUp)
        {
            AudioManager.instance.PlaySound(15);
        }
        playerStats.IsHexDamageUp = false;
        Destroy(hexDamageEffect);
    }


    public void TurnOnInfiniteDashEffect()
    {
        AudioManager.instance.PlaySound(10);
        AudioManager.instance.PlaySound(30, 1.5f);
        playerStats.IsInfiniteDashUp = true;
        StartCoroutine(uiManager.ShowPowerUpText("INFINITE DASH"));
        infiniteDashEffect = Instantiate(infiniteDashPreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffInfiniteDashEffect()
    {
        if (playerStats.IsInfiniteDashUp)
        {
            AudioManager.instance.PlaySound(16);
        }
        playerStats.IsInfiniteDashUp = false;
        Destroy(infiniteDashEffect);
    }

    public void TurnOnInfiniteAmmoEffect()
    {
        AudioManager.instance.PlaySound(13);
        AudioManager.instance.PlaySound(32, 1.5f);
        playerStats.IsInfiniteAmmoUp = true;
        StartCoroutine(uiManager.ShowPowerUpText("INFINITE AMMO"));
        infiniteAmmoEffect = Instantiate(infiniteAmmoPreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffInfiniteAmmoEffect()
    {
        if (playerStats.IsInfiniteAmmoUp)
        {
            AudioManager.instance.PlaySound(18);
        }
        playerStats.IsInfiniteAmmoUp = false;
        Destroy(infiniteAmmoEffect);
    }

    public void TurnOnGodModeEffect()
    {
        AudioManager.instance.PlaySound(14);
        AudioManager.instance.PlaySound(33, 1.5f);
        playerStats.IsGodModeUp = true;
        StartCoroutine(uiManager.ShowPowerUpText("GODLIKE"));
        godModeEffect = Instantiate(godModePreFab, transform.position, Quaternion.identity);
    }

    public void TurnOffGodModeEffect()
    {
        if (!playerStats.IsHexDamageUp)
        {
            playerStats.DamageMultiplier = 1;
        }
        MoveSpeed = 7.5f;
        if (playerStats.IsGodModeUp)
        {
            AudioManager.instance.PlaySound(19);
        }
        playerStats.IsGodModeUp = false;
        Destroy(godModeEffect);
    }

    public void TurnOnShield()
    {
        shield.GetComponent<SpriteRenderer>().enabled = true;
        AudioManager.instance.PlaySound(12);
        AudioManager.instance.PlaySound(31, 1.5f);
        StartCoroutine(uiManager.ShowPowerUpText("SHIELD"));
        playerStats.IsShieldUp = true;
    }

    private void TurnOffShield()
    {
        shield.GetComponent<SpriteRenderer>().enabled = false;
        AudioManager.instance.PlaySound(17);
        playerStats.IsShieldUp = false;
    }
}
