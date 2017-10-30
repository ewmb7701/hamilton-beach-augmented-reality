using System.Collections.Generic;
using System.Xml.Serialization;

using SINe.Collections;

using SINe.Unity.Utility;
using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Unity.OnEdit;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// Asset;
    /// Collection of steps to be included in a DynamicAssetBundle;
    /// </summary>
    public class StepCollection : XmlAsset,
        IOnLoadedFromAssetDatabase
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Properties, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Return the dictionary;
        /// Key: ID;
        /// Value: StepPtr;
        /// </summary>
        [XmlIgnore]
        public Dictionary<string, StepPtr> Dictionary
        {
            get
            {
                return _stepPtrsDict;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Instance
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Dictionary for the steps;
        /// </summary>
        private Dictionary<string, StepPtr> _stepPtrsDict = new Dictionary<string, StepPtr>();

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialzied
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The StepPtrs to Steps in this StepCollection;
        /// </summary>
        public ListIterable<StepPtr> _StepPtrs = new ListIterable<StepPtr>();

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
            _onEdit_Steps = new OnEditCollectionIterable_List<StepPtr, ListIterable<StepPtr>>(_StepPtrs, "StepPtrs");
        }
    }

    public class StepCollectionPtr : XmlAssetPtr<StepCollection> { }
}