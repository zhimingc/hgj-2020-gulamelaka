using System;
using System.Collections.Generic;
using UnityEngine;
 
public class Toolbox : Singleton<Toolbox>
{
    // Used to track any global components added at runtime.
    private Dictionary<Type, Component> m_Components = new Dictionary<Type, Component>();

    // Prevent constructor use.
    protected Toolbox() { }
 
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Instance.Wake();
    }

    void Wake()
    {
        // Put initialization code here.
        Add(typeof(GameController));
    }
 
    // The methods below allow us to add global components at runtime.
    public Component Add(Type componentID)
    {
        if (m_Components.ContainsKey(componentID))
        {
            Debug.LogWarning("[Toolbox] Global component ID \"" + componentID + "\" already exist! Returning that.");
            return Get(componentID);
        }

        GameObject newChild = new GameObject(componentID.ToString());
        newChild.transform.SetParent(transform);
        var newComponent = newChild.AddComponent(componentID);
        m_Components.Add(componentID, newComponent);
        return newComponent;
    }

    public void AddAndInit<T>(T component) 
    {
        Add(typeof(T));
        m_Components[typeof(T)] = (Component) (object) component;
    }

    public void Remove(Type componentID)
    {
        Component component;

        if (m_Components.TryGetValue(componentID, out component))
        {
            Destroy(component);
            m_Components.Remove(componentID);
        }
        else
        {
            Debug.LogWarning("[Toolbox] Trying to remove nonexistent component ID \"" + componentID + "\"! Typo?");
        }
    }

    public T Get<T>() 
    {
        Type type = typeof(T);
        if (m_Components.ContainsKey(type))
        {
        return (T) Convert.ChangeType(m_Components[type], typeof(T));
        }

        Debug.LogWarning("[Toolbox] Global component ID \"" + type + "\" doesn't exist! Typo?");
        return (T) Convert.ChangeType(null, typeof(T));
    }

    public Component Get(Type componentID)
    {
        Component component;

        if (m_Components.TryGetValue(componentID, out component))
        {
            return component;
        }

        Debug.LogWarning("[Toolbox] Global component ID \"" + componentID + "\" doesn't exist! Typo?");
        return null;
    }
}