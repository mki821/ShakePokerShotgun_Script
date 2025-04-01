using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] bosses;

    private int bossCounter = 0;
    private float seconds = 0;

    private void Update() {
        seconds += Time.deltaTime;

        if(seconds > 120f) {
            seconds -= 120f;
            Instantiate(bosses[bossCounter++], transform.position, Quaternion.identity);
        }
    }
}
