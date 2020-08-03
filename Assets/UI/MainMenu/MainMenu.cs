using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;
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
        Profiler.BeginSample("MAIN MENU ENABLE");
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        Button homeButton = root.Query<Button>("Home");
        homeButton.clickable.clicked += ShowHome;
        RegisterSounds(homeButton);
        
        Button newButton = root.Query<Button>("NewGame");
        RegisterSounds(newButton);

        Button collectionButton = root.Query<Button>("Collection");
        collectionButton.clickable.clicked += ShowCollectionScreen;
        RegisterSounds(collectionButton);
        
        Button storeButton = root.Query<Button>("Store");
        RegisterSounds(storeButton);
        
        Button dailyLoginButton = root.Query<Button>("DailyLogin");
        dailyLoginButton.clickable.clicked += ShowDailyLoginScreen;
        
        Button optionButton = root.Query<Button>("OptionButton");
        optionButton.clickable.clicked += ShowOptionsScreen;
        
        VisualElement MoneyRoot = root.Query<VisualElement>("Money");
        Money = MoneyRoot.Query<Label>();

        VisualElement DiamondRoot = root.Query<VisualElement>("Diamond");
        Diamonds = DiamondRoot.Query<Label>();
        
        VisualElement MetalRoot = root.Query<VisualElement>("Metal");
        Metal = MetalRoot.Query<Label>();
        
        Profiler.EndSample();
        
        CurrencyManager.ChangedEvent.AddListener(CurrencyChanged);
        
        ShowHome();
    }

    private void RegisterSounds(Button button)
    {
        button.clickable.clicked += PlayClickedSound;
        button.RegisterCallback<MouseEnterEvent>(PlayHoverSound);

    }

    private void PlayClickedSound()
    {
        AudioManager.Instance.PlayOneShot("ButtonClick");
    }
    
    private void PlayHoverSound(MouseEnterEvent evt)
    {
        AudioManager.Instance.PlayOneShot("ButtonHover");
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

