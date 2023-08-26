using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public static GameObject myPrefab;
    private static string enemyTag = "Enemy";
    public static GameObject nextLevelPrefab;

    public static void SpawnEnemies(Dictionary<Vector2Int, int> distancesDict,Dictionary<Vector2Int, HashSet<Vector2Int>> roomDict) 
    {
        
        GameObject[] existingEnemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject enemy in existingEnemies)
        {
            DestroyImmediate(enemy);
        }
        var sortedItems = distancesDict.ToList();
        sortedItems.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        Dictionary<Vector2Int, int> sortedDistancesDict = sortedItems.ToDictionary(item => item.Key, item => item.Value);


        KeyValuePair<Vector2Int, int> firstItem = sortedDistancesDict.First();
        KeyValuePair<Vector2Int, int> lastItem = sortedDistancesDict.Last();
        sortedDistancesDict.Remove(firstItem.Key);
        sortedDistancesDict.Remove(lastItem.Key);

        int iterator = 0;
        foreach (var kvp in sortedDistancesDict)
        {
            int spawnNumber = Random.Range(1+iterator, 3+iterator);

            var keyToFind = kvp.Key;
            var roomToSpawnIn = roomDict[keyToFind];
            List<Vector2Int> selectedValues = new List<Vector2Int>();

            while (selectedValues.Count < spawnNumber && roomToSpawnIn.Count > 0)
            {
                int randomIndex = Random.Range(0, roomToSpawnIn.Count);
                Vector2Int randomValue = roomToSpawnIn.ElementAt(randomIndex);

                selectedValues.Add(randomValue);
                Instantiate(myPrefab, new Vector3(randomValue.x+0.5f, randomValue.y+0.5f, 0), Quaternion.identity);
                roomToSpawnIn.Remove(randomValue);
            }
            iterator++; 
        }


        Instantiate(nextLevelPrefab, new Vector3(lastItem.Key.x + 0.5f, lastItem.Key.y + 0.5f, 0), Quaternion.identity);
    }


    private static Color HexToColor(string hex)
    {
        Color newColor = Color.white;
        ColorUtility.TryParseHtmlString(hex, out newColor);
        return newColor;
    }
}
