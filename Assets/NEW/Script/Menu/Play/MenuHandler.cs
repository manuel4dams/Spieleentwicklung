using UnityEngine;

namespace ScriptGG
{
    public class MenuHandler : MonoBehaviour
    {
        public bool allowMenuToggle = false;
        public SceneHandler sceneHandler;
        public GameObject menuContainer;
        public GameObject settingsContainer;

        void Update()
        {
            if (!Input.GetButtonDown("Cancel"))
                return;

            if (settingsContainer.activeSelf)
            {
                Debug.Log("bla");
                HideSettingsShowMenu();
                return;
            }

            if (allowMenuToggle)
                ToggleMenu();
        }

        private void ToggleMenu()
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

        private void HideSettingsShowMenu()
        {
            settingsContainer.SetActive(false);
            menuContainer.SetActive(true);
        }
    }
}
