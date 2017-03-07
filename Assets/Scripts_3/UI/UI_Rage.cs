using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Rage : MonoBehaviour {
    
    public Image foreground;

    public static UI_Rage ui_rage;

    private void Start()
    {
        if(ui_rage == null)
        {
            ui_rage = this;
        }

        Update_Rage(0, 100);
    }

    public void Update_Rage(float _current_rage, float _max_rage)
    {
        if(foreground != null)
        {
            foreground.fillAmount = (_current_rage / _max_rage);
        }
    }
}
