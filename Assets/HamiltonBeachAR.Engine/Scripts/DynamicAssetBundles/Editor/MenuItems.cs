using SINe.Unity.DynamicAssetBundleFramework;

using UnityEditor;

using SINe.Unity.DynamicAssetBundleFramework.Editor;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles.Editor
{
    /// <summary>
    /// MenuItems to create XmlAssets;
    /// </summary>
    static public class MenuItems
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // XmlAssets
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        [MenuItem(XmlAsset.MenuPrefix + "MiscInfo", false, XmlAsset.MenuPriority)]
        static public void Create_XmlAsset_MiscInfo()
        {
            DABEditorUtility.TryCreateXmlAsset<MiscInfo>();
        }

        [MenuItem(XmlAsset.MenuPrefix + "Step", false, XmlAsset.MenuPriority)]
        static public void Create_XmlAsset_Step()
        {
            DABEditorUtility.TryCreateXmlAsset<Step>();
        }

        [MenuItem(XmlAsset.MenuPrefix + "Part", false, XmlAsset.MenuPriority)]
        static public void Create_XmlAsset_Part()
        {
            DABEditorUtility.TryCreateXmlAsset<Part>();
        }

        [MenuItem(XmlAsset.MenuPrefix + "StepCollection", false, XmlAsset.MenuPriority)]
        static public void Create_XmlAsset_StepCollection()
        {
            DABEditorUtility.TryCreateXmlAsset<StepCollection>();
        }
    }
}