using UnityEngine;

public class EggSpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _eggsType;
    private int _eggsCount = 60;

    private void Start()
    {
        SpawnEggs();
    }

    private void SpawnEggs()
    {
        for (int i = 0; i < _eggsCount; i++)
        {
            Instantiate(SelectEgg(), SelectEggPosition(), Quaternion.identity);
        }
    }

    private GameObject SelectEgg()
    {
        var rnd = Random.Range(0, 100);

        if (rnd < 10) return _eggsType[(int)EggType.Epic];
        else if (rnd < 30) return _eggsType[(int)EggType.Rare];
        else return _eggsType[(int)EggType.Common];
    }

    private Vector3 SelectEggPosition()
    {
        var rndX = Random.Range(-24, 24);
        var rndZ = Random.Range(-24, 24);

        return new Vector3(rndX, 30, rndZ);
    }
}
