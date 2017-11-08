/// Credit Feaver1968 
/// Sourced from - http://forum.unity3d.com/threads/scroll-to-the-bottom-of-a-scrollrect-in-code.310919/

namespace UnityEngine.UI.Extensions
{
    static public class ScrollRectExtensions
    {
        static public void ScrollToTop(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        static public void ScrollToBottom(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}