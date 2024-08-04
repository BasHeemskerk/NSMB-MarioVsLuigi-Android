using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Audio;

using Photon.Pun;

public class Settings : Singleton<Settings> {
    public AudioMixer mixer;

    private float _volumeMaster, _volumeMusic, _volumeSFX;
    private int _fpsGame;
    public int[] fpsValues;

    public float VolumeMaster {
        get => _volumeMaster;
        set {
            _volumeMaster = Mathf.Clamp01(value);
            ApplyVolumeSettings();
        }
    }
    public float VolumeSFX {
        get => _volumeSFX;
        set {
            _volumeSFX = Mathf.Clamp01(value);
            ApplyVolumeSettings();
        }
    }
    public float VolumeMusic {
        get => _volumeMusic;
        set {
            _volumeMusic = Mathf.Clamp01(value);
            ApplyVolumeSettings();
        }
    }
    public int FPSGame {
        get => _fpsGame;
        set {
            _fpsGame = Mathf.Clamp(value, 0, 3);
            SetFPSGame(_fpsGame);
        }
    }

    public string nickname;
    public int character, skin, amountOfControls = 5;
    public bool scrControls = true, ndsResolution = false, fireballFromSprint = true, vsync = false, fourByThreeRatio = false;
    public bool scoreboardAlways = true, filter = true;
    public bool globalServer = true, vibration, useDpad = false;
    public float[] controlSizeMultiplier, controlX, controlY;

    public void Awake() {
        if (!InstanceCheck())
            return;



        Instance = this;
        controlSizeMultiplier = new float[amountOfControls];
        controlX = new float[amountOfControls];
        controlY = new float[amountOfControls];
        LoadSettingsFromPreferences();
        ApplyVolumeSettings();
    }


    public void LoadSettingsFromPreferences() {
        nickname = PlayerPrefs.GetString("Nickname");
        if (nickname == null || nickname == "")
            nickname = "Player" + Random.Range(1000, 10000);

        scrControls = true;
        VolumeSFX = PlayerPrefs.GetFloat("volumeSFX", 0.5f);
        VolumeMusic = PlayerPrefs.GetFloat("volumeMusic", 0.25f);
        VolumeMaster = PlayerPrefs.GetFloat("volumeMaster", 1);
        ndsResolution = PlayerPrefs.GetInt("NDSResolution", 0) == 1;
        fireballFromSprint = false;
        FPSGame = PlayerPrefs.GetInt("fpsGame", 1); //0 = 30, 1 = 60, 2 = 120, 3 = 144
        vsync = PlayerPrefs.GetInt("VSync", 0) == 1;
        fourByThreeRatio = PlayerPrefs.GetInt("NDS4by3", 0) == 1;
        scoreboardAlways = true;
        filter = PlayerPrefs.GetInt("ChatFilter", 1) == 1;
        globalServer = PlayerPrefs.GetInt("GlobalServer", 1) == 1;
        vibration = PlayerPrefs.GetInt("Vibration", 1) == 1;
        useDpad = PlayerPrefs.GetInt("UseDpad", 0) == 1;
        character = PlayerPrefs.GetInt("Character", 0);
        skin = PlayerPrefs.GetInt("Skin", 0);

        //control customization
        for (int i=0; i < amountOfControls; i++){
            controlSizeMultiplier[i] = PlayerPrefs.GetFloat("control_size_multiplier_" + i); //control_ is general name, 0 is the id of the control
            controlX[i] = PlayerPrefs.GetFloat("control_y_" + i); //control_ is general name, 0 is the id of the control
            controlY[i] = PlayerPrefs.GetFloat("control_x_" + i); //control_ is general name, 0 is the id of the control
        }
    }
    public void SaveSettingsToPreferences() {
        PlayerPrefs.SetString("Nickname", Regex.Replace(PhotonNetwork.NickName, "\\(\\d*\\)", ""));
        PlayerPrefs.SetFloat("volumeSFX", VolumeSFX);
        PlayerPrefs.SetFloat("volumeMusic", VolumeMusic);
        PlayerPrefs.SetFloat("volumeMaster", VolumeMaster);
        PlayerPrefs.SetInt("NDSResolution", ndsResolution ? 1 : 0);
        //PlayerPrefs.SetInt("FireballFromSprint", fireballFromSprint ? 1 : 0);
        PlayerPrefs.SetInt("fpsGame", FPSGame);
        PlayerPrefs.SetInt("VSync", vsync ? 1 : 0);
        //PlayerPrefs.SetInt("ScrControls", 1);
        PlayerPrefs.SetInt("NDS4by3", fourByThreeRatio ? 1 : 0);
        //PlayerPrefs.SetInt("ScoreboardAlwaysVisible", scoreboardAlways ? 1 : 0);
        PlayerPrefs.SetInt("ChatFilter", filter ? 1 : 0);
        PlayerPrefs.SetInt("GlobalServer", globalServer ? 1 : 0);
        PlayerPrefs.SetInt("Vibration", vibration ? 1 : 0);
        PlayerPrefs.SetInt("UseDpad", useDpad ? 1 : 0);
        PlayerPrefs.SetInt("Character", character);
        PlayerPrefs.SetInt("Skin", skin);

        //control customization
        for (int i=0; i < amountOfControls; i++){
            PlayerPrefs.SetFloat("control_size_multiplier_" + i, controlSizeMultiplier[i]); //control_ is general name, 0 is the id of the control
            PlayerPrefs.SetFloat("control_x_" + i, controlX[i]); //control_ is general name, 0 is the id of the control
            PlayerPrefs.SetFloat("control_y_" + i, controlY[i]); //control_ is general name, 0 is the id of the control
        }

        PlayerPrefs.Save();
    }

    void ApplyVolumeSettings() {
        mixer.SetFloat("MusicVolume", Mathf.Log10(VolumeMusic) * 20);
        mixer.SetFloat("SoundVolume", Mathf.Log10(VolumeSFX) * 20);
        mixer.SetFloat("MasterVolume", Mathf.Log10(VolumeMaster) * 20);
    }

    public void SetFPSGame(int fps) {
        _fpsGame = fps;
        Application.targetFrameRate = fps == 0 ? 30 : fps == 1 ? 60 : fps == 2 ? 120 : 144;
    }
}