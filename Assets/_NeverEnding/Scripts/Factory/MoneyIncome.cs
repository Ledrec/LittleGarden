using UnityEngine;
using TMPro;
using System.Numerics;

public class MoneyIncome : MonoBehaviour
{
    [SerializeField]private TextMeshPro price;

    private void OnEnable()
    {
        transform.GetChild(0).rotation = Camera.main.transform.rotation;
    }
    public void SetPrice(BigInteger _newPrice)
    {
        if (price == null) { return; }
        price.text = _newPrice.ToCompactString();
    }
    public void FinishAnimation() => IncomeMessages.RemoveMessage(gameObject);
}