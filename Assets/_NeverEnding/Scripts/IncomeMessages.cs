using UnityEngine;
using UnityEngine.Pool;
using System.Numerics;

public class IncomeMessages : MonoBehaviour
{
    public static IncomeMessages instance;
    [SerializeField]private GameObject moneyIncomePrefab;
    private ObjectPool<GameObject> pool;
    
    private void OnEnable()
    {
        instance = this;
        pool = new ObjectPool<GameObject>(
            () => Instantiate(moneyIncomePrefab, UnityEngine.Vector3.zero, UnityEngine.Quaternion.identity, transform),
            x => x.SetActive(true),
            x => x.SetActive(false),
            x => Destroy(x)
        );
    }
    
    public static void AddMessage(UnityEngine.Vector3 _position, BigInteger _amount)
    {
        if (instance == null) { return; }
        GameObject _go = instance.pool.Get();
        _go.transform.position = _position;
        _go.GetComponent<MoneyIncome>().SetPrice(_amount);
    }
    public static void RemoveMessage(GameObject _go) => instance.pool.Release(_go);
}