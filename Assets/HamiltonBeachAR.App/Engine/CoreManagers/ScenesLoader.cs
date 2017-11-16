using SINeDUSTRIES.UnityEngine.DynamicAssetsFramework;
using SINeDUSTRIES.UnityEngine.Scenes;

using UnityEngine;

namespace RealitEX.HamiltonBeachAR.SceneManagement
{
    /// <summary>
    /// SceneLoader for HamBam;
    /// </summary>
    public class ScenesLoader : MonoBehaviour
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
            if (ScenarioScene.TryLoadScenarioSceneWithScearioDARID(dabID))
            {
                global::UnityEngine.Debug.LogFormat("Loaded Scenario with ScenarioDAR {0}", dabID);
            }
            else
            {
                foreach (DynamicAssetRoot dar in DynamicAssetsManager.I.Database)
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
            ScenesManager.I.LoadSceneWhenValid(ScenePID.Menu_Main); // load the main menu
        }

        /// <summary>
        /// Load Parts scene;
        /// </summary>
        public void LoadParts()
        {
            ScenesManager.I.LoadSceneWhenValid(ScenePID.Parts); // load parts
        }
    }
}