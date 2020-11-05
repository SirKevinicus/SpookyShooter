using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    private Canvas canvas;
    private Player player;

    public GameObject objectivesHeader;
    public Objective ojc_explore;
    private ShootingGallery gallery;

    public List<Objective> objectives = new List<Objective>();

    private Vector3 startPos;
    public int lineHeight;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        player = FindObjectOfType<Player>();

        // REFERENCES
        gallery = FindObjectOfType<ShootingGallery>();
        gallery.onStartGallery += HideObjectivesHUD;
        gallery.onEndGallery += ShowObjectivesHUD;

        ShowObjectivesHUD();
    }

    public void ShowExploreObjective()
    {
        startPos = objectivesHeader.transform.localPosition - new Vector3(0, 30, 0);
        CreateObjective(ojc_explore);
    }

    public void ShowObjectivesHUD()
    {
        canvas.enabled = true;
    }
    public void HideObjectivesHUD()
    {
        canvas.enabled = false;
    }
    public void CreateObjective(Objective objective)
    {
        Objective o = Instantiate(objective, transform);
        o.transform.localPosition = startPos;
        o.ActivateObjective(this);

        objectives.Add(o);
        startPos -= new Vector3(0, lineHeight, 0);
    }

    public void CreateObjective(Objective objective, int goal)
    {
        Objective o = Instantiate(objective, transform);
        o.transform.localPosition = startPos;

        o.ActivateObjective(this, goal);

        objectives.Add(o);
        startPos -= new Vector3(0, lineHeight, 0);
    }

    public void RemoveObjective(Objective objective)
    {
        objectives.Remove(objective);
        Destroy(objective.gameObject);
        startPos += new Vector3(0, lineHeight, 0);

        foreach (Objective obj in objectives)
        {
            startPos += new Vector3(0, lineHeight, 0);
            obj.transform.localPosition += startPos;
        }
    }
}
