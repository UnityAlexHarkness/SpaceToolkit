using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

public class CollectionScreen : MonoBehaviour
{
    private ScrollView ScrollView;
    
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        ScrollView = root.Query<ScrollView>("Contents");
        
        AsyncOperationHandle<VisualTreeAsset> handle = Addressables.LoadAssetAsync<VisualTreeAsset>("CollectionItem");
        handle.Completed += FillWithRandomStuff;
    }

    private void FillWithRandomStuff(AsyncOperationHandle<VisualTreeAsset> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
            CreateCollectionItems(handle.Result);
    }
    
    private void CreateCollectionItems(VisualTreeAsset visualTree) 
    {
        AsyncOperationHandle handle = Addressables.LoadAssetsAsync<Texture2D>("Number", null);
        handle.Completed += operationHandle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                List<Texture2D> textures = handle.Result as List<Texture2D>;
                
                int current = 0;

                for (int i = 0; i < 200; ++i)
                {
                    VisualElement ui = visualTree.CloneTree("Background");
                    VisualElement icon = ui.Query<VisualElement>("Icon");
                    icon.style.backgroundImage = textures[current];

                    ScrollView.Add(ui);

                    current++;
                    if (current == 10)
                        current = 0;
                }
            }
        };
    }
}