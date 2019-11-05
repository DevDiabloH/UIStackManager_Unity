using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPage : MonoBehaviour
{
    public string targetPage;
    protected Button m_Button;

    protected virtual void Awake()
    {
        m_Button = GetComponent<Button>();

        try
        {
            m_Button.onClick.AddListener(OnClick);
        }
        catch
        {
            Debug.LogError("Button Component is not found");
        }
    }

    protected virtual void OnClick()
    {
        Open();
    }

    protected void Open()
    {
        UIStackManager.instance.TryOpenPage(targetPage);
    }
}
