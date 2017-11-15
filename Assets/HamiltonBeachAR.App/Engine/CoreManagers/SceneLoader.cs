using SINeDUSTRIES.UnityEngine.DynamicAssetsFramework;
using SINeDUSTRIES.UnityEngine.Managers;
using SINeDUSTRIES.UnityEngine.SceneManagement;
using SINeDUSTRIES.UnityEngine.SceneManagers;

namespace RealitEX.HamiltonBeachAR.SceneManagement
{
    /// <summary>
    /// SceneLoader for HamBam;
    /// </summary>
    public class SceneLoader : CoreManager<SceneLoader>
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Public
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Load the Scenario scene;
        /// </summary>
        /// <param name="dabID">The DARID of the ScenarioDAR to load;</param>
        public void LoadScenario(string dabID)
        {
            if (ARScenarioSceneManager.TryLoadScenarioSceneWithScearioDARID(dabID))
            {
                global::UnityEngine.Debug.LogFormat("Loaded Scenario with ScenarioDAR {0}", dabID);
            }
            else
            {
                foreach (DynamicAssetRoot dar in DynamicAssetRoots.I.Database)
                {
                    global::UnityEngine.Debug.LogFormat("DAR in Database {0}", dar.ID);
                }
                throw new System.Exception("Could not load Scenario with ScenarioDAR" + dabID);
            }
        }

        /// <summary>
        /// Load the Main Menu scene;
        /// </summary>
        public void LoadMainMenu()
        {
            Scenes.I.LoadSceneWhenValid(ScenePID.Menu_Main); // load the main menu
        }

        //protected override void Update()
        //{
        //    base.Update();

        //    Debug.LogFormat("Paths ProductSetting Update; PersistentData path {0}", Application.persistentDataPath);
        //}

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Core Handlers
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public override void HandlersRegister(object handler) { }

        public override void HandlersUnregister(object handler) { }
    }
}