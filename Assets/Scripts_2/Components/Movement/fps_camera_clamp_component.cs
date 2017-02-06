using UnityEngine;
using System.Collections;
using System;

public class fps_camera_clamp_component : MonoBehaviour {

    private Camera player_camera;
    public float top_limit = 270.0f;
    public float mid_point = 141.0f;
    public float bottom_limit = 90.0f;

	// Use this for initialization
	void Start () {
        player_camera = this.transform.root.GetComponentInChildren<Camera>();
	}

    public void Clamp_Camera()
    {
        Vector3 camera_angles = player_camera.transform.rotation.eulerAngles;
        if(camera_angles.x > bottom_limit && camera_angles.x < top_limit)
        {
            if(camera_angles.x < mid_point)
            {
                camera_angles.x = bottom_limit;
            }
            else
            {
                camera_angles.x = top_limit;
            }

            player_camera.transform.rotation = Quaternion.Euler(camera_angles);
        }
    }
}
