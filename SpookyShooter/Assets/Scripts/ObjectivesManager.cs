using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    private Canvas canvas;
    private Player player;
    private ShootingGallery gallery;

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

    public void ShowObjectivesHUD()
    {
        canvas.enabled = true;
    }
    public void HideObjectivesHUD()
    {
        canvas.enabled = false;
    }

    public void CreateObjective(Objective objective, int goal)
    {
        Objective o = Instantiate(objective, transform);
        o.ActivateObjective(player, goal);
    }
}
