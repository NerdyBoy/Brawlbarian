using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(t_destroy_after_time))]
public class t_object_score_display : MonoBehaviour {

    [SerializeField]
    private float lifespan = 1;
    [SerializeField]
    private float rising_speed = 1;

    private TextMesh text_mesh = null;
    private t_destroy_after_time destroy_after_time = null;

    private GameObject player_object = null;

    private bool active = false;

	// Use this for initialization
	void Start () {
        Initialize();
        rising_speed = Random.Range(1, 5);
	}

    public void Initialize() {
        destroy_after_time = GetComponent<t_destroy_after_time>();
        text_mesh = GetComponent<TextMesh>();

        if (null != destroy_after_time) {
            destroy_after_time.Set_Lifespan(lifespan);
        }
        player_object = GameObject.FindGameObjectWithTag("player");

        destroy_after_time.enabled = true;

    }

    public void Setup(string _score, string _combo) {
        if(null == text_mesh) {
            text_mesh = GetComponent<TextMesh>();
        }
        if (null != text_mesh) {
            text_mesh.text = "<color=#ffff00ff>" + _score + "</color> <color=#00ff00ff>" + _combo + "</color>";
            active = true;
        }
    }
	// Update is called once per frame
	void Update () {
        Raise();
        Rotate_To_Face_Player();
	}

    void Raise(){
        Vector3 new_position = this.transform.position;
        new_position.y += rising_speed * Time.deltaTime;
        this.transform.position = new_position;
    }

    void Rotate_To_Face_Player() {
        if(null == player_object) {
            player_object = GameObject.FindGameObjectWithTag("player");
        }
        if(null != player_object) {
            this.transform.LookAt(player_object.transform);
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}
