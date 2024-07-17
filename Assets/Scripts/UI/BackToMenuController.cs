using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(1000)]
public class BackToMenuController : MonoBehaviour {

    private Button menuButton;

    private struct ElementId {
        public const string MenuButton = "menu-button";
    }


    private void OnEnable() {
        var ui = GetComponent<UIDocument>().rootVisualElement;

        menuButton = ui.Q<Button>(
            ElementId.MenuButton
        );
        menuButton.clicked += GoToMenu;
    }


    private void GoToMenu() {}
}
