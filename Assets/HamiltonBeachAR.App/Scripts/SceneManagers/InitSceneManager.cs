using SINe.Unity.Managers;
using SINe.Unity.SceneManaging;

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

        protected override void Start()
        {
            base.Start();

            SceneManagerCore.Inst.LoadSceneWhenValid(ScenePID.Menu_Main); // load Menu/Main
        }
    }
}