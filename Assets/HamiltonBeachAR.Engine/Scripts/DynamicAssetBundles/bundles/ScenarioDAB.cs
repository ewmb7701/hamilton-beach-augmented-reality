using SINe.Types;

using SINe.Unity.DynamicAssetBundleFramework;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// DynamicAssetBundle for Scenarios;
    /// </summary>
    [TypeTag("Scenario")]
    [RequiredAssetsPointers(typeof(StepCollectionPtr))]
    public class ScenarioDAB : DynamicAssetBundle { }
}