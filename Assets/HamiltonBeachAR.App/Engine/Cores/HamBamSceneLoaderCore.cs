using SINeDUSTRIES.UnityEngine.DynamicAssetBundleFramework;
using SINeDUSTRIES.UnityEngine.Managers;
using SINeDUSTRIES.UnityEngine.SceneManagement;
using SINeDUSTRIES.UnityEngine.SceneManagers;

namespace RealitEX.HamiltonBeachAR.SceneManagement
{
    /// <summary>
    /// SceneLoader for HamBam;
    /// </summary>
    public class HamBamSceneLoaderCore : CoreManager<HamBamSceneLoaderCore>
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
            if (ARScenarioSceneManager.TryLoadScenarioSceneWithScearioDABID(dabID))
            {
                global::UnityEngine.Debug.LogFormat("Loaded Scenario with ScenarioDAB {0}", dabID);
            }
            else
            {
                foreach (DynamicAssetBundle dab in DABCore.I.Database)
                {
                    global::UnityEngine.Debug.LogFormat("DAB in Database {0}", dab.DABID);
                }
                throw new System.Exception("Could not load Scenario with ScenarioDAB" + dabID);
            }
        }

        /// <summary>
        /// Load the Main Menu scene;
        /// </summary>
        public void LoadMainMenu()
        {
            SceneCore.I.LoadSceneWhenValid(ScenePID.Menu_Main); // load the main menu
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