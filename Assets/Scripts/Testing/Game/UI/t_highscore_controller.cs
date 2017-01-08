using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_highscore_controller : MonoBehaviour {

    [SerializeField]
    private GameObject score_scroller = null;

    [SerializeField]
    private Text[] scores;

    [SerializeField]
    private int aim_index = 0;

    [SerializeField]
    private float vertical_distance = 0.0f;

    [SerializeField]
    private float vertical_offset = 100.0f;

    [SerializeField]
    private float scroll_speed = 1f;

    [SerializeField]
    private Vector3 start_position;

    [SerializeField]
    private Vector3 target_position;

    [SerializeField]
    private Color text_color;

	// Use this for initialization
	void Start () {
	    if(null != score_scroller) {
            start_position = score_scroller.transform.position;
            target_position = start_position;
            target_position.y -= ((10 - (aim_index - 1)) * vertical_distance) + vertical_offset;
            scores[aim_index - 1].color = text_color;
            print(text_color);
            StartCoroutine(Move_To_Position());
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Move_To_Position() {
        while(score_scroller.transform.position != target_position) {
            score_scroller.transform.position = Vector3.Lerp(start_position, target_position, scroll_speed * Time.time);
            yield return new WaitForSeconds(0);
        }
    }
}
