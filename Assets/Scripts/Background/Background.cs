using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour, IEventListener
{
    private VisualElement root;
    void Awake()
    {
        // Inicia a UI Document e encontra o componente Button dentro do documento da UI
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument não encontrado no GameObject.");
            return;
        }
        root = uiDocument.rootVisualElement;

        var buttonSlideMenu = root.Q<Button>("button_slide_menu");
        buttonSlideMenu.clicked += () => { var eventData = new Event("MostrarSlideMenu", null); EventManager.Notify("MostrarSlideMenu", eventData); };
    }
        public void OnEventReceived(Event eventData)
    { 
    }
}
