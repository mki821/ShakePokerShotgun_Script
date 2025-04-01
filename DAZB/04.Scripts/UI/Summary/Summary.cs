using UnityEngine;
using UnityEngine.UI;

public class Summary : MonoBehaviour
{
    public GameObject[] pages;
    public Button[] pageChangeButtons; // 1: left 2: right
    public int currentPage;

    private void Start() {
        SetPage(currentPage);
    }

    public void RightPage() {
        currentPage++;
        SetPage(currentPage);
    }

    public void LeftPage() {
        currentPage--;
        SetPage(currentPage);
    }

    public void SetPage(int idx) {
        foreach (GameObject iter in pages) {
            iter.SetActive(false);
        }
        pages[idx].SetActive(true);
        switch (idx) {
            case 0: {
                pageChangeButtons[0].gameObject.SetActive(false);
                pageChangeButtons[1].gameObject.SetActive(true);
                break;
            }
            case 1: {
                pageChangeButtons[0].gameObject.SetActive(true);
                pageChangeButtons[1].gameObject.SetActive(true);
                break;
            }
            case 2: {
                pageChangeButtons[0].gameObject.SetActive(true);
                pageChangeButtons[1].gameObject.SetActive(false);
                break;
            }
        }
    }
}
