using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorOnOff : MonoBehaviour
{
    public GameObject canvas; //제어할 캔버스

    private void OnEnable()
    {
        Cursor.visible = true;
        Destroy(this, 3.0f);
    }
    private void OnDisable()
    {
        Cursor.visible = false;
        Destroy(this, 3.0f);
    }
}
