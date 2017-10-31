using SINe.Collections;

using UnityEngine;

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
        // Methods, Serialzied
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Determine all the AnimationClips in all the StepPtrs;
        /// </summary>
        public AnimationClip[] DetAnimationClips()
        {
            AnimationClip[] animationClips = new AnimationClip[_StepPtrs.Count]; // create array

            for (int i = 0; i < _StepPtrs.Count; i++) // for all the StepPtrs
            {
                animationClips[i] = _StepPtrs[i].Asset._AnimationClipPtr.Asset; // add the clip
            }

            return animationClips; // return those clip
        }

        /// <summary>
        /// Determine all the Names in all the StepsPtrs;
        /// </summary>
        /// <returns></returns>
        public string[] DetStepNames()
        {
            string[] names = new string[_StepPtrs.Count]; // create array

            for (int i = 0; i < _StepPtrs.Count; i++) // for all the StepPtrs
            {
                names[i] = _StepPtrs[i].Asset._Name; // add the clip
            }

            return names; // return those clip
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