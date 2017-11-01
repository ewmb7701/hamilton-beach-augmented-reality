using UnityEngine;
using UnityEngine.UI;

using SINe.Unity.Managers;
using SINe.Unity.DynamicAssetBundles;
using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Collections;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for a the Debug Scene;
    /// </summary>
    public class DebugSceneManager : SceneManager<DebugSceneManager>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Inspector
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        [SerializeField]
        private Text _uiText;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Start()
        {
            base.Start();

            _uiText.text = "";

            foreach (DebugDAB dabDebug in DABCore.Inst.Database.GetAllOfType<DebugDAB>()) // for all DABDebug DynamicAsssetBundles
            {
                XmlAsset_Debug asset;
                if (dabDebug.Assets.TryOutAsset(out asset)) // got the debug component
                {
                    _uiText.text += asset.ItsAString + " ";
                }
            }
        }
    }
}