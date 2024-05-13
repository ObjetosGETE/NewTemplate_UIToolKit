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
            Debug.LogError("UIDocument não encontrado no GameObject.");
            return;
        }

        buttonDisplayStopWatch = uiDocument.rootVisualElement.Q<Button>("ButtonDisplayStopWatch");

        if (buttonDisplayStopWatch == null)
        {
            Debug.LogError("Botão de parar o cronômetro não encontrado. Certifique-se de que o nome está correto.");
            return;
        }

        buttonDisplayStopWatch.RegisterCallback<ClickEvent>(ButtonClicked);
    }

    void OnDisable()
    {
        // Desregistra o callback para evitar vazamentos de memória
        if (buttonDisplayStopWatch != null)
        {
            buttonDisplayStopWatch.UnregisterCallback<ClickEvent>(ButtonClicked);
        }
    }

    void ButtonClicked(ClickEvent evt)
    {
        Debug.Log("Button clicked!");
        StopTheStopwatch();
        // Adiciona essa linha para parar a propagação do evento
        evt.StopPropagation();

    }

    private void StopTheStopwatch()
    {
        EventsUI.UIEventsManager.Instance.HideCharacter();
        EventsUI.UIEventsManager.Instance.StopStopwatch();
    }
}
