using System;
using UnityEngine;
using MonoMod;
using Mono.Cecil;

public class patch_camera_control : camera_control
{
    private Vector3 cam_offset;
    private Vector3 old_cam_pos;
    private const float SPEED = 0.1f;

    [MonoModReplace]
    private void Start()
    {
        this.cam_offset = new Vector3(0, 0, 0);
        this.old_cam_pos = new Vector3(0, 0, 0);
    }
    private extern void orig_Update();
    private void Update()
    {
        //Debug.Log("thing 1");
        this.gameObject.transform.position = this.old_cam_pos;
        orig_Update();
        //Debug.Log("thing 2");
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                this.cam_offset.z -= SPEED;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.cam_offset.z += SPEED;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                this.cam_offset.x -= SPEED;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                this.cam_offset.x += SPEED;
            }
            if (Input.GetKey(KeyCode.Home))
            {
                this.cam_offset = Vector3.zero;
            }
        }
        this.old_cam_pos = this.gameObject.transform.position;
        this.gameObject.transform.position += this.cam_offset;
        //Debug.Log("thing 3");
    }
}