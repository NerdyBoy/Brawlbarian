using UnityEngine;
using System.Collections;

public static class cursor_display : object {

    public static void Disable_Cursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void Enable_Cursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
