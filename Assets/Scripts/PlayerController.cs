using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float bulletForce = 20f;

    private float timer = 0;
    private float dashTime = 0.2f; //0.2f
    private float dashSpeed = 6f; //4f
    private float fireCoolDown = 0.1f;
    private float dashInvulnerabilityDelay = 0.5f;

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject bulletPreFab;
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

    void Start()
    {
        //PlayerStats stats = GameManager.status.PlayerStats;
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal") + rb2D.velocity.x;
        movement.y = Input.GetAxisRaw("Vertical") + rb2D.velocity.y;

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1")) //mouse1
        {
            if(timer >= fireCoolDown)
            {
                Shoot();
                timer = 0;
            }
        }

        if (Input.GetButton("Fire2")) //mouse2
        {
            Bomb();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(Dash());
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
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet2 = Instantiate(bulletPreFab, firePoint2.position, firePoint2.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);

        GameObject bullet3 = Instantiate(bulletPreFab, firePoint3.position, firePoint3.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(firePoint3.up * bulletForce, ForceMode2D.Impulse);
    }

    private void Bomb()
    {
            GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
            enemy[0].GetComponent<Enemy>().TakeDamage(10000);
    }

    public void ChangePlayerColor(Color color)
    {
        PlayerStats player = GameManager.status.PlayerStats;

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
        PlayerStats player = GameManager.status.PlayerStats;

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
        PlayerStats player = GameManager.status.PlayerStats;

        player.IsDashing = true;
        while (player.IsDashing)
        {
            SetPreviousColor();
            ChangePlayerColor(dashColor);
            
            
            if (Input.GetAxisRaw("Horizontal") > 0)
                //movement.x +=  0.1f;
                rb2D.velocity = Vector2.right * dashSpeed;
            else if (Input.GetAxisRaw("Horizontal") < 0)
                //movement.x -= 0.1f;
                rb2D.velocity = Vector2.left * dashSpeed;

            if (Input.GetAxisRaw("Vertical") > 0)
                //movement.y += 0.1f;
                rb2D.velocity = Vector2.up * dashSpeed;
            else if (Input.GetAxisRaw("Vertical") < 0)
                //movement.y -= 0.1f;
                rb2D.velocity = Vector2.down * dashSpeed;

            GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(dashTime);
            GetComponent<BoxCollider2D>().enabled = true;
            rb2D.velocity = Vector2.zero;
            player.IsDashing = false;
            player.IsInvulnerable = true;
            yield return new WaitForSeconds(dashInvulnerabilityDelay);
            player.IsInvulnerable = false;
            ChangePlayerColor(previousColor);
        }

    }

}
