using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSkillHandlers : MonoBehaviour
{
    [SerializeField] private GameObject _fireEffectPrefab;
    WeaponBullet _bulletMain;
    private void Awake() {
        _bulletMain = GetComponent<WeaponBullet>();

        _bulletMain.eventListener[Ranking.HIGHCARD] = DefaultFire;
        _bulletMain.eventListener[Ranking.ONEPAIR] = DefaultFire;
        _bulletMain.eventListener[Ranking.TWOPAIR] = DefaultFire;
        _bulletMain.eventListener[Ranking.STRAIGHT] = Straight;
        _bulletMain.eventListener[Ranking.TRIPLE] = Triple;
        _bulletMain.eventListener[Ranking.BACKSTRAIGHT] = Backstraight;
        _bulletMain.eventListener[Ranking.MOUNTAIN] = Mountain;
        _bulletMain.eventListener[Ranking.FOURCARD] = Fourcard;
        _bulletMain.eventListener[Ranking.FULLHOUSE] = Fullhouse;
        _bulletMain.eventListener[Ranking.FLUSH] = Flush;
        _bulletMain.eventListener[Ranking.STRAIGHTFLUSH] = StraightFlush;
        _bulletMain.eventListener[Ranking.ROYALSTRAIGHTFLUSH] = RoyalBackStraightFlush;
        _bulletMain.eventListener[Ranking.BACKSTRAIGHTFLUSH] = BackStraightFlush;
    }

    private void OnDestroy() {
        _bulletMain.eventListener.Remove(Ranking.HIGHCARD);
        _bulletMain.eventListener.Remove(Ranking.ONEPAIR);
        _bulletMain.eventListener.Remove(Ranking.TWOPAIR);
        _bulletMain.eventListener.Remove(Ranking.STRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.TRIPLE);
        _bulletMain.eventListener.Remove(Ranking.BACKSTRAIGHT);
        _bulletMain.eventListener.Remove(Ranking.MOUNTAIN);
        _bulletMain.eventListener.Remove(Ranking.FOURCARD);
        _bulletMain.eventListener.Remove(Ranking.FULLHOUSE);
        _bulletMain.eventListener.Remove(Ranking.FLUSH);
        _bulletMain.eventListener.Remove(Ranking.STRAIGHTFLUSH);
        _bulletMain.eventListener.Remove(Ranking.ROYALSTRAIGHTFLUSH);
        _bulletMain.eventListener.Remove(Ranking.BACKSTRAIGHTFLUSH);
    }


    void DefaultFire(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // TEST 총알
        for (int i = -2, k = 0; i < 3; i++, k ++)
        {
            bullets[k].transform.position = start;
            bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (5 * i), Vector3.forward);
        }
    }

    void Straight(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // 순서대로 정렬
        bullets = bullets.OrderBy(n => {
            string name = n.GetComponentInChildren<SpriteRenderer>().sprite.name;
            string number = name.Substring(name.IndexOf("_") + 1);
            if (number == "A") number = "1";
            return int.Parse(number);
        }).ToArray();

        int i = 0;
        foreach (var bullet in bullets)
        {
            bullet.SetActive(false);
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
            Wait(() => {
                bullet.SetActive(true);
                bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                bullet.transform.position = start;
                // bullet.transform.position += bullet.transform.right * i;
            }, i * .03f);
            i++;
        }
    }

    void Triple(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();
        int fireDamage = _bulletMain.GetDamange(45);

        for (int i = -2, k = 0; i < 3; i++, k ++)
        {
            bullets[k].transform.position = start;
            bullets[k].transform.rotation = Quaternion.AngleAxis(angle + (5 * i), Vector3.forward);

            // 화염디버프 적용 ㄱㄱ
            int idx = k;
            var bulletSys = bullets[k].GetComponent<CardWeaponBullet>();
            bulletSys.OnCallback += (Collider2D collider) => {
                if (collider.TryGetComponent<DebuffFire>(out var debuffSys_)) Destroy(debuffSys_); // 기존꺼 삭제

                var debuffSys = collider.AddComponent<DebuffFire>();
                debuffSys.damage = fireDamage;
                debuffSys.effectEntity = _fireEffectPrefab;
                return false;
            };
        }
    }
    void Backstraight(Vector2 start, float angle) {
        var bullets = _bulletMain.CreateBullets();

        // 순서대로 정렬
        bullets = bullets.OrderBy(n => {
            string name = n.GetComponentInChildren<SpriteRenderer>().sprite.name;
            string number = name.Substring(name.IndexOf("_") + 1);
            if (number == "A") number = "1";
            return int.Parse(number);
        }).ToArray();

        int i = 0;
        foreach (var bullet in bullets)
        {
            bullet.SetActive(false);
            bullet.GetComponentInChildren<SpriteRenderer>().sortingOrder = i;
            Wait(() => {
                bullet.SetActive(true);
                bullet.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                bullet.transform.position = start;
                // bullet.transform.position += bullet.transform.right * i;
            }, i * .03f);
            i++;
        }
    }
    void Mountain(Vector2 start, float angle) {
        float?[,] spawnCoords = new float?[5, 5] {
         {null, null, 0,null,null},
            {-1, null,0,null,1},
            {-1, null,0,null,1},
            {-1, null,0,null,1},
            {-1,  -0.5f, 0,  0.5f,  1},
        };

        Queue<GameObject> cards = new();
        for (int i = 0; i < 3; i++)
        {
            foreach (var card in _bulletMain.CreateBullets())
            {
                card.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                card.SetActive(false);

                cards.Enqueue(card);
            }
        }

        print("------ cards");
        print(cards.Count);
        
        for (int i = 0; i < spawnCoords.GetLength(0); i++)
        {
            for (int k = 0; k < spawnCoords.GetLength(1); k++) {
                var x = spawnCoords[i, k];
                if (x == null) continue;

                var entity = cards.Dequeue();
                Wait(() => {
                    entity.SetActive(true);
                    entity.transform.position = start;
                    entity.transform.position += entity.transform.up * x.Value;
                }, i * .01f);
            }
        }
    }

    void Fourcard(Vector2 start, float angle) {
        for (int domi = 0; domi < 4; domi++)
        {
            var bullets = _bulletMain.CreateBullets();

            for (int i = -2, k = 0; i < 2; i++, k ++)
            {
                bullets[k].SetActive(false);
                var idx = i;
                var _k = k;
                Wait(() => {
                    bullets[_k].SetActive(true);
                    bullets[_k].transform.position = start;
                    bullets[_k].transform.rotation = Quaternion.AngleAxis(angle + (5 * idx), Vector3.forward);
                }, domi * .05f);
            }
            Destroy(bullets[4]);
        }
    }
    
    void Fullhouse(Vector2 start, float angle) {
        List<GameObject> bullets = new();

        for (int i = 0; i < 2; i++)
            foreach (var item in _bulletMain.CreateBullets())
                bullets.Add(item);

        for (float i = -5, k = 0; i <= 5; i+=1.1f, k ++)
        {
            bullets[(int)k].transform.position = start;
            bullets[(int)k].transform.rotation = Quaternion.AngleAxis(angle + (2.5f * i), Vector3.forward);

            int idx = (int)k;
            var bulletSys = bullets[idx].GetComponent<CardWeaponBullet>();
            // bulletSys.damage  = 0;
            bulletSys.OnCallback += (Collider2D collider) => {
                DebuffFreeze[] domi = collider.GetComponents<DebuffFreeze>();
                print(domi.Length);
                foreach(var item in domi)
                {
                    Destroy(item);
                    print("기존거 삭제");
                }
                collider.AddComponent<DebuffFreeze>();
                print("컴포넌트 붙임");

                return true;
            };
        }
    }
    
    [SerializeField] GameObject[] shapeGroups;
    void Flush(Vector2 start, float angle) {
        CardShape shape = CheckCard.instance.playerCards[0].cardShape;
        int saveDamage = _bulletMain.GetDamange();
        var bullets = _bulletMain.CreateBullets();
        for (int i = 1; i < 5; i++)
            Destroy(bullets[i]);

        bullets[0].transform.position = start;
        bullets[0].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        CheckCard.instance.rankingInfo.ranking = Ranking.NONE;
        bullets[0].GetComponent<CardWeaponBullet>().damage = _bulletMain.GetDamange();
        CheckCard.instance.rankingInfo.ranking = Ranking.FLUSH;

        bullets[0].GetComponent<CardWeaponBullet>().OnCallback += CreateFlushCardHandler(shape, bullets[0].transform.position, saveDamage, bullets[0]);
    }
    
    void StraightFlush(Vector2 start, float angle) {
        CardShape shape = CheckCard.instance.playerCards[0].cardShape;

        var bullets = _bulletMain.CreateBullets();

        // 데미지 구함
        CheckCard.instance.rankingInfo.ranking = Ranking.NONE;
        int defaultDamage = _bulletMain.GetDamange();
        CheckCard.instance.rankingInfo.ranking = Ranking.STRAIGHTFLUSH;
        int editDamage = _bulletMain.GetDamange();

        for (int i = 0; i < 4; i++)
        {
            var bulletSys = bullets[i].GetComponent<CardWeaponBullet>();
            bulletSys.damage = defaultDamage;
            bulletSys.OnCallback += CreateFlushCardHandler(shape, bullets[i].transform.position, editDamage, bullets[i]);

            bullets[i].transform.position = start;
            bullets[i].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            angle += 90;
        }
        Destroy(bullets[4]);
        
    }
    Func<Collider2D, bool> CreateFlushCardHandler(CardShape shape, Vector3 start, int damage, GameObject myEntity) {
        // start가 콜백 이후에도 메모리 주소복사가 아니라 좌표가 업데이트 되지 않음
        return (Collider2D other) => {
            var parent = Instantiate(shapeGroups[(int)shape]);
            parent.transform.position = other.ClosestPoint(myEntity.transform.position);
            
            int i;
            for (i = 0; i < parent.transform.childCount; i++)
            {
                var card = parent.transform.GetChild(i);
                var bulletSys = card.AddComponent<CardWeaponBullet>();
                
                bulletSys.speed = 10;
                bulletSys.damage = damage;
                bulletSys.customDir = (card.position - parent.transform.position).normalized;
                bulletSys.standEntity = transform.root;
                // bulletSys.OnCallback += (Collider2D other) => {
                //     return false; // 삭제 방지
                // };
            }

            for (i = 0; i < 25; i++) {
                parent.transform.GetChild(0).transform.SetParent(null, true);
            }

            Destroy(parent.gameObject);
            Destroy(myEntity);
            // parent.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return true;
        };
    }

    void BackStraightFlush(Vector2 start, float angle) {
        StartCoroutine(_BackStraightFlush());
    }
    void RoyalBackStraightFlush(Vector2 start, float angle) {
        StartCoroutine(_RoyalBackStraightFlush());
    }

    // test
    private void Start() {
        // StartCoroutine(_BackStraightFlush());
    }

    [SerializeField] GameObject clockTemplate;
    IEnumerator _BackStraightFlush() {
        var saveShape = CheckCard.instance.playerCards[0].cardShape;
        CheckCard.instance.rankingInfo.ranking = Ranking.NONE;
        var saveDamage_default = _bulletMain.GetDamange();
        CheckCard.instance.rankingInfo.ranking = Ranking.BACKSTRAIGHTFLUSH;
        var saveDamage = _bulletMain.GetDamange();

        // 총알 미리 생성
        var bulletList = new Queue<GameObject>();
        for (int i = 0; i < 2; i++)
            foreach (var item in _bulletMain.CreateBullets())
            {
                item.SetActive(false);
                item.GetComponent<CardWeaponBullet>().enabled = false;
                item.transform.position = transform.root.position;
                bulletList.Enqueue(item);
            }

        for (int i = 6; i < bulletList.Count; i++)
            Destroy(bulletList.Dequeue());

        _bulletMain.fireDisable = _bulletMain.interactDisable = true;

        while (Time.timeScale > 0) {
            yield return null;
            Time.timeScale = Mathf.Max(Time.timeScale - Time.unscaledDeltaTime, 0);
        }

        // 초기화
        var clock = Instantiate(clockTemplate, transform.root).transform;
        var texts = clock.Find("Texts");
        clock.transform.localScale = Vector3.one * 0.7f;
        clock.transform.position = transform.root.position;

        for (int i = 0; i < texts.childCount; i++)
        {
            var redner = texts.GetChild(i).GetComponent<SpriteRenderer>();
            redner.material = new(redner.material);
            redner.color *= new Color(1,1,1,0);
        }
        
        var roundCenter = clock.Find("roundCenter");
        var roundCenter_render = roundCenter.GetComponent<SpriteRenderer>();
        roundCenter_render.material = new Material(roundCenter_render.material);
        roundCenter_render.material.SetFloat("_FillAmount", 0);

        var roundCenter2 = clock.Find("roundCenter2");
        var roundCenter2_render = roundCenter2.GetComponent<SpriteRenderer>();
        roundCenter2_render.material = new Material(roundCenter2_render.material);
        roundCenter2_render.material.SetFloat("_FillAmount", 0);

        var roundCenter3 = clock.Find("roundCenter3");
        var roundCenter3_render = roundCenter3.GetComponent<SpriteRenderer>();
        roundCenter3_render.material = new Material(roundCenter3_render.material);
        roundCenter3_render.material.SetFloat("_FillAmount", 0);

        DOTween.To(() => 0f, number => roundCenter_render.material.SetFloat("_FillAmount", number), 1f, .7f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
        DOTween.To(() => 0f, number => roundCenter2_render.material.SetFloat("_FillAmount", number), 1f, .8f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
        DOTween.To(() => 0f, number => roundCenter3_render.material.SetFloat("_FillAmount", number), 1f, .9f).SetEase(Ease.OutQuad).SetUpdate(true).Play();

        for (int i = 0; i < texts.childCount; i++) {
            texts.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
        }

        yield return new WaitForSecondsRealtime(1);

        HashSet<int> activeNums = new() {0, 4, 8};
        var cardFirstInfo = clock.Find("CardFirstPos");

        void setCards() {
            foreach (int idx in activeNums)
            {
                var card = bulletList.Dequeue();
                var bullet = card.GetComponent<CardWeaponBullet>();
                card.transform.position = clock.position;

                bullet.damage = saveDamage_default;
                bullet.OnCallback += CreateFlushCardHandler(saveShape, card.transform.position, saveDamage, card);
                card.SetActive(true);
                
                RotateAround(cardFirstInfo.position, Quaternion.Euler(new(0,0,90)), clock.position, Vector3.back, idx * 30, out var endPos, out var endRotate);

                card.transform.DOMove(endPos, .5f).SetEase(Ease.OutQuad).SetUpdate(true);
                card.transform.DORotateQuaternion(endRotate, .5f).SetEase(Ease.OutQuad).SetUpdate(true).OnComplete(() => {
                    card.GetComponent<CardWeaponBullet>().enabled = true;
                });
            }
        }

        setCards();
        
        yield return new WaitForSecondsRealtime(.5f);

        for (int i = 0; i < texts.childCount; i++) {
            var render = texts.GetChild(i).GetComponent<SpriteRenderer>();
            if (activeNums.Contains(i)) {
                DOTween.To(() => 4f, number => render.material.SetColor("_Color", new Color(1*Mathf.Pow(2,number),1*Mathf.Pow(2,number),1*Mathf.Pow(2,number))), 2f, 1f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
            } else {
                render.DOFade(.5f, 1f).SetUpdate(true);
            }
        }

        // 출발~
        CamManager.StartShake(2, .5f);
        Time.timeScale = 1;

        while (Time.timeScale > 0) {
            yield return null;
            Time.timeScale = Mathf.Max(Time.timeScale - (Time.unscaledDeltaTime / 0.5f), 0);
        }
        // yield return new WaitForSecondsRealtime(.5f);

        activeNums = new() { 2, 6, 10 };
        setCards();

        yield return new WaitForSecondsRealtime(.5f);

        for (int i = 0; i < texts.childCount; i++) {
            var render = texts.GetChild(i).GetComponent<SpriteRenderer>();
            if (activeNums.Contains(i)) {
                render.DOFade(1, .3f).SetUpdate(true);
                DOTween.To(() => 4f, number => render.material.SetColor("_Color", new Color(1*Mathf.Pow(2,number),1*Mathf.Pow(2,number),1*Mathf.Pow(2,number))), 2f, .3f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
            } else {
                DOTween.To(() => render.material.GetColor("_Color").a, number => render.material.SetColor("_Color", new Color(1*Mathf.Pow(2,number),1*Mathf.Pow(2,number),1*Mathf.Pow(2,number))), 1f, .3f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
                render.DOFade(.5f, .3f).SetUpdate(true);
            }
        }

        Time.timeScale = 1;
        CamManager.StartShake(2, 1.5f);

        yield return new WaitForSecondsRealtime(.3f);

        foreach (var item in clock.GetComponentsInChildren<SpriteRenderer>())
        {
            item.DOFade(0, .3f);
        }
        
        Destroy(clock.gameObject, .31f);
        _bulletMain.fireDisable = _bulletMain.interactDisable = false;
    }
    IEnumerator _RoyalBackStraightFlush() {
        var saveShape = CheckCard.instance.playerCards[0].cardShape;
        CheckCard.instance.rankingInfo.ranking = Ranking.NONE;
        var saveDamage_default = _bulletMain.GetDamange();
        CheckCard.instance.rankingInfo.ranking = Ranking.ROYALSTRAIGHTFLUSH;
        var saveDamage = _bulletMain.GetDamange();

        _bulletMain.fireDisable = _bulletMain.interactDisable = true;
        while (Time.timeScale > 0) {
            yield return null;
            Time.timeScale = Mathf.Max(Time.timeScale - Time.unscaledDeltaTime, 0);
        }

        // 초기화
        var clock = Instantiate(clockTemplate, transform.root).transform;
        var texts = clock.Find("Texts");
        clock.transform.position = transform.root.position;

        var textMat = texts.GetChild(0).GetComponent<SpriteRenderer>().material;
        for (int i = 0; i < texts.childCount; i++)
        {
            var redner = texts.GetChild(i).GetComponent<SpriteRenderer>();
            redner.material = textMat;
            redner.color *= new Color(1,1,1,0);
        }

        var roundCenter = clock.Find("roundCenter");
        var roundCenter_render = roundCenter.GetComponent<SpriteRenderer>();
        roundCenter_render.material = new Material(roundCenter_render.material);
        roundCenter_render.material.SetFloat("_FillAmount", 0);

        var roundCenter2 = clock.Find("roundCenter2");
        var roundCenter2_render = roundCenter2.GetComponent<SpriteRenderer>();
        roundCenter2_render.material = new Material(roundCenter2_render.material);
        roundCenter2_render.material.SetFloat("_FillAmount", 0);

        var roundCenter3 = clock.Find("roundCenter3");
        var roundCenter3_render = roundCenter3.GetComponent<SpriteRenderer>();
        roundCenter3_render.material = new Material(roundCenter3_render.material);
        roundCenter3_render.material.SetFloat("_FillAmount", 0);

        AudioManager.Instance.PlaySound("Clock");

        for (int i = 0; i < texts.childCount; i++)
        {
            texts.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1, 0.5f).SetUpdate(true);
        }

        DOTween.To(() => 0f, number => roundCenter_render.material.SetFloat("_FillAmount", number), 1f, .7f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
        DOTween.To(() => 0f, number => roundCenter2_render.material.SetFloat("_FillAmount", number), 1f, .8f).SetEase(Ease.OutQuad).SetUpdate(true).Play();
        DOTween.To(() => 0f, number => roundCenter3_render.material.SetFloat("_FillAmount", number), 1f, .9f).SetEase(Ease.OutQuad).SetUpdate(true).Play();

        // 카드 안삼 만듬
        var bullets = new List<GameObject>();
        string[] cardLoop = {"spade_A", "diamond_A", "heart_A", "clover_A"};
        CardShape[] cardShapeLoop = {CardShape.SPADE, CardShape.DIAMOND, CardShape.HEART, CardShape.CLUB };
        int domi_a = cardLoop.Length - 1;
        for (int i = 0; i < 3; i++)
        {
            var list = _bulletMain.CreateBullets();

            int k = 0;
            foreach (var item in list)
            {
                if (i == 2 && k > 1) {
                    Destroy(item);
                    continue;
                }
                
                if (cardLoop.Length <= domi_a) domi_a = 0;
                item.GetComponentInChildren<SpriteRenderer>().sprite = _bulletMain.GetCardSprite(cardLoop[domi_a]);
                item.transform.parent = clock.transform;
                item.transform.position = transform.root.position;

                // 콜백 생성
                var bullet = item.GetComponent<CardWeaponBullet>();
                bullet.OnCallback += CreateFlushCardHandler(cardShapeLoop[domi_a], item.transform.position, saveDamage, item);
                bullet.damage = saveDamage_default;

                bullets.Add(item);
                k ++;
                domi_a++;
            }
        }
    
        var cardFirstInfo = clock.Find("CardFirstPos");

        int angle = 0;
        foreach (var item in bullets)
        {
            RotateAround(cardFirstInfo.position, cardFirstInfo.rotation, clock.position, Vector3.back, angle, out var endPos, out var endRotate);

            item.transform.DOMove(endPos, .5f).SetEase(Ease.OutQuad).SetUpdate(true).SetDelay(0.05f * (angle / 30) + 1);
            item.transform.DORotateQuaternion(endRotate, .5f).SetEase(Ease.OutQuad).SetUpdate(true).SetDelay(0.05f * (angle / 30) + 1);
            Wait(() => AudioManager.Instance.PlaySound("Draw"), 0.05f * (angle / 30) + 1, true);
            angle += 30;
        }

        // 번쩍
        DOTween.To(() => 4f, number => textMat.SetColor("_Color", new Color(1*Mathf.Pow(2,number),1*Mathf.Pow(2,number),1*Mathf.Pow(2,number))), 2f, 1f).SetEase(Ease.OutQuad).SetUpdate(true).Play().SetDelay(2f);

        yield return new WaitForSecondsRealtime(1 + 1 + 1);
        clock.DORotate(new Vector3(0,0, 360 * 20 * -1), 5f, RotateMode.FastBeyond360).SetEase(Ease.InQuad).SetUpdate(true).OnComplete(() => Destroy(clock.gameObject));
        clock.DOScale(.5f, 1.9f).SetEase(Ease.OutQuad).SetUpdate(true);
        foreach (var item in bullets) {
            item.transform.DOScale(item.transform.localScale / .5f, 1.9f).SetEase(Ease.OutQuad).SetUpdate(true);
        }

        CamManager.StartShake(1, 1.5f);

        yield return new WaitForSecondsRealtime(2);

        CamManager.StartShake(5, 2);

        // 부모 없애ㅐㅐㅐ
        foreach (var item in bullets)
            item.transform.SetParent(null, true);

        foreach (var item in clock.GetComponentsInChildren<SpriteRenderer>())
            item.DOFade(0, 1).SetUpdate(true);

        AudioManager.Instance.PlaySound("Attack");

        Time.timeScale = 1;
        _bulletMain.fireDisable = _bulletMain.interactDisable = false;
    }

        void RotateAround(Vector3 currentPos, Quaternion currentRotate, Vector3 cetner, Vector3 axis, float angle, out Vector3 endPos, out Quaternion endRotate){
            Vector3 pos = currentPos;
            Quaternion rot = Quaternion.AngleAxis(angle, axis); // get the desired rotation
            Vector3 dir = pos - cetner; // find current direction relative to center
            dir = rot * dir; // rotate the direction
            endPos = cetner + dir; // define new position
            // rotate object to keep looking at the center:
            Quaternion myRot = currentRotate;
            endRotate = currentRotate * Quaternion.Inverse(myRot) * rot * myRot;
        }

    void Wait(UnityAction callback, float time, bool real = false) {
        StartCoroutine(_Wait(callback, time, real));
    }
    IEnumerator _Wait(UnityAction cb, float time, bool real) {
        if (real)
            yield return new WaitForSecondsRealtime(time);
        else
            yield return new WaitForSeconds(time);
        cb.Invoke();
    }
}
