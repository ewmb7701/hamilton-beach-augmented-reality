using UnityEngine.SceneManagement;

using SINe.UnityEngine.Managers;
using SINe.UnityEngine.SceneManagement;
using SINe.UnityEngine.UI;

namespace AugmentEX.HamiltonBeachAR.UI
{
    /// <summary>
    /// SingletonComponent for the SideMenuPositioner;
    /// </summary>
    public class SideMenuPositioner : SingletonComponent<SideMenuPositioner, RectTransformPositionerCycler>,
        IHandler_Event_Scene<IOnCoda>
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Methods, Core Handlers
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Handler for when the current scene enter the coda;
        /// </summary>
        void IHandler_Event_Scene<IOnCoda>.OnEvent(ScenePID scene, LoadSceneMode mode)
        {
            Component.LerpPos0(); // return to the 0th position
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Alive
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        virtual protected void Start()
        {
            SceneManagerCore.I.HandlersRegister(this); // register to scene core handlers
        }

        virtual protected void OnDestroy()
        {
            SceneManagerCore.I.HandlersUnregister(this); // unregister from Scene core handlers
        }
    }
}