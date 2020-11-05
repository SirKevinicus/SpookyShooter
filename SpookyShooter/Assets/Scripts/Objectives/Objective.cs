using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Objective : MonoBehaviour
{
    protected ObjectivesManager manager;
    public TextMeshProUGUI ojc_tmpro;
    public string objective_text;

    public delegate void OnObjectiveComplete();
    public event OnObjectiveComplete onObjectiveComplete;

    public virtual void ActivateObjective(ObjectivesManager manager)
    {
        this.manager = manager;
    }

    public virtual void ActivateObjective(ObjectivesManager manager, int goal)
    {
        ojc_tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected virtual void CompleteObjective()
    {
        onObjectiveComplete?.Invoke();
    }
    protected virtual void UpdateObjective()
    {
        ojc_tmpro.text = objective_text;
    }
}
