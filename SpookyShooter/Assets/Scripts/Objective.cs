using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Objective : MonoBehaviour
{
    public TextMeshProUGUI ojc_tmpro;
    public string objective_text;

    public delegate void OnObjectiveComplete();
    public event OnObjectiveComplete onObjectiveComplete;

    public virtual void ActivateObjective(Player player, int goal)
    {
        ojc_tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }

    protected void CompleteObjective()
    {
        onObjectiveComplete?.Invoke();
    }
    protected virtual void UpdateObjective()
    {
        ojc_tmpro.text = objective_text;
    }
}
