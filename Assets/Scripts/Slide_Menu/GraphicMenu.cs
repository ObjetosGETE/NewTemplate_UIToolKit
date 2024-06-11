using UnityEngine;
using UnityEngine.UIElements;
public class GraphicMenu : MonoBehaviour
{
    //private VisualElement graphicMenu;
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

        //root.AddToClassList("graphicmenu_hide");

        OcultarGraphicMenu();
    }

    private void MostrarGraphicMenu()
    {
        if (root != null)
        {
            root.style.display = DisplayStyle.Flex;
        }
    }

    private void OcultarGraphicMenu()
    {
        if (root != null)
        {
            root.style.display = DisplayStyle.None;
        }
    }
}
