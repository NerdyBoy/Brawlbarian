using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_rage_controller : MonoBehaviour {

    public static ui_rage_controller rage_controller;

    [SerializeField]
    private Image background_image;
    [SerializeField]
    private Image foreground_image;

	// Use this for initialization
	void Start () {
	    if(null == rage_controller)
        {
            rage_controller = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Set_Max(float _value)
    {
        background_image.fillAmount = _value;
    }

    public void Set_Current(float _value)
    {
        print(_value);
        foreground_image.fillAmount = _value;
    }
}
