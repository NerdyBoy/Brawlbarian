using UnityEngine;
using System.Collections;

public class door_animation : MonoBehaviour {

    public GameObject door_object;
    public float rotate_speed = 15.0f;
    public float rotation_amount = 120.0f;
    public float rage_limit = 150.0f;
    Vector3 final_rotation;
    private bool open = false;

	// Use this for initialization
	void Start () {
        Vector3 current_rotation = door_object.transform.rotation.eulerAngles;
        final_rotation = current_rotation;
        final_rotation = new Vector3(0, 120, 0);
	}

    void Update()
    {
        if(open == false && Rage.rage != null && Rage.rage.total_rage > rage_limit)
        {
            StartCoroutine(Open_Door());
            open = true;
        }
    }

    IEnumerator Open_Door()
    {
        if(Application.loadedLevel == 5)
        {
            door_object.SetActive(false);
            door_object.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        while(door_object.transform.rotation.eulerAngles.y != 120)
        {
            Vector3 current_rotation = door_object.transform.rotation.eulerAngles;
            door_object.transform.Rotate(door_object.transform.up, rotate_speed * Time.deltaTime);
            if(door_object.transform.rotation.eulerAngles.y - 120 < 10)
            {
                door_object.transform.rotation.SetEulerAngles(final_rotation);
                StopAllCoroutines();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
