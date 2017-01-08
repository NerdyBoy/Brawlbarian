using UnityEngine;
using System.Collections;

public class t_rotation : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rotate_Object(rotation_struct _rotation_struct) {
        if (true == _rotation_struct.rotate_horizontal) {
            //camera_object.transform.Rotate(new Vector3(-_y_delta, 0, 0));
            _rotation_struct.rotation_object.transform.Rotate(new Vector3(_rotation_struct.vertical_delta, 0, 0));
        }
        if(true == _rotation_struct.rotate_vertical) {
            _rotation_struct.rotation_object.transform.Rotate(new Vector3(0.0f, _rotation_struct.horizontal_delta, 0.0f));
        }
    }
}
