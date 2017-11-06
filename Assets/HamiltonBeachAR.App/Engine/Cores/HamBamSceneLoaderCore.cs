using SINe.UnityEngine.Managers;

using AugmentEX.HamiltonBeachAR.SceneManagers;

namespace AugmentEX.HamiltonBeachAR.SceneManagement
{
    /// <summary>
    /// SceneLoader for HamBam;
    /// </summary>
    public class HamBamSceneLoaderCore : Core<HamBamSceneLoaderCore>
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Public
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Load the Scenario scene;
        /// </summary>
        /// <param name="dabID">The DABID of the ScenarioDAB to load;</param>
        public void LoadScenario(string dabID)
        {
            ScenarioSceneManager.TryLoadScenarioSceneWithScearioDABID(dabID);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Core Handlers
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void HandlersRegister(object handler) { }

        public override void HandlersUnregister(object handler) { }
    }
}