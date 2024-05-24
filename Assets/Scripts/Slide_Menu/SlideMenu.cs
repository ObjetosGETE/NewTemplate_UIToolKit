using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static MyBox.EditorTools.MyGUI;

public class SlideMenu : MonoBehaviour, IEventListener
{

    private VisualElement graphicMenu;
    void Awake()
    {
        // Inicia a UI Document e encontra o componente Button dentro do documento da UI
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument n�o encontrado no GameObject.");
            return;
        }
        graphicMenu = uiDocument.rootVisualElement.Q<VisualElement>("graphicmenu");
        if (graphicMenu == null)
        {
            Debug.LogError("Menu de configura��o gr�fica n�o encontrado na �rvore de elementos da UI.");
            return;
        }
        graphicMenu.AddToClassList("graphicmenu_hide");
    }

    void OnEnable()
    {
        EventManager.Subscribe("MostrarPainelGrafico", this);
        EventManager.Subscribe("MostrarPainelAudio", this);
        EventManager.Subscribe("MostrarPainelRequisitos", this);
        EventManager.Subscribe("VoltarGraficosAoPadrao", this);
        EventManager.Subscribe("SalvarConfiguracaoGrafica", this);
    }

    void OnDisable()
    {
        EventManager.Unsubscribe("MostrarPainelGrafico", this);
        EventManager.Unsubscribe("MostrarPainelAudio", this);
        EventManager.Unsubscribe("MostrarPainelRequisitos", this);
        EventManager.Unsubscribe("VoltarGraficosAoPadrao", this);
        EventManager.Unsubscribe("SalvarConfiguracaoGrafica", this);
    }

    public void OnEventReceived(Event eventData)
    {
        switch (eventData.EventType)
        {
            case "MostrarPainelGrafico":
                graphicMenu.RemoveFromClassList("graphicmenu_hide");
                Debug.Log("Gr�ficos");
                break;
            case "MostrarPainelAudio":
                Debug.Log("Audio");
                break;
            case "MostrarPainelRequisitos":
                Debug.Log("Requisitos");
                break;
            case "VoltarGraficosAoPadrao":
                graphicMenu.AddToClassList("graphicmenu_hide");
                Debug.Log("Voltar Gr�ficos ao Padr�o");
                break;
            case "SalvarConfiguracaoGrafica":
                graphicMenu.AddToClassList("graphicmenu_hide");
                Debug.Log("Salvar Configura��o Gr�fica");
                break;
        }
    }


}
