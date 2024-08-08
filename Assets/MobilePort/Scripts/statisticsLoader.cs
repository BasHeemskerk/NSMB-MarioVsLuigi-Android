using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class statisticsLoader : MonoBehaviour
{
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

    void Start(){
        load_playerpref_stat();
        setText();
    }

    float calculateKD(float kills, float deaths){
        float kd = kills / deaths;
        if (kd == 0){
            kd = 0;
        }
        return kd;
    }

    float calculateWL(float wins, float losses){
        float wl = wins / losses;
        if (wl == 0){
            wl = 0;
        }
        return wl;
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
    
}
