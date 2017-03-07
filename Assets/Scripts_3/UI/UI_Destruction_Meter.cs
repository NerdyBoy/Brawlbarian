using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Destruction_Meter : MonoBehaviour {

    public static UI_Destruction_Meter ui_destruction_meter;

    public Image foreground;
    public Image background;

    private void Start()
    {
        if(ui_destruction_meter == null)
        {
            ui_destruction_meter = this;
        }
    }

    public void Update_Destruction_Meter(int _initial_number, int _current_number)
    {
        print(_initial_number + " " + _current_number);
        float amount = (float)_current_number / (float)_initial_number;
        print(amount);
        foreground.fillAmount = amount;
    }


}
