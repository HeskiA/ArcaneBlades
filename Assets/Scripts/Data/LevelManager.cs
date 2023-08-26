using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int level = 1;
    public int finalLevel = 5;
    public static int score = 0;
    public static int scoreIncrement = 5;

    public static string levelString = "Level: ";
    public static string scoreString = "Score: ";
    public float addEnemySpeedOnLevelUp = 1f;
    public float enemySpeed = 2f;
    public int enemyDamage = 10;
    public int addEnemyDamageOnLevelUp = 10;

    private float enemySpeedDefault;
    private int enemyDamageDefault;
    [SerializeField] public TMP_Text levelTxt;
    [SerializeField] public TMP_Text scoreTxt;
    void Start()
    {
        levelTxt.text = levelString + level;
        scoreTxt.text = scoreString + score;
        enemySpeedDefault = enemySpeed;
        enemyDamageDefault = enemyDamage;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = scoreString + score;
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

    public void OnDeath()
    {
        level = 1;
        enemySpeed = enemySpeedDefault;
        enemyDamage = enemyDamageDefault;
    }

    public static void incrementScore()
    {
        score += scoreIncrement * level;
    }
}
