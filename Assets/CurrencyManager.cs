using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CurrencyChangedEvent : UnityEvent<CurrencyManager.Currency, int>
{
}

public class CurrencyManager : MonoBehaviour
{
    private static CurrencyManager CurrencyManagerInstance;

    public static CurrencyManager Instance
    {
        get { return CurrencyManagerInstance; }
    }
    
    public enum Currency
    {
        Diamond,
        Metal,
        Money,
        
        Length,
    }

    static int[] CurrencyValues;
    public static CurrencyChangedEvent ChangedEvent;

    public void OnEnable()
    {
        CurrencyManagerInstance = this;
        
        CurrencyValues = new int[(int)Currency.Length];
        ChangedEvent = new CurrencyChangedEvent();
    }

    public void AddCurrency(Currency currency, int value)
    {
        CurrencyValues[(int)currency] += value;
        ChangedEvent.Invoke(currency, CurrencyValues[(int)currency]);
    }
    
    public void RemoveCurrency(Currency currency, int value)
    {
        CurrencyValues[(int)currency] -= value;
        ChangedEvent.Invoke(currency, CurrencyValues[(int)currency]);
    }
}
