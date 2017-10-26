using SINe.Unity.DynamicAssetBundleFramework;
using SINe.Unity.DynamicAssetBundleFramework.Editor;

using UnityEditor;

namespace AugmentEX.HamiltonBeachAR.DynamicAssetBundles.Editor
{
    /// <summary>
    /// MenuItems to create XmlAssets;
    /// </summary>
    public static class MenuItems
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // XmlAssets
        ///////////////////////////////////////////////////////////////////////////////////////////////////

        [MenuItem(XmlAsset.MenuPrefix + "MiscInfo", false, XmlAsset.MenuPriority)]
        public static void Create_XmlAsset_MiscInfo()
        {
            DABEditorUtility.TryCreateXmlAsset<MiscInfo>();
        }

        [MenuItem(XmlAsset.MenuPrefix + "Step", false, XmlAsset.MenuPriority)]
        public static void Create_XmlAsset_Step()
        {
            DABEditorUtility.TryCreateXmlAsset<Step>();
        }

        [MenuItem(XmlAsset.MenuPrefix + "Part", false, XmlAsset.MenuPriority)]
        public static void Create_XmlAsset_Part()
        {
            DABEditorUtility.TryCreateXmlAsset<Part>();
        }
    }
}