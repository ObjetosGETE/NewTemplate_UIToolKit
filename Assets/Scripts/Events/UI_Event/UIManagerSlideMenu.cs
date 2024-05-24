using UnityEngine;
using UnityEngine.UIElements;

public class UIManagerSlideMenu : MonoBehaviour
{
    private VisualElement root;

    void OnEnable()
    {
        
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        // Slide Menu
        var buttonGraph = root.Q<Button>("ButtonGraph");
        buttonGraph.clicked += () => {var eventData = new Event("MostrarPainelGrafico", null); EventManager.Notify("MostrarPainelGrafico", eventData);};

        var buttonAudio = root.Q<Button>("ButtonAudio");
        buttonAudio.clicked += () => { var eventData = new Event("MostrarPainelAudio", null); EventManager.Notify("MostrarPainelAudio", eventData); };

        var buttonReq = root.Q<Button>("ButtonReq");
        buttonReq.clicked += () => { var eventData = new Event("MostrarPainelRequisitos", null); EventManager.Notify("MostrarPainelRequisitos", eventData); };

        // Graphic Menu
        var buttonDefault = root.Q<Button>("buttondefault");
        buttonDefault.clicked += () => { var eventData = new Event("VoltarGraficosAoPadrao", null); EventManager.Notify("VoltarGraficosAoPadrao", eventData); };

        var buttonSave = root.Q<Button>("buttonsave");
        buttonSave.clicked += () => { var eventData = new Event("SalvarConfiguracaoGrafica", null); EventManager.Notify("SalvarConfiguracaoGrafica", eventData); };
    }
}

