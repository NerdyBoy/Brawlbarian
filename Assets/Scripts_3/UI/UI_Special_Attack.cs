using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Special_Attack : MonoBehaviour {

    public static UI_Special_Attack ui_special_attack;

    public Image explosion_image;
    public Image fire_image;

	// Use this for initialization
	void Start () {
	    if(ui_special_attack == null)
        {
            ui_special_attack = this;
        }
	}
	
    public void Set_Image(int _image_number)
    {
        Turn_Off_All();
        switch(_image_number)
        {
            case 0:
                fire_image.enabled = true;
                break;
            case 1:
                explosion_image.enabled = true;
                break;
        }
    }

    void Turn_Off_All()
    {
        explosion_image.enabled = false;
        fire_image.enabled = false;
    }
}
