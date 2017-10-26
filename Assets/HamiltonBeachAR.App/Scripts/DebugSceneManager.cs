using UnityEngine;
using UnityEngine.UI;

using SINe.Unity.Managers;
using SINe.Unity.DynamicAssetBundles;
using SINe.Unity.DynamicAssetBundleFramework;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for a the Debug Scene;
    /// </summary>
    public class DebugSceneManager : Manager_Scene
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

            foreach (DABDebug dabDebug in DABCore.Inst.Database.GetAllOfType<DABDebug>()) // for all DABDebug DynamicAsssetBundles
            {
                DABComponent_Debug debugComponent;
                if(dabDebug.Components.TryGetValue(out debugComponent)) // got the debug component
                {
                    _uiText.text += debugComponent._DebugAssetPointer.Asset.ItsAString + " ";
                }

                DABComponent_Debug2 debugComponent2;
                if (dabDebug.Components.TryGetValue(out debugComponent2)) // got the debug component 2
                {
                    _uiText.text += debugComponent2._DebugAssetPointer.Asset.ItsAString + " ";
                }
            }
        }
    }
}