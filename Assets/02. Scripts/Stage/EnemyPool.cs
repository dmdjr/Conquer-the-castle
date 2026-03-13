using System.Collections.Generic;
using UnityEngine;

// 역할: 적 오브젝트 풀 관리만 담당 (생성/반환)
public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int initialSize = 10;

    private Queue<GameObject> available = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            var obj = Instantiate(enemyPrefab, transform);
            obj.SetActive(false);
            available.Enqueue(obj);
        }
    }

    public GameObject Get(Vector3 position)
    {
        var obj = available.Count > 0
            ? available.Dequeue()
            : Instantiate(enemyPrefab, transform); // 풀 소진 시 예비 생성

        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        available.Enqueue(obj);
    }
}
