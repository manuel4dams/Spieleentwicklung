using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptGG
{
    public class SceneHandler : MonoBehaviour
    {
        // private float initialFixedDeltaTime;

        // void Start()
        // {
        //     initialFixedDeltaTime = Time.fixedDeltaTime;
        // }

        public void SwitchToMenu()
        {
            SetGameActive(true);
            SceneManager.LoadScene("Menu");
        }

        public void SwitchToPlay()
        {
            SetGameActive(true);
            SceneManager.LoadScene("MainGG");
        }

        public void SwitchToSettings()
        {
            SetGameActive(true);
            SceneManager.LoadScene("Settings");
        }

        public void SetGameActive(bool active)
        {
            Time.timeScale = active ? 1f : 0f;
            // Next line causes the player to get blown away if the game is paused when the player is standing on a ragdoll zombie
            // So for any reason hints to do this were wrong
            // Time.fixedDeltaTime = initialFixedDeltaTime * Time.timeScale;
            AudioListener.pause = !active;
        }

        public void Exit()
        {
            Application.Quit(0);
        }
    }
}
