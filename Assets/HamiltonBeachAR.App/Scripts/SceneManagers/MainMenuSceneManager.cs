using UnityEngine;

using SINe.Unity.Managers;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for the Menus/Main;
    /// </summary>
    public class MainMenuSceneManager : SceneManager<MainMenuSceneManager>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Alive
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        override protected void Update()
        {
            base.Update();

            debugTryLoadScenario();
        }

        private void debugTryLoadScenario()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.LogWarningFormat("Loading Debug ScenarioDAB...");
                ScenarioSceneManager.TryLoadScenarioSceneWithScearioDABID(ScenarioSceneManager.DebugDABID); // do debug load
            }
        }
    }
}