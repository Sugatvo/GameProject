using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    public EntityData entityData;

    public float health { get; private set; }
    public bool selected { get; private set; }

    protected bool started { get; private set; }

    private Camera mainCamera;

    private GameObject selector;
    private Transform canvas;
    private Slider healthBar;


    private void Start()
    {
        selector = transform.Find("Selector").gameObject;
        canvas = transform.Find("Canvas").transform;
        healthBar = transform.Find("Canvas/HealthBar").GetComponent<Slider>();
        if (!selector || !healthBar)
            Debug.LogError("Not valid Entity. Missing 'Selector' or 'HealthBar'");

        selector.SetActive(false);
        healthBar.gameObject.SetActive(false);

        mainCamera = Camera.main;

        health = entityData.MaxHealth;
        healthBar.maxValue = entityData.MaxHealth;
        healthBar.value = health;

        OnStart();
        started = true;
    }
    protected virtual void OnStart() { }

    private void Update()
    {
        canvas.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);

        OnUpdate();

        if (selected && Input.GetKeyDown(KeyCode.Delete))
            OnDelete();
    }
    protected virtual void OnUpdate() { }
    private void LateUpdate()
    {
        OnLateUpdate();
    }
    protected virtual void OnLateUpdate() { }

    protected virtual void OnDelete()
    {
        DestroyEntity();
    }

    public void SetSelected(bool state)
    {
        if (state)
        {
            if (!selected)
            {
                selected = true;
                selector.SetActive(true);
                OnSelected();
            }
        }
        else
        {
            if (selected)
            {
                selected = false;
                selector.SetActive(false);
                OnDeselected();
            }
        }
    }
    protected virtual void OnSelected() { }
    protected virtual void OnDeselected() { }

    public void Heal(float heal)
    {
        if (heal > 0)
        {
            health += heal;
            OnHealthChange(heal);
        }
    }
    public void Damage(float damage)
    {
        if (damage > 0)
        {
            health -= damage;
            if (health <= 0)
                DestroyEntity();
            else
                OnHealthChange(-damage);
        }
    }
    public void DestroyEntity()
    {
        SetSelected(false);
        OnDestroyEntity();
        Destroy(gameObject);
        if(gameObject.name == "Centro Urbano CPU")
        {
            UIManager.instance.ShowWin();
        }
    }
    protected virtual void OnHealthChange(float damage)
    {
        healthBar.value = health;
        if (healthBar.value != healthBar.maxValue && !healthBar.gameObject.activeSelf)
            healthBar.gameObject.SetActive(true);
    }
    protected virtual void OnDestroyEntity() { }
}
[System.Serializable]
public class EntityData
{
    [Header("Basic Information")]
    public float MaxHealth;
}
