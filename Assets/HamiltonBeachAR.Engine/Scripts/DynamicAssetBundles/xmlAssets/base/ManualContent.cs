using SINe;
using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Unity.Utility;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles
{
    /// <summary>
    /// XmlAsset with a string content to be displayed to the user;
    /// </summary>
    public abstract class ManualContent : XmlAsset
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Properties
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public string ID
        {
            get
            {
                return _Title.ToID();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Fields, Serialized
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Display title;
        /// </summary>
        public string _Title = "";

        /// <summary>
        /// Display description;
        /// </summary>
        public string _Description = "";

        ///// <summary>
        ///// The offset from the start of the manual for the language section of the manual;
        ///// </summary>
        //public int _ManaulOffset;

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // OnEdit
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        public override void OnEdit_Actual(IEditorAPI editorAPI)
        {
            _Title = editorAPI.OnEdit.Field(_Title, "Name");
            _Description = editorAPI.OnEdit.Field(_Description, "Description");
            //_ManaulOffset = editorAPI.OnEdit.Field(_ManaulOffset, "Manual Offset");
        }

        public override string ToString()
        {
            return ID;
        }
    }
}