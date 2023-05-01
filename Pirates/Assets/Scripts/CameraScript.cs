using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform attachedPlayer;
    Camera thisCamera;

    [Header("Adjust")]
    [SerializeField] float blendAmount;
    [SerializeField] float camToBoxRatio;

    // Use this for initialization
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 player = attachedPlayer.transform.position;
        Vector3 newCamPos = player * blendAmount + transform.position * (1.0f - blendAmount);

        //edge constraints
        Vector2 boxSize;
        boxSize.x = thisCamera.orthographicSize * thisCamera.aspect * camToBoxRatio;
        boxSize.y = thisCamera.orthographicSize * camToBoxRatio;

        Vector3 playerRelativePos = player - transform.position;

        if (playerRelativePos.x > boxSize.x)
            newCamPos.x = player.x - boxSize.x;
        else if (playerRelativePos.x < -boxSize.x)
            newCamPos.x = player.x + boxSize.x;

        if (playerRelativePos.y > boxSize.y)
            newCamPos.y = player.y - boxSize.y;
        else if (playerRelativePos.y < -boxSize.y)
            newCamPos.y = player.y + boxSize.y;

        transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);

        //float camX, camY;
        //camX = newCamPos.x;
        //camY = newCamPos.y;

        //float screenX0, screenX1, screenY0, screenY1;
        //float box_x0, box_x1, box_y0, box_y1;
        //box_x0 = player.x - boxSize.x;
        //box_x1 = player.x + boxSize.x;
        //box_y0 = player.y - boxSize.y;
        //box_y1 = player.y + boxSize.y;

        //Vector3 bottomLeft = thisCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //Vector3 topRight = thisCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        //screenX0 = bottomLeft.x;
        //screenX1 = topRight.x;

        //if (box_x0 < screenX0)
        //    camX = player.x + 0.5f * (screenX1 - screenX0) - boxSize.x;
        //else if (box_x1 > screenX1)
        //    camX = player.x - 0.5f * (screenX1 - screenX0) + boxSize.x;

        //screenY0 = bottomLeft.y;
        //screenY1 = topRight.y;

        //if (box_y0 < screenY0)
        //    camY = player.y + 0.5f * (screenY1 - screenY0) - boxSize.y;
        //else if (box_y1 > screenY1)
        //    camY = player.y - 0.5f * (screenY1 - screenY0) + boxSize.y;

        //transform.position = new Vector3(camX, camY, transform.position.z);
    }
}
