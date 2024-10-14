using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    Transform _canvas;
    public Transform Canvas
    {
        get
        {
            if (_canvas == null)
                _canvas = GameObject.Find("Canvas").transform;

            if (_canvas == null)
                Debug.LogError("ĵ������ �����ϴ�.");

            return _canvas;
        }
    }

    public GameObject CreateUI(string path, Transform parent = null)
    {
        if(parent == null)
            return Managers.Resource.Instantiate($"UI/{path}", Canvas);
        else
            return Managers.Resource.Instantiate($"UI/{path}", parent);
    }

    public void Clear()
    {
        _canvas = null;
    }
}
