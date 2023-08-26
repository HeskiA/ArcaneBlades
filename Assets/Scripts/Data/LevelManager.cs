using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static int level = 1;
    public int finalLevel = 5;

    public static string levelString = "Level: ";
    public float addEnemySpeedOnLevelUp = 1f;
    public float enemySpeed = 2f;
    public int enemyDamage = 10;
    public int addEnemyDamageOnLevelUp = 10;

    [SerializeField] public TMP_Text levelTxt;
    void Start()
    {
        levelTxt.text = levelString + level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementLevel()
    {
        level++;
        if (level == finalLevel)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            levelTxt.text = levelString + level;
            enemySpeed += addEnemySpeedOnLevelUp;
            enemyDamage += addEnemyDamageOnLevelUp;
        }

    }

    public int GetLevel()
    {
        return level;
    }

    public float GetSpeed()
    {
        return enemySpeed;
    }

    public int GetDamage()
    {
        return enemyDamage;
    }
}
