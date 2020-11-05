using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DropAmmo : MonoBehaviour
{
    private AmmoDrop drop;
    private int num;

    public int minNum;
    public int maxNum;
    private AmmoTypes type;

    public void Start()
    {
        type = GetRandomAmmoType();
        num = Random.Range(minNum, maxNum);

        switch(type)
        {
            case AmmoTypes.pistol:
                drop = Resources.Load("PistolAmmoCrate", typeof(AmmoDrop)) as AmmoDrop;
                break;
            case AmmoTypes.scifi:
                drop = Resources.Load("ScifiAmmoCrate", typeof(AmmoDrop)) as AmmoDrop;
                break;
            default:
                break;
        }
    }

    public void SpawnAmmoDrop(Vector3 position)
    {
        Random.Range(0, 1);
        Vector3 actualPos = position + Vector3.up * 1.1f;
        Instantiate(drop, actualPos, Quaternion.identity);
        drop.Initialize(type, num);
    }

    public AmmoTypes GetRandomAmmoType()
    {
        System.Array A = System.Enum.GetValues(typeof(AmmoTypes));
        AmmoTypes V = (AmmoTypes)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
