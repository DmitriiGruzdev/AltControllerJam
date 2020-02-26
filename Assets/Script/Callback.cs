using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Callback
{

    public delegate void VoidCallback();
    public event VoidCallback onVoidCallback;

    public Callback()
    {

    }

    public Callback(VoidCallback callback)
    {
        onVoidCallback = callback;
    }

    public void StartCallback()
    {
        if (onVoidCallback != null)
        {
            onVoidCallback();
        }
    }

    public void Release()
    {
        onVoidCallback = null;
    }
}
