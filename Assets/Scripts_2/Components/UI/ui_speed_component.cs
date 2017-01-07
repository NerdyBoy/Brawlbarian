using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_speed_component : MonoBehaviour {

    private Text speed_text;

    // Use this for initialization
    void Start()
    {
        speed_text = GetComponent<Text>();
    }

    void Update_Speed(float _amount)
    {
        speed_text.text = "Speed: x" + _amount.ToString();
    }
}
