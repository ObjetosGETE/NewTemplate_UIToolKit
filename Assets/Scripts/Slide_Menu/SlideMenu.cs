using UnityEngine;
using UnityEngine.UIElements;


public class SlideMenu : MonoBehaviour, IEventListener
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

        var buttonGraph = root.Q<Button>("ButtonGraph");
        buttonGraph.clicked += () => { var eventData = new Event("MostrarPainelGrafico", null); EventManager.Notify("MostrarPainelGrafico", eventData); };

        var buttonAudio = root.Q<Button>("ButtonAudio");
        buttonAudio.clicked += () => { var eventData = new Event("MostrarPainelAudio", null); EventManager.Notify("MostrarPainelAudio", eventData); };

        var buttonReq = root.Q<Button>("ButtonReq");
        buttonReq.clicked += () => { var eventData = new Event("MostrarPainelRequisitos", null); EventManager.Notify("MostrarPainelRequisitos", eventData); };

        OcultarMenu();
    }

    void OnEnable()
    {
        EventManager.Subscribe("MostrarPainelGrafico", this);
        EventManager.Subscribe("MostrarPainelAudio", this);
        EventManager.Subscribe("MostrarPainelRequisitos", this);
        EventManager.Subscribe("MostrarSlideMenu", this);  // Inscrever-se para mostrar o menu
        EventManager.Subscribe("OcultarSlideMenu", this);  // Inscrever-se para ocultar o menu

        //EventManager.Subscribe("VoltarGraficosAoPadrao", this);
        //EventManager.Subscribe("SalvarConfiguracaoGrafica", this);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe("MostrarPainelGrafico", this);
        EventManager.Unsubscribe("MostrarPainelAudio", this);
        EventManager.Unsubscribe("MostrarPainelRequisitos", this);
        EventManager.Unsubscribe("MostrarSlideMenu", this);  // Cancelar a inscrição do evento
        EventManager.Unsubscribe("OcultarSlideMenu", this);  // Cancelar a inscrição do evento

        //EventManager.Unsubscribe("VoltarGraficosAoPadrao", this);
        //EventManager.Unsubscribe("SalvarConfiguracaoGrafica", this);

        
    }

    public void OnEventReceived(Event eventData)
    {
        switch (eventData.EventType)
        {
            case "MostrarPainelGrafico":
                //graphicMenu.RemoveFromClassList("graphicmenu_hide");
                Debug.Log("Gráficos");
                break;
            case "MostrarPainelAudio":
                Debug.Log("Audio");
                break;
            case "MostrarPainelRequisitos":
                Debug.Log("Requisitos");
                break;
            case "MostrarSlideMenu":
                MostrarMenu();
                break;
            case "OcultarSlideMenu":
                OcultarMenu();
                break;
                //case "VoltarGraficosAoPadrao":
                //    graphicMenu.AddToClassList("graphicmenu_hide");
                //    Debug.Log("Voltar Gráficos ao Padrão");
                //    break;
                //case "SalvarConfiguracaoGrafica":
                //    graphicMenu.AddToClassList("graphicmenu_hide");
                //    Debug.Log("Salvar Configuração Gráfica");
                //    break;
        }
    }

    private void MostrarMenu()
    {
        if (root != null)
        {
            root.style.display = DisplayStyle.Flex; 
        }
    }

    private void OcultarMenu()
    {
        if (root != null)
        {
            root.style.display = DisplayStyle.None; 
        }
    }
}
