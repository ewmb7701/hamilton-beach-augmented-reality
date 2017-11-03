using SINe.Unity.Managers;
using SINe.Unity.SceneManagement;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for Load/Init;
    /// </summary>
    public class InitSceneManager : SceneManager<InitSceneManager>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Mono Method
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        override protected void Start()
        {
            base.Start();

            SceneManagerCore.I.LoadSceneWhenValid(ScenePID.Menu_Main); // load Menu/Main
        }
    }
}