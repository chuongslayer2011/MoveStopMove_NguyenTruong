using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;

    public Transform target;
    public Vector3 offset;
    public float speed = 5;
    public Camera Camera;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        Camera = Camera.main;
    }
    void Start()
    {  
        target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
    }
    public void SetOffset(float x, float y, float z)
    {
        Vector3 offset = new Vector3(x, y, z);
        this.offset = offset;
    }
    public void ZoomOut()
    {
        this.offset.y += 0.5f;
    }
    public void SetCameraOnMainManu()
    {
        transform.rotation = Quaternion.Euler(40, 0, 0);
        SetOffset(0, 3, -2);
    }
    public void SetCameraOnPlay()
    {
        transform.rotation = Quaternion.Euler(60, 0, 0);
        SetOffset(0, 8, -6);
    }
    public void SetCameraOnSkinShop()
    {
        SetOffset(0, 0f, -5);
        transform.rotation = Quaternion.Euler(15, 0, 0);
    }
}
