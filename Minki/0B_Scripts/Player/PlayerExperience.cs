using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int level = 1;

    private int _curNeedExp = 100;
    private int _curExp = 0;

    public void ExpUP(int value)
    {
        _curExp += value;
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
        if (_curExp >= _curNeedExp)
        {
            ChangeLevel();
            AudioManager.Instance.PlayBGM("LevelUp");
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void CheckRemainLevelUP()
    {
        if (_curExp >= _curNeedExp)
        {
            ChangeLevel();
            GameManager.Instance.OnStatCanvas();
        }
    }

    public void ChangeLevel()
    {
        _curExp -= _curNeedExp;
        _curNeedExp = (int)(_curNeedExp * 1.5f);
        ++level;
    }
    
    public void SetExp()
    {
        GameManager.Instance.SetEXP(_curExp, _curNeedExp);
    }
}
