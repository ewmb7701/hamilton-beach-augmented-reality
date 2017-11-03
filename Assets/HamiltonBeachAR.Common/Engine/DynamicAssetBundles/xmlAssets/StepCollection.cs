using System.Linq;

using SINe.Collections;

using UnityEngine;

using SINe.UnityEngine.Utility;
using SINe.UnityEngine.DynamicAssetBundleFramework;
using SINe.UnityEngine.OnEdit;

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
        // Methods, Public
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Determine all the AnimationClips in all the StepPtrs;
        /// </summary>
        public AnimationClip[] DetAnimationClips()
        {
            //Debug.LogFormat("Detreminign. StepPtrs null? {0} element 0 null? {1} Asset null? {2}", _StepPtrs == null, _StepPtrs[0] == null, _StepPtrs[0].Asset == null);
            return (from stepPtr in _StepPtrs select stepPtr.Asset._AnimationClipPtr.Asset).ToArray();
        }

        /// <summary>
        /// Determine all the Names in all the StepsPtrs;
        /// </summary>
        /// <returns></returns>
        public string[] DetStepIDS()
        {
            return (from stepPtr in _StepPtrs select stepPtr.Asset.ID).ToArray();
        }

        /// <summary>
        /// Get a step at the index;
        /// </summary>
        public Step GetStep(int index)
        {
            return _StepPtrs[index].Asset;
        }

        //public Tuple<string, AnimationClip>[] DetAnimationClipPlayables()
        //{
        //    Tuple<string, AnimationClip>[] animationClips = new Tuple<string, AnimationClip>[_StepPtrs.Count]; // create array

        //    for (int i = 0; i < _StepPtrs.Count; i++) // for all the StepPtrs
        //    { 
        //        animationClips[i] = new Tuple<string, AnimationClip>(_StepPtrs[i].Asset._Name, _StepPtrs[i].Asset._AnimationClipPtr.Asset); // add the clip
        //    }

        //    return animationClips; // return those clip
        //}

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialzied
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// The StepPtrs to Steps in this StepCollection;
        /// </summary>
        public ListEX<StepPtr> _StepPtrs = new ListEX<StepPtr>(); 

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        private OnEditCollectionIterable_List<StepPtr, ListEX<StepPtr>> _onEdit_Steps;

        override public void OnEdit_Actual(IEditorAPI editorAPI)
        {
            _onEdit_Steps.OnEdit(editorAPI); // new onEdit for the steps
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Loading
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public void OnLoadedFromAssetDatabase(IEditorAPI editorAPI)
        {
            _onEdit_Steps = new OnEditCollectionIterable_List<StepPtr, ListEX<StepPtr>>(_StepPtrs, "StepPtrs");
        }
    }

    public class StepCollectionPtr : XmlAssetPtr<StepCollection> { }
}