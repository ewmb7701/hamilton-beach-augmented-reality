using SINe.Unity.Managers;

using AugmentEX.HamiltonBeachAR.Managers;

namespace AugmentEX.HamiltonBeachAR.SceneManagers
{
    /// <summary>
    /// SceneManager for the Menus/Main;
    /// </summary>
    public class MainMenuSceneManager : SceneManager<MainMenuSceneManager>
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Alive
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        override protected void Start()
        {
            base.Start();

            SceneTitleText.Component.text = "Main Menu"; // set the scene title
        }
    }
}