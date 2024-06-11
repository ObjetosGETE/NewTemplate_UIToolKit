using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DisplayStopWatch : MonoBehaviour, IEventListener
{
    private Label displayStopWatch;
    private float timeElapsed = 0f;
    private bool timerRunning = false;
    private VisualElement root;

    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("UIDocument não encontrado no GameObject.");
            return;
        }
        root = uiDocument.rootVisualElement;
        displayStopWatch = root.Q<Label>("DisplayStopWatch");

        if (displayStopWatch == null)
        {
            Debug.LogError("Label do Cronômetro não encontrado. Certifique-se de que o nome está correto.");
            return;
        }
        
        root = uiDocument.rootVisualElement;

        var pauseButton = root.Q<Button>("pauseButton");
        pauseButton.clicked += () => { var eventData = new Event("PausarCronometro", null); EventManager.Notify("PausarCronometro", eventData); };

        var unPauseButton = root.Q<Button>("unPauseButton");
        unPauseButton.clicked += () => { var eventData = new Event("DespausarCronometro", null); EventManager.Notify("DespausarCronometro", eventData); };

        var restartButton = root.Q<Button>("restartButton");
        restartButton.clicked += () => { var eventData = new Event("ReiniciarCronometro", null); EventManager.Notify("ReiniciarCronometro", eventData); };

        var stopButton = root.Q<Button>("stopButton");
        stopButton.clicked += () => { var eventData = new Event("PararCronometro", null); EventManager.Notify("PararCronometro", eventData); };
    }

    void OnEnable()
    {
        StartTimer();
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
        StopTimer();
    }

    private void StartTimer()
    {
        timerRunning = true;
        StartCoroutine(UpdateTimer());
    }

    private void StopTimer()
    {
        timerRunning = false;
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerRunning)
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

    public void OnEventReceived(Event eventData)
    {
        switch (eventData.EventType)
        {
            case "DespausarCronometro":
                StartTimer();
                break;
            case "PausarCronometro":
                StopTimer();
                break;
            case "ReiniciarCronometro":
                timeElapsed = 0;
                UpdateLabel(0, 0, 0);
                if (!timerRunning) StartTimer();
                break;
            case "PararCronometro":
                StopTimer();
                timeElapsed = 0;
                break;
        }
    }
}




