using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ui_time_remaining_global_component : MonoBehaviour {

    public static ui_time_remaining_global_component ui_time_component;

    [SerializeField]
    Text time_text;

    private void Start()
    {
        if(null != ui_time_component)
        {
            Destroy(ui_time_component.gameObject);
        }
        ui_time_component = this;
    }

    public void Update_Time(string _time)
    {
        if(null != time_text)
        {
            time_text.text = _time;
        }
    }
}
