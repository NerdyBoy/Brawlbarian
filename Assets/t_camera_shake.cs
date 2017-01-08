using UnityEngine;
using System.Collections;

public class t_camera_shake : MonoBehaviour {

    private Vector3 initial_position;
    private Quaternion initial_rotation;
    public float shake_intesity;
    public float shake_decay;
    public float shake_duration;
    public float shake_gap;
    private bool is_shaking = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(Time.fixedTime % shake_gap == 0 && Time.fixedTime != 0 && false == is_shaking) {
            is_shaking = true;
            StartCoroutine(Screen_Shake());
        }
	}

    IEnumerator Screen_Shake() {
        //SendMessageUpwards("Set_Look_Enabled", false);
        float start_time = Time.fixedTime;
        float end_time = start_time + shake_duration;
        initial_position = this.transform.localPosition;
        initial_rotation = this.transform.rotation;

        while(Time.fixedTime < end_time) {
            transform.localPosition = initial_position + Random.insideUnitSphere * shake_intesity;
            /*transform.rotation = new Quaternion(
                            this.transform.rotation.x + Random.Range(-shake_intesity, shake_intesity) * .2f,
                            this.transform.rotation.y + Random.Range(-shake_intesity, shake_intesity) * .2f,
                            this.transform.rotation.z + Random.Range(-shake_intesity, shake_intesity) * .2f,
                            this.transform.rotation.w + Random.Range(-shake_intesity, shake_intesity) * .2f);*/
            shake_intesity -= shake_decay;
            yield return new WaitForSeconds(0);
        }
        //this.transform.localPosition= initial_position;
        //this.transform.rotation = initial_rotation;
        is_shaking = false;
        //SendMessageUpwards("Set_Look_Enabled", true);
    }
    
}
