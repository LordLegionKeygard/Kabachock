using UnityEngine;

public class EggSpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] eggObjects;
    private int eggsCount = 60;

    private void Start()
    {
        SpawnEggs();
    }

    private void SpawnEggs()
    {
        for (int i = 0; i < eggsCount; i++)
            Instantiate(SelectEgg(), SelectEggPosition(), Quaternion.identity);
    }

    private GameObject SelectEgg()
    {
        var rnd = Random.Range(0, 100);

        if (rnd < 10) return eggObjects[(int)EggType.Epic];
        else if (rnd < 30) return eggObjects[(int)EggType.Rare];
        else return eggObjects[(int)EggType.Common];
    }

    private Vector3 SelectEggPosition()
    {
        var rndX = Random.Range(-24, 24);
        var rndZ = Random.Range(-24, 24);

        return new Vector3(rndX, 30, rndZ);
    }
}
