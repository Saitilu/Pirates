using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public static float biggestCameraEdge;
    float renderDistance = 2;
    float enemyRenderDistance;
    float timeTillNextSpawn = 0;

    [SerializeField] GameObject enemyPrefab;

    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = UnityEngine.Camera.main;
        biggestCameraEdge = camera.orthographicSize * (camera.aspect > 1 ? camera.aspect : 1); // gets the length of whichever edge of the camera is longest
        enemyRenderDistance = (((renderDistance * 3) + 1) / 4);
    }

    void Update()
    {
        timeTillNextSpawn -= Time.deltaTime;
        if (timeTillNextSpawn <= 0)
        {
            timeTillNextSpawn = Random.Range(1f, 5f);

            float spawnAreaPerimeter = enemyRenderDistance * camera.orthographicSize * (1 + camera.aspect) * 2;
            float enemySpawn = Random.Range(0f, spawnAreaPerimeter);

            Quaternion direction = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
            Rigidbody2D newSpawn;

            float halfWidth = enemyRenderDistance * camera.orthographicSize * camera.aspect;
            float halfHeight = enemyRenderDistance * camera.orthographicSize;
            Vector3 camPos = camera.transform.position;
            camPos.z = 0;

            //TopEdge
            enemySpawn -= halfWidth;
            if (enemySpawn < halfWidth)
                newSpawn = Instantiate(enemyPrefab, camPos + new Vector3(enemySpawn, halfHeight, 0), direction, transform).GetComponent<Rigidbody2D>();
            else
            {//Right Edge
                enemySpawn -= halfWidth;
                enemySpawn -= halfHeight;

                if (enemySpawn < enemyRenderDistance * halfHeight)
                    newSpawn = Instantiate(enemyPrefab, camPos + new Vector3(halfWidth, enemySpawn, 0), direction, transform).GetComponent<Rigidbody2D>();
                else
                {//BottomEdge
                    enemySpawn -= halfHeight;
                    enemySpawn -= halfWidth;

                    if (enemySpawn < halfWidth)
                        newSpawn = Instantiate(enemyPrefab, camPos - new Vector3(enemySpawn, halfHeight, 0), direction, transform).GetComponent<Rigidbody2D>();
                    else
                    {//LeftEdge
                        enemySpawn -= halfWidth;
                        enemySpawn -= halfHeight;

                        newSpawn = Instantiate(enemyPrefab, camPos - new Vector3(halfWidth, enemySpawn, 0), direction, transform).GetComponent<Rigidbody2D>();
                    }
                }
            }
            newSpawn.velocity = direction * new Vector3(2, 0, 0);
        }
    }

    public void ScrollDespawn(GameObject enemy)
    {
        Vector3 enemyRelativePos = enemy.transform.position - camera.transform.position;
        if (enemyRelativePos.x > biggestCameraEdge * renderDistance)
            Destroy(enemy);
        if (enemyRelativePos.x < -biggestCameraEdge * renderDistance)
            Destroy(enemy);
        if (enemyRelativePos.y > biggestCameraEdge * renderDistance)
            Destroy(enemy);
        if (enemyRelativePos.y < -biggestCameraEdge * renderDistance)
            Destroy(enemy);
    }
}