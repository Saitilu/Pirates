using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;

    [Header("Adjust")]
    [SerializeField] float renderDistance;
    [SerializeField] int numberOfStars;
    [SerializeField] float minStars;
    [SerializeField] float maxStars;

    Camera camera;
    float biggestCameraEdge;

    // Start is called before the first frame update
    void Start()
    {
        numberOfStars = (int)(numberOfStars * Mathf.Pow(renderDistance, 2));

        camera = UnityEngine.Camera.main;
        biggestCameraEdge = camera.orthographicSize * (camera.aspect > 1 ? camera.aspect : 1); // gets the length of whichever edge of the camera is longest

        float xPos, yPos, scale;
        for (int i = 1; i <= numberOfStars; i++)
        {
            xPos = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
            yPos = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
            scale = Random.Range(minStars, maxStars);
            GameObject star = Instantiate(starPrefab, new Vector2(xPos, yPos), Quaternion.identity, transform);
            star.GetComponent<Transform>().localScale = new Vector3(scale, scale, 1);
        }
    }

    public void ScrollSpawn(GameObject star)
    {
        Vector3 starRelativePos = star.transform.position - camera.transform.position;
        if (starRelativePos.x > biggestCameraEdge * renderDistance)
        {
            starRelativePos.x -= biggestCameraEdge * renderDistance * 2;
            starRelativePos.y = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
        }
        else if (starRelativePos.x < -biggestCameraEdge * renderDistance)
        {
            starRelativePos.x += biggestCameraEdge * renderDistance * 2;
            starRelativePos.y = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
        }
        if (starRelativePos.y > biggestCameraEdge * renderDistance)
        {
            starRelativePos.y -= biggestCameraEdge * renderDistance * 2;
            starRelativePos.x = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
        }
        else if (starRelativePos.y < -biggestCameraEdge * renderDistance)
        {
            starRelativePos.y += biggestCameraEdge * renderDistance * 2;
            starRelativePos.x = Random.Range(-biggestCameraEdge * renderDistance, biggestCameraEdge * renderDistance);
        }
        star.transform.position = starRelativePos + camera.transform.position;
    }
}
