using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 offset;
    public Transform PlayerTransform;
    // Update is called once per frame
    private void Start()
    {
        offset.x = 0;
        offset.y = 5;
        offset.z = -10;
    }

    void Update()
    {
        transform.position = PlayerTransform.position + offset;
    }
}
