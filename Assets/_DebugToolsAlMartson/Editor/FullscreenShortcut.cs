using UnityEditor;
using UnityEngine;

/// <summary>
/// Is there a keyboard shortcut to maximize the Game window in Unity in Play Mode? <br >
/// [ https://stackoverflow.com/questions/54431635/is-there-a-keyboard-shortcut-to-maximize-the-game-window-in-unity-in-play-mode#:~:text=Ctrl%20%2B%20Space%20maximizes%20most%20windows,2018%20when%20in%20edit%20mode. ]
/// 
/// </summary>
[InitializeOnLoad]
static class FullscreenShortcut
{

#if UNITY_EDITOR

    static FullscreenShortcut()
    {
        EditorApplication.update += Update;
    }


    static void Update()
    {
        if (EditorApplication.isPlaying && ShouldToggleMaximize())
        {
            EditorWindow.focusedWindow.maximized = !EditorWindow.focusedWindow.maximized;
        }
    }

    /// <summary>
    /// This is the Keyboard Shortcut.
    /// </summary>
    /// <returns></returns>
    private static bool ShouldToggleMaximize()
    {
        return Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Space);
    }

#endif

}