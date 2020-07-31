using UnityEngine;
using UnityEngine.UIElements;

public class Options : MonoBehaviour
{
    private VisualElement OptionsRoot;

    private VisualElement GraphicsSettings;

    private VisualElement AudioSettings;
    
    private void OnEnable()
    {
        OptionsRoot = GetComponent<UIDocument>().rootVisualElement;
        OptionsRoot.style.display = DisplayStyle.None;

        SetupOptionsMenuButtons();

        SetupGraphicsSettings();
        
        SetupAudioSettings();
    }

    private void SetupOptionsMenuButtons()
    {
        Button optionCloseButton = OptionsRoot.Query<Button>("OptionCloseBG");
        optionCloseButton.clickable.clicked += CloseOptionsScreen;
        
        Button graphicsButton = OptionsRoot.Query<Button>("Graphics");
        graphicsButton.clickable.clicked += ShowGraphicsOptions;
        
        Button audioButton = OptionsRoot.Query<Button>("Audio");
        audioButton.clickable.clicked += ShowAudioOptions;
    }

    private void SetupGraphicsSettings()
    {
        GraphicsSettings = OptionsRoot.Query<VisualElement>("GraphicsSettings");
    }

    private void SetupAudioSettings()
    {
        AudioSettings = OptionsRoot.Query<VisualElement>("AudioSettings");

        Slider slider = AudioSettings.Query<Slider>("Music");
        slider.RegisterCallback<ChangeEvent<float>>(MusicVolumeChanged);
        
        slider = AudioSettings.Query<Slider>("SFX");
        slider.RegisterCallback<ChangeEvent<float>>(SFXVolumeChanged);
    }

    private void MusicVolumeChanged(ChangeEvent<float> evt)
    {
    }
    
    private void SFXVolumeChanged(ChangeEvent<float> evt)
    {
    }

    private void CloseOptionsScreen()
    {
        OptionsRoot.style.display = DisplayStyle.None;
    }

    private void ShowGraphicsOptions()
    {
        AudioSettings.style.display = DisplayStyle.None;
        GraphicsSettings.style.display = DisplayStyle.Flex;
    }

    private void ShowAudioOptions()
    {
        AudioSettings.style.display = DisplayStyle.Flex;
        GraphicsSettings.style.display = DisplayStyle.None;
    }
}
