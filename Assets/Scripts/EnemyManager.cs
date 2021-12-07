using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public float health = 300f;
    public float shotsPerSekond = 0.5f;
    public int scoreValue;
    public AudioClip laserSound;
    public AudioClip dieSound;

    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void Update()
    {
        float probability = Time.deltaTime * shotsPerSekond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed);
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
                AudioSource.PlayClipAtPoint(dieSound, transform.position, 1f);
                Destroy(gameObject);
                scoreKeeper.Score(scoreValue);
            }
        }
    }
}
