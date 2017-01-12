using UnityEngine;
using System.Collections;
using System;

public class fps_camera_clamp_component : MonoBehaviour {

    private Camera player_camera;

	// Use this for initialization
	void Start () {
        player_camera = this.transform.root.GetComponentInChildren<Camera>();
	}

    public void Clamp_Camera()
    {
        Vector3 camera_angles = player_camera.transform.rotation.eulerAngles;
        if(camera_angles.x > 37.0f && camera_angles.x < 300.0f)
        {
            if(camera_angles.x < 141.0f)
            {
                camera_angles.x = 37.0f;
            }
            else
            {
                camera_angles.x = 300.0f;
            }

            player_camera.transform.rotation = Quaternion.Euler(camera_angles);
        }
    }
}
