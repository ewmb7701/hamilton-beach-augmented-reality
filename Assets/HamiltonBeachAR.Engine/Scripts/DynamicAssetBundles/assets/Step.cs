using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Unity.OnEdit;
using SINe.Unity.Utility;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// XmlAsset for a step;
    /// </summary>
    public class Step : ManualContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialzied
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Pointer to AnimationClip to play;
        /// </summary>
        public AnimationClipPtr _Animation = new AnimationClipPtr();

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public override void OnEdit_Actual(IEditorAPI editorAPI)
        {
            base.OnEdit_Actual(editorAPI);

            //_Animation.OnEdit(editorAPI);
            _Animation = editorAPI.OnEdit.Field(_Animation, _Animation.OnEditLabel());
        }
    }

    /// <summary>
    /// AssetPointer to Step Asset;
    /// </summary>
    public class StepPtr : AssetPointer_XmlAsset<Step> { }
}