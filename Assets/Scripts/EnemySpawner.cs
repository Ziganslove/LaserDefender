using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float width, height, vertCenter;
    public float speed;
    public float spawnDelay = 0.5f;

    private float minX, maxX;
    private bool movingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        SpawnUntilFull();
        GetExtremePointsOfX();;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(0, vertCenter), new Vector3(width, height));
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        if (transform.position.x <= minX)
        {
            movingRight = true;
        }
        else if(transform.position.x >= maxX)
        {
            movingRight = false;
        }

        if (AllMembersDead())
        {
            SpawnUntilFull();
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity);
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy =  Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }

    void GetExtremePointsOfX()
    {
        float distance = transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftmost.x + (0.5f * width);
        maxX = rightmost.x - (0.5f * width);
    }
}
