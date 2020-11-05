using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgDealtIndicator : MonoBehaviour
{
    public TextMeshProUGUI dmgText;
    private RaycastHit hit;
    private float hitTopBound;
    private Player player;

    public void Initialize(int dmg, RaycastHit target)
    {
        dmgText.text = "" + dmg;
        hit = target;
        transform.parent = target.transform;

        Renderer hitRend = hit.transform.GetComponentInChildren<Renderer>();
        if (!hitRend) hitRend = hit.transform.GetComponentInParent<Renderer>();
        if (!hitRend) return;
        // Get the top of the renderer
        hitTopBound = hitRend.bounds.extents.y;

        transform.position = hit.transform.position + new Vector3(0, hitTopBound + 0.1f, -0.1f);
    }

    public void KillIndicator()
    {
        Destroy(this.gameObject);
    }
}
