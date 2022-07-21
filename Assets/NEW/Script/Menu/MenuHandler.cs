using UnityEngine;

namespace ScriptGG
{
    public class MenuHandler : MonoBehaviour
    {
        public SceneHandler sceneHandler;
        public GameObject menuContainer;

        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
                ToggleMenu();
        }

        public void ToggleMenu()
        {
            SetMenuActive(!menuContainer.activeSelf);
        }

        public void ShowMenu()
        {
            SetMenuActive(true);
        }

        public void HideMenu()
        {
            SetMenuActive(false);
        }

        private void SetMenuActive(bool active)
        {
            sceneHandler.SetGameActive(!active);
            menuContainer.SetActive(active);
        }
    }
}
