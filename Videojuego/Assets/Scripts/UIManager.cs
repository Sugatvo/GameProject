using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject inventory;
    public GameObject[] panels;

    public int activePanel { get; private set; }
    public List<Entity> activeControllers = new List<Entity>();


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

    public GameObject ShowPanel(Entity entity, int id)
    {
        if(activePanel != id)
            ClearActivePanel();
        if (id >= 0 && id < panels.Length)
        {
            activeControllers.Add(entity);
            inventory.SetActive(true);
            activePanel = id;
            return panels[id];
        }
        return null;
    }

    public GameObject ShowPanel(Entity entity, string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] && panels[i].name == name)
                return ShowPanel(entity, i);
        }
        return ShowPanel(entity, -1);
    }

    private void ClearActivePanel()
    {
        List<Entity> copy = new List<Entity>(activeControllers);
        foreach (Entity entity in copy)
        {
            entity.SetSelected(false);
        }
        activePanel = -1;
    }

    public void ReleasePanel(Entity entity, int id)
    {
        if (id >= 0 && id < panels.Length)
        {
            activeControllers.Remove(entity);
            if (activeControllers.Count == 0)
            {
                inventory.SetActive(false);
                panels[id].SetActive(false);
                activePanel = -1;
            }
        }
    }

    public void ReleasePanel(Entity entity, string name)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i] && panels[i].name == name)
            {
                ReleasePanel(entity, i);
                break;
            }
        }
    }

    private void HidePanels()
    {
        inventory.SetActive(false);
        for (int i = 0; i < panels.Length; i++)
        {
            if(panels[i])
                panels[i].SetActive(false);
        }
    }


}
