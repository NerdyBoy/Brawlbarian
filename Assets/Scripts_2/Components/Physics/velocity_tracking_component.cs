using UnityEngine;
using System.Collections;

public class velocity_tracking_component : MonoBehaviour {

    [SerializeField]
    private Transform monitor_point;

    private Vector3 last_position;
    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        Monitor_Position();
	}
	
	// Update is called once per frame
	void Update () {
        Monitor_Position();
	}

    void Monitor_Position()
    {
        if(null != monitor_point)
        {
            velocity = monitor_point.transform.position - last_position;
            last_position = monitor_point.transform.position;
        }
        else
        {
            velocity = this.transform.position - last_position;
            last_position = this.transform.position;
        }
    }

    public Vector3 Get_Velocity()
    {
        return velocity;
    }
}
