using UnityEngine;

using SINe.Unity.Managers;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for the Menus/Main;
    /// </summary>
    public class MainMenuSceneManager : SceneManager<MainMenuSceneManager>
    {
        protected override void Start()
        {
            base.Start();

            Debug.LogWarningFormat("Loading Debug ScenarioDAB...");
            ScenarioSceneManager.TryLoadScenarioSceneWithScearioDABID(ScenarioSceneManager.DebugDABID); // do debug load
        }
    }
}