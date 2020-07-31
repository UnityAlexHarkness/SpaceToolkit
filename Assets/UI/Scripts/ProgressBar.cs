using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ProgressBar : MonoBehaviour
{
    public UnityEvent ProgressFinished;
    
    private VisualElement FillBar;

    private float currentFill;

    private float Left;
    private float MaxSize;

    private bool Finished;
    private bool Initialized;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        FillBar = root.Query<VisualElement>("ProgressBarFill");
    }

    private void SetupFillBar()
    {
        currentFill = 0;

        if (!float.IsNaN(FillBar.layout.width))
        {
            Debug.Log("FRAME : " + Time.frameCount);
            MaxSize = FillBar.layout.width;
            Left = FillBar.layout.xMin;

            Initialized = true;
            Finished = false;
        }
    }

    void Update()
    {
        if (Initialized)
        {
            if (!Finished)
            {
                currentFill += Time.deltaTime * 15;
                currentFill = Mathf.Min(currentFill, 100);
    
                if (currentFill >= 100)
                {
                    Finished = true;
                    ProgressFinished?.Invoke();
                }
    
                float fill = (MaxSize - Left) * (currentFill / 100);
                FillBar.style.right = Left + (MaxSize - fill);
            }
        }
        else
        {
            SetupFillBar();
        }
    }
}
