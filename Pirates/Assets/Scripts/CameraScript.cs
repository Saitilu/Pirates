using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform attachedPlayer;
    Camera thisCamera;
    [SerializeField] float blendAmount;

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
        transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
    }
}
