using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    private VisualElement character;
    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        character = uiDocument.rootVisualElement.Q<VisualElement>("Character");
        

        if (character == null)
        {
            Debug.LogError("Label do Cronômetro não encontrado. Certifique-se de que o nome está correto.");
            return;
        }
    }
    void OnEnable()
    {
        EventsUI.UIEventsManager.Instance.OnSendMessage += ReceiveMessage;
    }

    void OnDisable()
    {
        EventsUI.UIEventsManager.Instance.OnSendMessage -= ReceiveMessage;
    }

    void ReceiveMessage(EventsUI.UIEventsManager.MessageData data)
    {
        switch (data.Message)
        {
            case "Mostrar Personagem":
                character.style.display = DisplayStyle.Flex;
                break;
            case "Ocultar Personagem":
                character.style.display = DisplayStyle.None;
                break;
        }
    }
}
