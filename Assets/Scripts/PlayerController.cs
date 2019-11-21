using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float bulletForce = 20f;

    private float timer = 0;
    private float fireCoolDown = 0.1f;

    public Transform firePoint;
    public GameObject bulletPreFab;
    public Rigidbody2D rb2D;
    public Camera _camera; //variable name 'camera' is not available

    Vector2 movement;
    Vector2 mousePosition;

    private Color originalColor;
    public Color OriginalColor { get { return originalColor; } }

    void Start()
    {
        PlayerStats stats = GameManager.status.PlayerStats;
        originalColor = GetComponent<SpriteRenderer>().color;
        Debug.Log(stats.Hp);
        Debug.Log(stats.MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        timer += Time.deltaTime;
        //Debug.Log(timer);

        if (Input.GetButton("Fire1")) //mouse1
        {
            if(timer >= fireCoolDown)
            {
                Shoot();
                timer = 0;
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
        GameObject bullet = Instantiate(bulletPreFab, firePoint.position, firePoint.rotation); //GameObject bullet = jotta päästään käsiksi myöhemmin
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void ChangePlayerColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

}
