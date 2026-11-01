using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyListText;
    [SerializeField] private GameObject enemyPrefab;



    private List<GameObject> activeEnemies = new List<GameObject>();
    private int enemyCounter = 0;

    private void Start()
    {
        UpdateUI();
    }


    public void SpawnEnemy()
    {
        GameObject enemy;
        Vector3 pos = new Vector3(Random.Range(-6f, 6f), 0.5f, Random.Range(-4f, 4f));

        if (enemyPrefab != null)
            enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
        else
        {
            enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
            enemy.transform.position = pos;
        }

        enemyCounter++;
        enemy.name = "Enemy " + enemyCounter;

        activeEnemies.Add(enemy);
        UpdateUI();
    }


    public void DefeatEnemy(GameObject enemy)
    {
        if (enemy == null || !activeEnemies.Contains(enemy))
        {
            Debug.LogWarning("DefeatEnemy: enemy is not in the active list.");
            return;
        }

        activeEnemies.Remove(enemy);
        Destroy(enemy);
        UpdateUI();
    }


    public void DefeatLastEnemy()
    {
        if (activeEnemies.Count == 0)
        {
            Debug.LogWarning("No enemies to defeat.");
            return;
        }

        DefeatEnemy(activeEnemies[activeEnemies.Count - 1]);
    }


    public void WipeWave()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i] != null)
                Destroy(activeEnemies[i]);
        }

        activeEnemies.Clear();
        enemyCounter = 0;
        UpdateUI();
    }


    private void UpdateUI()
    {
        if (enemyListText == null) return;

        string display = "Active Enemies: " + activeEnemies.Count + "\n";

        for (int i = 0; i < activeEnemies.Count; i++)
        {
            display += (i + 1) + ". " + activeEnemies[i].name + "\n";
        }

        enemyListText.text = display;
    }
}