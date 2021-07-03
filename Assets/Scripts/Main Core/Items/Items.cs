using UnityEngine;

public abstract class Items : GameManger
{
    [SerializeField]
    protected GameObject _effectItemBlack, _effectItemOrange, _effectItemRed;
    private Vector3 randomPosSpawn;

    protected Vector3 RandomPosSpawn  => randomPosSpawn;

    private void Awake()
    {
        randomPosSpawn = new Vector3(Random.Range(-12, 12), -8, -1f);
    }
    protected abstract void ChecktoDestroyItem();
    protected abstract void RandomTorquees();
    protected abstract void RandomForcee();

}
