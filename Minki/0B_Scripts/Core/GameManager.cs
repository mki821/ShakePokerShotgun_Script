using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [Header("Player Info")]
    public Transform playerTrm;
    public PlayerExperience player;
    public PlayerHealth playerHealth;
    public WeaponBullet playerBulletSys;
    public Image hpFill;
    public Image expFill;
    public GameObject bakcBoard;
    public GameObject canvas;

    public RuntimeAnimatorController[] enemy;
    public GameObject SettingCanvas;

    public int enemyKill = 0;
    public string timer = "00:00";

    float maxHp = 200f;
    public int curHp = 200;

    [SerializeField] private GameObject statCanvas;


    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            player = Instance.player;
            statCanvas = Instance.statCanvas;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(hpFill) hpFill.fillAmount = 1;
        if(expFill) expFill.fillAmount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerBulletSys.interactDisable)
        {
            SettingCanvas.SetActive(!SettingCanvas.activeSelf);
        }
    }
    public void OnStatCanvas()
    {
        statCanvas.SetActive(true);
        AudioManager.Instance.PlayBGM("LevelUp");
    }
    public void SetHP(int nowHp)
    {
        hpFill.fillAmount = nowHp / maxHp;
    }
    public void SetEXP(int curExp, int needExp)
    {
        expFill.fillAmount = curExp / (float)needExp;
    }

    public void SaveTimer(string str) {
        timer = str;
    }
}
