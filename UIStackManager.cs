using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPageStackInitialize { Initialize, NotInitialize }

public class UIStackManager : MonoBehaviour
{
    public static UIStackManager instance = null;
    public string debugPageCode = string.Empty;
    public string PageCode { get; private set; }

    private readonly string m_DefaultPageCode = "A00";
    private readonly string m_PrefabPath = "Prefab/UI/Page/";

    private Stack<string> m_Stack;
    public Stack<string> Stack
    {
        get
        {
            return m_Stack;
        }
    }

    private Dictionary<string, GameObject> m_Dic;
    public Dictionary<string, GameObject> Dic
    {
        get
        {
            return m_Dic;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        m_Stack = new Stack<string>();
        m_Dic = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        if (debugPageCode == "")
        {
            TryOpenPage(m_DefaultPageCode);
        }
        else
        {
            TryOpenPage(debugPageCode);
        }
    }

    public void StackInitialize(EPageStackInitialize data)
    {
        switch (data)
        {
            case EPageStackInitialize.Initialize:
                m_Stack.Clear();
                m_Stack.Push(m_DefaultPageCode);
                break;

            case EPageStackInitialize.NotInitialize:
                break;
        }
    }

    public bool IsCreatedPage(string key)
    {
        if (key != PageCode && key != "")
        {
            m_Stack.Push(key);
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
                GameObject obj = Resources.Load<GameObject>(m_PrefabPath + key);

                if (obj == null)
                {
                    Debug.LogError("Exception :: Prefab not found. Resources/" + m_PrefabPath + key);
                }
                else
                {
                    GameObject newObject = Instantiate(Resources.Load<GameObject>(m_PrefabPath + key));
                    AddDictionary(key, newObject);
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
                    Debug.LogError("Exception :: Prefab not found. Resources/" + m_PrefabPath + key);
                }
            }
        }
    }

    public void AddDictionary(string key, GameObject obj)
    {
        Off();
        m_Dic.Add(key, obj);
    }

    public void PrevPage()
    {
        if (m_Stack.Count < 2)
        {
            Debug.LogError("prev page is not found.");
            return;
        }

        Off();
        m_Stack.Pop();
        string _prevPageCode = m_Stack.Pop();
        PageCode = _prevPageCode;
        m_Dic[_prevPageCode].SetActive(true);
        m_Stack.Push(_prevPageCode);
    }

    private void Off()
    {
        if (Dic.Count == 0)
        {
            return;
        }

        foreach (KeyValuePair<string, GameObject> items in Dic)
        {
            if(true == Dic[items.Key].activeSelf)
            {
                Dic[items.Key].SetActive(false);
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style;
        float width;
        float height;
        style = new GUIStyle();
        style.fontSize = 40;
        style.normal.textColor = Color.green;

        width = Screen.width / 1.5f;
        height = Screen.height;


        GUI.Label(new Rect(0, height - 100f, 100, 100), "Stack Count :: " + stack.Count, style);
        GUI.Label(new Rect(0, height - 150f, 100, 100), "Curr Code :: " + CURR_PAGE_CODE, style);

        string[] strs = stack.ToArray();

        for (int i = 0; i < strs.Length; i++)
        {
            GUI.Label(new Rect(0, height - 200f + (i * -50f), 100, 100), "Stack [" + i + "] :: " + strs[i], style);
        }
    }
}
