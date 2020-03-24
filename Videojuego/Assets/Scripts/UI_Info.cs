using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Info : MonoBehaviour
{
    public static UI_Info instance;

    public GameObject inventory;
    public GameObject[] panels;

    public int activePanel { get; private set; }
    public List<ResourceBehaviour> activeControllers = new List<ResourceBehaviour>();


    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        ClearActivePanel();
        instance = this;
    }

    public GameObject ShowPanel(ResourceBehaviour r, int id)
    {
        if (activePanel != id)
            ClearActivePanel();
        if (id >= 0 && id < panels.Length)
        {
            activeControllers.Add(r);
            inventory.SetActive(true);
            activePanel = id;
            return panels[id];
        }
        return null;
    }

    public GameObject ShowPanel(ResourceBehaviour r, string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] && panels[i].name == name)
                return ShowPanel(r, i);
        }
        return ShowPanel(r, -1);
    }

    private void ClearActivePanel()
    {
        List<ResourceBehaviour> copy = new List<ResourceBehaviour>(activeControllers);
        foreach (ResourceBehaviour r in copy)
        {
            r.SetSelected(false);
        }
        activePanel = -1;
    }

    public void ReleasePanel(ResourceBehaviour r, int id)
    {
        if (id >= 0 && id < panels.Length)
        {
            activeControllers.Remove(r);
            if (activeControllers.Count == 0)
            {
                inventory.SetActive(false);
                panels[id].SetActive(false);
                activePanel = -1;
            }
        }
    }

    public void ReleasePanel(ResourceBehaviour r, string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] && panels[i].name == name)
            {
                ReleasePanel(r, i);
                break;
            }
        }
    }

    private void HidePanels()
    {
        inventory.SetActive(false);
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i])
                panels[i].SetActive(false);
        }
    }

}
