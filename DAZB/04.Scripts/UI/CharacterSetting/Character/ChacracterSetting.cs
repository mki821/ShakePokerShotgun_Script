using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ChacracterSetting : MonoBehaviour
{  
    public void HoverMouse(GameObject obj) {
        obj.GetComponent<RectTransform>().DOScale(new Vector3(0.85f, 0.85f, 0.85f), 0.15f);
    }

    public void UnHoverMouse(GameObject obj) {
        obj.GetComponent<RectTransform>().DOScale(new Vector3(0.7f + 0.3f, 0.7f + 0.3f, 0.7f + 0.3f), 0.15f);
    }

    public void NextScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
