using System;
using UnityEngine.UIElements;

class ButtonTemplate : VisualElement
{
    public ButtonTemplate()
    {
        m_Status = String.Empty;
    }

    string m_Status;
    public string status { get; set; }

    public new class UxmlFactory : UxmlFactory<ButtonTemplate, UxmlTraits> {}

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
       UxmlStringAttributeDescription m_Status = new UxmlStringAttributeDescription { name = "status" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            ((ButtonTemplate)ve).status = m_Status.GetValueFromBag(bag, cc);
            Button button = ve.Query<Button>();
            
            if (button != null )
                button.text = m_Status.GetValueFromBag(bag, cc);
        }
    }
}