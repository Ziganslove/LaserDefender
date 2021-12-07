using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour
{
    public float speed = 15f;
    public float padding;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate = 0.2f;
    public float health = 500f;
    public AudioClip laserSound;

    private float minX, maxX;
    // Start is called before the first frame update
    void Start()
    {
        float distance = transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftmost.x + padding;
        maxX = rightmost.x - padding;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
        AudioSource.PlayClipAtPoint(laserSound, transform.position, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
                LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                levelManager.LoadLevel("Win");
            }
        }
    }
}
