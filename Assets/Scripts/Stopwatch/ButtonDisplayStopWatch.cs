using UnityEngine;
using UnityEngine.UIElements;

public class ButtonDisplayStopWatch : MonoBehaviour
{
    private Button buttonDisplayStopWatch;

    void Start()
    {
        // Inicia a UI Document e encontra o componente Button dentro do documento da UI
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument n�o encontrado no GameObject.");
            return;
        }

        buttonDisplayStopWatch = uiDocument.rootVisualElement.Q<Button>("ButtonDisplayStopWatch");

        if (buttonDisplayStopWatch == null)
        {
            Debug.LogError("Bot�o de parar o cron�metro n�o encontrado. Certifique-se de que o nome est� correto.");
            return;
        }

        buttonDisplayStopWatch.RegisterCallback<ClickEvent>(ButtonClicked);
    }

    void OnDisable()
    {
        // Desregistra o callback para evitar vazamentos de mem�ria
        if (buttonDisplayStopWatch != null)
        {
            buttonDisplayStopWatch.UnregisterCallback<ClickEvent>(ButtonClicked);
        }
    }

    void ButtonClicked(ClickEvent evt)
    {
        Debug.Log("Button clicked!");
        StopTheStopwatch();
        // Adiciona essa linha para parar a propaga��o do evento
        evt.StopPropagation();

    }

    private void StopTheStopwatch()
    {
        EventsUI.UIEventsManager.Instance.HideCharacter();
        EventsUI.UIEventsManager.Instance.StopStopwatch();
    }
}
