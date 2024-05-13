using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayStopWatch : MonoBehaviour
{
    private Label displayStopWatch;
    private float timeElapsed = 0f;
    private bool timerRunning = false; // Inicializa como false para evitar execução sem controle

    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        displayStopWatch = uiDocument.rootVisualElement.Q<Label>("DisplayStopWatch");

        if (displayStopWatch == null)
        {
            Debug.LogError("Label do Cronômetro não encontrado. Certifique-se de que o nome está correto.");
            return;
        }
    }

    void OnEnable()
    {
        EventsUI.UIEventsManager.Instance.OnSendMessage += ReceiveMessage;
        StartTimer(); // Inicia a corrotina apenas se tudo estiver configurado corretamente
    }

    void OnDisable()
    {
        StopTimer(); // Para a corrotina
        EventsUI.UIEventsManager.Instance.OnSendMessage -= ReceiveMessage;
    }

    private void StartTimer()
    {
        timerRunning = true;
        StartCoroutine(UpdateTimer());
    }

    private void StopTimer()
    {
        timerRunning = false;
        StopCoroutine(UpdateTimer()); // Garante que a corrotina seja interrompida corretamente
    }

    private IEnumerator UpdateTimer()
    {
        while (timerRunning) // Ajusta a condição de loop para depender do estado 'timerRunning'
        {
            timeElapsed += Time.deltaTime;
            UpdateLabel((int)(timeElapsed / 3600), (int)((timeElapsed / 60) % 60), (int)(timeElapsed % 60));
            yield return null;
        }
    }

    private void UpdateLabel(int hours, int minutes, int seconds)
    {
        displayStopWatch.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    void ReceiveMessage(EventsUI.UIEventsManager.MessageData data)
    {
        switch (data.Message)
        {
            case "Iniciar Cronometro":
                StartTimer();
                break;
            case "Parar Cronometro":
                StopTimer();
                break;
            case "Reiniciar Cronometro":
                timeElapsed = 0;
                UpdateLabel(0, 0, 0);
                if (!timerRunning) StartTimer();
                break;
        }
    }
}



