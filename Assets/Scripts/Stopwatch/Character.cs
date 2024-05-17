using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour, IEventListener
{
    private VisualElement character;
    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        character = uiDocument.rootVisualElement.Q<VisualElement>("character");
        

        if (character == null)
        {
            Debug.LogError("Label do Cronômetro não encontrado. Certifique-se de que o nome está correto.");
            return;
        }
    }
    void OnEnable()
    {
        EventManager.Subscribe("PausarCronometro", this);
        EventManager.Subscribe("DespausarCronometro", this);
        EventManager.Subscribe("ReiniciarCronometro", this);
        EventManager.Subscribe("PararCronometro", this);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe("PausarCronometro", this);
        EventManager.Unsubscribe("DespausarCronometro", this);
        EventManager.Unsubscribe("ReiniciarCronometro", this);
        EventManager.Unsubscribe("PararCronometro", this);
        
    }

    public void OnEventReceived(Event eventData)
    {
        switch (eventData.EventType)
        {
            case "DespausarCronometro":
                character.style.display = DisplayStyle.Flex;
                break;
            case "PausarCronometro":
                character.style.display = DisplayStyle.None;
                break;
            case "ReiniciarCronometro":
                character.style.display = DisplayStyle.Flex;
                break;
            case "PararCronometro":
                character.style.display = DisplayStyle.None;
                break;
        }
    }
}
