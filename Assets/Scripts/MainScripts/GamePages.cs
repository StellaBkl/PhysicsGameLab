using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePages : MonoBehaviour
{
    public GameObject gamesObject;

    public GameObject GetGames(string gameId)
    {
        GameObject game = gameObject.transform.Find("Game"+gameId).gameObject;
        Debug.Log(game);
        return game;
    }
}
