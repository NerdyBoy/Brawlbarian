using UnityEngine;
using System.Collections;

public class start_screen : MonoBehaviour {

    private void OnLevelWasLoaded(int level)
    {
        cursor_display.Enable_Cursor();
    }
}
