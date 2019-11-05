using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStackManager : MonoBehaviour
{
    public static UIStackManager instance = null;
    public string debugPageCode = string.Empty;
    public string PageCode { get; private set; }

    private readonly string defaultPageCode = "A00";
    private readonly string prefabPath = "Prefab/UI/Page/";

    private Stack<string> stack;
    public Stack<string> Stack
    {
        get
        {
            return stack;
        }
    }

    private Dictionary<string, GameObject> dic;
    public Dictionary<string, GameObject> Dic
    {
        get
        {
            return dic;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        stack = new Stack<string>();
        dic = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        if (debugPageCode == "")
        {
            TryOpenPage(defaultPageCode);
        }
        else
        {
            TryOpenPage(debugPageCode);
        }
    }

    public void StackInitialize(EPageSackInitialize data)
    {
        switch (data)
        {
            case EPageSackInitialize.Initialize:
                stack.Clear();
                stack.Push(defaultPageCode);
                break;

            case EPageSackInitialize.NotInitialize:
                break;
        }
    }

    public bool IsCreatedPage(string key)
    {
        if (key != PageCode && key != "")
        {
            stack.Push(key);
            PageCode = key;
        }

        if (Dic.Count == 0)
        {
            return false;
        }

        foreach (KeyValuePair<string, GameObject> items in Dic)
        {
            if (items.Key == key)
            {
                Open(key);
                return true;
            }
        }

        return false;
    }

    public void Open(string key)
    {
        Off();
        Dic[key].SetActive(true);
    }

    public void TryOpenPage(string key)
    {
        if (false == IsCreatedPage(key))
        {
            try
            {
                GameObject obj = Resources.Load<GameObject>(prefabPath + key);

                if (obj == null)
                {
                    Debug.LogError("Exception :: Prefab not found. Resources/" + prefabPath + key);
                }
                else
                {
                    Instantiate(Resources.Load<GameObject>(prefabPath + key));
                }
            }
            catch
            {
                if (key == "")
                {
                    Debug.LogError("Exception :: Page code is empty.");
                }
                else
                {
                    Debug.LogError("Exception :: Prefab not found. Resources/" + prefabPath + key);
                }
            }
        }
    }

    public void AddDictionary(string key, GameObject obj)
    {
        Off();
        dic.Add(key, obj);
    }

    public void PrevPage()
    {
        if (stack.Count < 2)
        {
            Debug.LogError("prev page is not found.");
            return;
        }

        Off();
        stack.Pop();
        string _prevPageCode = stack.Pop();
        PageCode = _prevPageCode;
        dic[_prevPageCode].SetActive(true);
        stack.Push(_prevPageCode);
    }

    private void Off()
    {
        if (Dic.Count == 0)
        {
            return;
        }

        foreach (KeyValuePair<string, GameObject> items in Dic)
        {
            if (true == Dic[items.Key].activeSelf)
            {
                Dic[items.Key].SetActive(false);
            }
        }
    }
}
