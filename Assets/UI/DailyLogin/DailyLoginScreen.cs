using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class DailyLoginScreen : MonoBehaviour
{
    private VisualElement Root;
    private int currentDay = 1;
    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        
        FillWithRandomStuff();
    }

    private void FillWithRandomStuff()
    {
        CreateDailyLoginItems();
    }
    
    private void CreateDailyLoginItems() 
    {
        for (int i = 1; i < 7; ++i)
        {
            int currencyType = Random.Range(0, 2);
            AsyncOperationHandle<Texture2D> handle;
                
            if (currencyType == 0)
                handle = Addressables.LoadAssetAsync<Texture2D>("Icon_Diamond.png");
            else
                handle = Addressables.LoadAssetAsync<Texture2D>("Icon_silvers.png");

            int current = i;
                
            handle.Completed += operationHandle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Texture2D texture = handle.Result;
                    
                    VisualElement day = Root.Query<VisualElement>("Day" + current);
                    
                    Label label = day.Query<Label>("Day");
                    label.text = "Day " + current.ToString();

                    VisualElement icon = day.Q<VisualElement>("Icon");
                    icon.style.backgroundImage = texture;
                    
                    label = day.Query<Label>("Value");
                    int loginValue = Random.Range(100, 1000);
                    label.text = loginValue.ToString();

                    day.RegisterCallback<ClickEvent>(evt =>
                    {
                        if (currentDay == current)
                        {
                            VisualElement slash = day.Query<VisualElement>("Slash");
                            slash.style.display = DisplayStyle.Flex;
                            
                            CurrencyManager.Instance.AddCurrency((CurrencyManager.Currency)currencyType, loginValue);
                            currentDay++;
                        }
                    });
                }
            };
        }
        
        VisualElement day7 = Root.Query<VisualElement>("Day7");
        Label money = day7.Query<Label>("MoneyValue");
        int moneyValue = Random.Range(100, 1000);
        money.text = moneyValue.ToString();
        
        Label diamond = day7.Query<Label>("DiamondValue");
        int diamondValue = Random.Range(100, 1000);
        diamond.text = diamondValue.ToString();
        
        day7.RegisterCallback<ClickEvent>(evt =>
        {
            if (currentDay == 7)
            {
                VisualElement slash = day7.Query<VisualElement>("Slash");
                slash.style.display = DisplayStyle.Flex;

                CurrencyManager.Instance.AddCurrency(CurrencyManager.Currency.Money, moneyValue);
                CurrencyManager.Instance.AddCurrency(CurrencyManager.Currency.Diamond, diamondValue);
                currentDay++;
            }
        });
    }
}