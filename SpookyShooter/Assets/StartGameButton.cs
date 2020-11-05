using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(delegate { FindObjectOfType<LevelManager>().GetComponent<LevelManager>().NextLevel(); });
    }
}
