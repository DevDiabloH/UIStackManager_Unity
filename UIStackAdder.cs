using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackAdder : MonoBehaviour
{
    void Start()
    {
        string key = this.gameObject.name.Split(' ')[0];

        if(null != UIStackManager.instance)
        {
            UIStackManager.instance.AddDictionary(key, this.gameObject);
        }
    }
}
