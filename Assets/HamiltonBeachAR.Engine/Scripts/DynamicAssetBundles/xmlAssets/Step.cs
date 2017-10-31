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
        public AnimationClipPtr _AnimationClipPtr = new AnimationClipPtr();

        /// <summary>
        /// The speed of the AnimationClip;
        /// </summary>
        public float _AnimationSpeed = 1f;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public override void OnEdit_Actual(IEditorAPI editorAPI)
        {
            base.OnEdit_Actual(editorAPI);

            //_Animation.OnEdit(editorAPI);
            _AnimationClipPtr = editorAPI.OnEdit.Field(_AnimationClipPtr, _AnimationClipPtr.OnEditLabel());
            _AnimationSpeed = editorAPI.OnEdit.Field(_AnimationSpeed, "Animation Speed");
        }
    }

    /// <summary>
    /// AssetPointer to Step Asset;
    /// </summary>
    public class StepPtr : XmlAssetPtr<Step> { }
}