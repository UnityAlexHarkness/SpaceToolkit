using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public UIDocument Options;
    public UIDocument Collection;
    public UIDocument DailyLogin;

    private Label Diamonds;
    private Label Metal;
    private Label Money;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        VisualElement homeTemplate = root.Query<VisualElement>("Home");
        Button homeButton = homeTemplate.Query<Button>();
        homeButton.clickable.clicked += ShowHome;

        VisualElement collectionTemplate = root.Query<VisualElement>("Collection");
        Button collectionButton = collectionTemplate.Query<Button>();
        collectionButton.clickable.clicked += ShowCollectionScreen;
        
        VisualElement dailyLoginTemplate = root.Query<VisualElement>("DailyLogin");
        Button dailyLoginButton = dailyLoginTemplate.Query<Button>();
        dailyLoginButton.clickable.clicked += ShowDailyLoginScreen;
        
        Button optionButton = root.Query<Button>("OptionButton");
        optionButton.clickable.clicked += ShowOptionsScreen;
        
        VisualElement MoneyRoot = root.Query<VisualElement>("Money");
        Money = MoneyRoot.Query<Label>();

        VisualElement DiamondRoot = root.Query<VisualElement>("Diamond");
        Diamonds = DiamondRoot.Query<Label>();
        
        VisualElement MetalRoot = root.Query<VisualElement>("Metal");
        Metal = MetalRoot.Query<Label>();
        
        CurrencyManager.ChangedEvent.AddListener(CurrencyChanged);
        
        ShowHome();
    }

    private void OnDestroy()
    {
        CurrencyManager.ChangedEvent.RemoveListener(CurrencyChanged);
    }

    private void CurrencyChanged(CurrencyManager.Currency currency, int newValue)
    {
        switch (currency)
        {
            case CurrencyManager.Currency.Money:
                StartCoroutine(ChangeCurrencyText(Money, int.Parse(Money.text), newValue));
                break;
            case CurrencyManager.Currency.Diamond:
                StartCoroutine(ChangeCurrencyText(Diamonds, int.Parse(Diamonds.text), newValue));
                break;
            case CurrencyManager.Currency.Metal:
                StartCoroutine(ChangeCurrencyText(Metal, int.Parse(Metal.text), newValue));
                break;
        }
    }

    private IEnumerator ChangeCurrencyText(Label text, int oldValue, int newValue)
    {
        int diff = newValue - oldValue;
        float time = 0;

        while (time < 1.0f)
        {
            time += Time.deltaTime;
            int value = (int)(diff * time);

            text.text = (oldValue + value).ToString();
            yield return null;
        }
        
        text.text = newValue.ToString();
        yield return null;
    }

    private void ShowHome()
    {
        Collection.rootVisualElement.style.display = DisplayStyle.None;
        DailyLogin.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    private void ShowCollectionScreen()
    {
        Collection.rootVisualElement.style.display = DisplayStyle.Flex;
        DailyLogin.rootVisualElement.style.display = DisplayStyle.None;
    }
    
    private void ShowDailyLoginScreen()
    {
        Collection.rootVisualElement.style.display = DisplayStyle.None;
        DailyLogin.rootVisualElement.style.display = DisplayStyle.Flex;
    }
    
    private void ShowOptionsScreen()
    {
        Options.rootVisualElement.style.display = DisplayStyle.Flex;
        Options.rootVisualElement.BringToFront();
    }
}

