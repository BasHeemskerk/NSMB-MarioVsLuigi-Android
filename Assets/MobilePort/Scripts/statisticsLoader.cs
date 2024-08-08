using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class statisticsLoader : MonoBehaviour
{
    private DateTime startTime;
    private TimeSpan totalTimeSpent;

    [Header("statistics list")]
    public TMP_Text starsCollected;
    public TMP_Text coinsCollected;
    public TMP_Text playedAsMario;
    public TMP_Text playedAsLuigi;
    public TMP_Text amountOfMatches;
    public TMP_Text enemysKilled;
    public TMP_Text KDRatio;
    public TMP_Text WLRatio;

    public TMP_Text mushroomsCollected;
    public TMP_Text miniMushroomsCollected;
    public TMP_Text megaMushroomsCollected;
    public TMP_Text helicoMushroomsCollected;
    public TMP_Text fireFlowersCollected;
    public TMP_Text iceFlowersCollected;
    public TMP_Text blueShellsCollected;
    public TMP_Text starsPowerupsCollected;

    public TMP_Text timePlayed;


    int collectedStars = 0;
    int collectedCoins = 0;
    int playedMario = 0;
    int playedLuigi = 0;
    int matchesPlayed = 0;
    int killedEnemys = 0;

    float kills = 0;
    float deaths = 0;
    float wins = 0;
    float losses = 0;

    int collectedMushrooms = 0;
    int collectedMiniMushrooms = 0;
    int collectedMegaMushrooms = 0;
    int collectedHelicoMushrooms = 0;
    int collectedFireFlowers = 0;
    int collectedIceFlowers = 0;
    int collectedBlueShells = 0;
    int collectedStarsPowerups = 0;

    public int hours;
    public int minutes;
    public int seconds;

    public bool canSaveLastLoad = true;

    void Start()
    {
        load_playerpref_stat();
        setText();
    }

    float calculateKD(float kills, float deaths){
        float kd = kills / deaths;
        if (kd == 0 || kd == null){
            kd = 0;
        }
        float roundedKd = Mathf.Round(kd * 100f) / 100f;
        return roundedKd;
    }

    float calculateWL(float wins, float losses){
        float wl = wins / losses;
        if (wl == 0 || wl == null){
            wl = 0;
        }
        float roundedWl = Mathf.Round(wl * 100f) / 100f;
        return roundedWl;
    }

    void load_playerpref_stat(){
        // load playerpref stats
        collectedStars = PlayerPrefs.GetInt("collectedStars");
        collectedCoins = PlayerPrefs.GetInt("collectedCoins");
        playedMario = PlayerPrefs.GetInt("playedMario");
        playedLuigi = PlayerPrefs.GetInt("playedLuigi");
        matchesPlayed = PlayerPrefs.GetInt("matchesPlayed");
        killedEnemys = PlayerPrefs.GetInt("killedEnemys");

        kills = PlayerPrefs.GetFloat("kills");
        deaths = PlayerPrefs.GetFloat("deaths");
        wins = PlayerPrefs.GetFloat("wins");
        losses = PlayerPrefs.GetFloat("losses");

        collectedMushrooms = PlayerPrefs.GetInt("collectedMushrooms");
        collectedMiniMushrooms = PlayerPrefs.GetInt("collectedMiniMushrooms");
        collectedMegaMushrooms = PlayerPrefs.GetInt("collectedMegaMushrooms");
        collectedHelicoMushrooms = PlayerPrefs.GetInt("collectedHelicoMushrooms");
        collectedFireFlowers = PlayerPrefs.GetInt("collectedFireFlowers");
        collectedIceFlowers = PlayerPrefs.GetInt("collectedIceFlowers");
        collectedBlueShells = PlayerPrefs.GetInt("collectedBlueShells");
        collectedStarsPowerups = PlayerPrefs.GetInt("collectedStarsPowerups");

        hours = PlayerPrefs.GetInt("hours");
        minutes = PlayerPrefs.GetInt("minutes");
        seconds = PlayerPrefs.GetInt("seconds");
    }

    void setText(){
        starsCollected.text = collectedStars.ToString() + " collected";
        coinsCollected.text = collectedCoins.ToString() + " collected";
        playedAsMario.text = "Played as " + playedMario.ToString() + " times";
        playedAsLuigi.text = "Played as " + playedLuigi.ToString() + " times";
        amountOfMatches.text = matchesPlayed.ToString() + " matches played";
        enemysKilled.text = killedEnemys.ToString() + " enemys killed";

        KDRatio.text = kills.ToString() + "K/" + deaths.ToString() + "D" + " (" + calculateKD(kills, deaths).ToString() + "kdr)";
        WLRatio.text = wins.ToString() + "W/" + losses.ToString() + "L" + " (" + calculateWL(wins, losses).ToString() + "wlr)";

        mushroomsCollected.text = collectedMushrooms.ToString() + " collected";
        miniMushroomsCollected.text = collectedMiniMushrooms.ToString() + " collected";
        megaMushroomsCollected.text = collectedMegaMushrooms.ToString() + " collected";
        helicoMushroomsCollected.text = collectedHelicoMushrooms.ToString() + " collected";
        fireFlowersCollected.text = collectedFireFlowers.ToString() + " collected";
        iceFlowersCollected.text = collectedIceFlowers.ToString() + " collected";
        blueShellsCollected.text = collectedBlueShells.ToString() + " collected";
        starsPowerupsCollected.text = collectedStarsPowerups.ToString() + " collected";
    }

    public void resetStats(){
        PlayerPrefs.SetInt("collectedStars", 0);
        PlayerPrefs.SetInt("collectedCoins", 0);
        PlayerPrefs.SetInt("playedMario", 0);
        PlayerPrefs.SetInt("playedLuigi", 0);
        PlayerPrefs.SetInt("matchesPlayed", 0);
        PlayerPrefs.SetInt("killedEnemys", 0);

        PlayerPrefs.SetFloat("kills", 0);
        PlayerPrefs.SetFloat("deaths", 0);
        PlayerPrefs.SetFloat("wins", 0);
        PlayerPrefs.SetFloat("losses", 0);

        PlayerPrefs.SetInt("collectedMushrooms", 0);
        PlayerPrefs.SetInt("collectedMiniMushrooms", 0);
        PlayerPrefs.SetInt("collectedMegaMushrooms", 0);
        PlayerPrefs.SetInt("collectedHelicoMushrooms", 0);
        PlayerPrefs.SetInt("collectedFireFlowers", 0);
        PlayerPrefs.SetInt("collectedIceFlowers", 0);
        PlayerPrefs.SetInt("collectedBlueShells", 0);
        PlayerPrefs.SetInt("collectedStarsPowerups", 0);

        PlayerPrefs.SetInt("hours", 0);
        PlayerPrefs.SetInt("minutes", 0);
        PlayerPrefs.SetInt("seconds", 0);

        PlayerPrefs.SetFloat("totalTimeSpent", 0);

        PlayerPrefs.Save();

        load_playerpref_stat();
        setText();
    }
    
}
