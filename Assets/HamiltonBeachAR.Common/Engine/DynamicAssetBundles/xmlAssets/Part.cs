using SINe.UnityEngine.DynamicAssetBundleFramework;
using SINe.UnityEngine.Utility;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// XmlAsset for a Part;
    /// </summary>
    public class Part : ManualContent
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialized
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Associated url for this part;
        /// </summary>
        public string _URL;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        override public void OnEdit_Actual(IEditorAPI editorAPI)
        {
            //_PartID = editorAPI.OnEdit.Popup_Enum<PartID>(_PartID, "Part ID"); // part id

            base.OnEdit_Actual(editorAPI); // content and title

            _URL = editorAPI.OnEdit.Field(_URL, "URL"); // url
        }
    }

    /// <summary>
    /// AssetPointer to an Part XmlAsset;
    /// </summary>
    public class PartPtr : XmlAssetPtr<Part> { }
}