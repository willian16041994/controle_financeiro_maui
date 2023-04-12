using Microsoft.Maui.Platform;

namespace ControleFinanceiro.Utils
{
    public class CloseKeyboard
    {
        public static void HideKeyboardAndroid()
        {
            #if ANDROID
            if (Platform.CurrentActivity.CurrentFocus != null)
            {
                Platform.CurrentActivity.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
            }
            #endif
        }
    }
}
