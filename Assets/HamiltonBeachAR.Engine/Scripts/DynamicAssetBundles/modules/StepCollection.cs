using SINe.Collections;

using SINe.Unity.Utility;
using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Unity.OnEdit;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// Collection of steps to be included in a DynamicAssetBundle;
    /// </summary>
    public class StepCollection : XmlAsset, 
        IOnAssetLoadedFromAssetDatabase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialzied
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The StepPtrs to Steps in this StepCollection;
        /// </summary>
        public ListIterable<StepPtr> _Steps = new ListIterable<StepPtr>();

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private OnEditCollectionIterable_List<StepPtr, ListIterable<StepPtr>> _onEdit_Steps;

        public override void OnEdit_Actual(IEditorAPI editorAPI)
        {
            _onEdit_Steps.OnEdit(editorAPI); // new onEdit for the steps
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Loading
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void OnLoadedFromAssetDatabase(IEditorAPI editorAPI)
        {
            _onEdit_Steps = new OnEditCollectionIterable_List<StepPtr, ListIterable<StepPtr>>(_Steps, "Steps");
        }
    }

    public class StepCollectionPtr : XmlAssetPtr<StepCollection> { }
}