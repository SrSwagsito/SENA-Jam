using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject UICanvas;

    [SerializeField] List<BaseUiPanel> screens = new List<BaseUiPanel>();

    [SerializeField] List<BaseUiPanel> overlays = new List<BaseUiPanel>();

    BaseUiPanel currenScreen;

    private readonly List<BaseUiPanel> activeOverlays = new List<BaseUiPanel>();

    bool isTransitioning;

    public void ShowScreen(string screenName)
    {
        BaseUiPanel panel = screens.Find(i => i.PanelName == screenName);

        if(panel == null)
        {
            Debug.LogError("No se encontro " + screenName); return;
        }

        if(panel == currenScreen)
        {
            Debug.Log("La pantalla" + screenName + "ya esta activa"); return;
        }

        if (!isTransitioning)
        {
            StartCoroutine(ShowScreenCoroutine(panel));
        }
    }

    public IEnumerator ShowScreenCoroutine(BaseUiPanel nextScreen)
    {
        isTransitioning = true;

        if (currenScreen != null)
        {
            yield return currenScreen.HideCoroutine();
            currenScreen.gameObject.SetActive(false);
        }

        currenScreen = nextScreen;
        currenScreen.gameObject.SetActive(true);
        currenScreen.Show();

        yield return currenScreen.ShowCoroutine(false);
        isTransitioning = false;

    }

    public void Hide()
    {
        if (isTransitioning) return;
        StartCoroutine(HideScreenCoroutine());
    }

    public IEnumerator HideScreenCoroutine()
    {
        if(currenScreen == null) yield break;
        isTransitioning = true;

        BaseUiPanel hideScreen = currenScreen;
        currenScreen = null;

        yield return currenScreen.HideCoroutine();
        hideScreen.gameObject.SetActive(false);
        isTransitioning = false;
    }

    public void ShowOverlay(string overlayName)
    {
        BaseUiPanel overlay = overlays.Find(i => i.PanelName == overlayName);

        if (overlayName == null)
        {
            Debug.LogError("El overlay " + overlayName + " no existe"); return; 
        }

        if (activeOverlays.Contains(overlay)) return;
        activeOverlays.Add(overlay);
        overlay.gameObject.SetActive(true);
        overlay.Show();

        StartCoroutine(overlay.ShowCoroutine(false));
    }

    public void HideOverlay(string overlayName)
    {
        BaseUiPanel overlay = activeOverlays.Find(i => i.PanelName == overlayName);

        if (overlay == null)
        {
            Debug.LogError("No se encontro " + overlayName); return;
        }

        if (overlay == currenScreen)
        {
            Debug.Log("La overlay" + overlayName + "ya esta activa"); return;
        }

        if(overlay != null)
        {
            StartCoroutine(CloseOverlayCoroutine(overlay));
        }
    }

    public IEnumerator CloseOverlayCoroutine(BaseUiPanel overlay)
    {
        activeOverlays.Remove(overlay);
        yield return overlay.HideCoroutine();

        overlay.gameObject.SetActive(false);
    }

    public void CloseAllOverlays()
    {
        foreach (BaseUiPanel overlay in activeOverlays.ToArray())
        {
            if (overlay)
            {
                StartCoroutine(CloseOverlayCoroutine(overlay)); 
            }
        }
    }


}
