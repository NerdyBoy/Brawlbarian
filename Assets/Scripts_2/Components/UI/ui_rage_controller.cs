using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_rage_controller : MonoBehaviour {

    public static ui_rage_controller rage_controller;

    [SerializeField]
    private Image background_image;
    [SerializeField]
    private Image foreground_image;

    public rage_component comp;

	// Use this for initialization
	void Start () {
	    if(null == rage_controller)
        {
            rage_controller = this;
        }
        Get_Comp();
	}

    //remove comp shit, better way of doing it than this
    void Get_Comp()
    {
        comp = GameObject.FindObjectOfType<rage_component>();
    }
	
	// Update is called once per frame
	void Update () {
        if(null == comp)
        {
            Get_Comp();
        }
        if (null != comp)
        {
            background_image.fillAmount = comp.max_rage / 100.0f;
        }
        if (null != comp)
        {
            foreground_image.fillAmount = comp.current_rage / 100.0f;
        }
	}

    public void Set_Max(float _value)
    {
        background_image.fillAmount = _value;
    }

    public void Set_Current(float _value)
    {
        foreground_image.fillAmount = _value;
    }
}
