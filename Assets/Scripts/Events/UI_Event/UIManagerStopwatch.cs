using UnityEngine;
using UnityEngine.UIElements;

public class UIManagerStopwatch : MonoBehaviour
{
    private VisualElement root;

    void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;
               
        var pauseButton = root.Q<Button>("pauseButton");
        pauseButton.clicked += () => {var eventData = new Event("PausarCronometro", null); EventManager.Notify("PausarCronometro", eventData);};

        var unPauseButton = root.Q<Button>("unPauseButton");
        unPauseButton.clicked += () => {var eventData = new Event("DespausarCronometro", null); EventManager.Notify("DespausarCronometro", eventData);};

        var restartButton = root.Q<Button>("restartButton");
        restartButton.clicked += () => {var eventData = new Event("ReiniciarCronometro", null); EventManager.Notify("ReiniciarCronometro", eventData);};

        var stopButton = root.Q<Button>("stopButton");
        stopButton.clicked += () => {var eventData = new Event("PararCronometro", null); EventManager.Notify("PararCronometro", eventData);};
    }
}

