using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneSettings", menuName = "Scene/Settings")]
public class SceneSettings : ScriptableObject
{
    [Header("Scene Conf")]
    public Scene scene;
    public SoundClip activeSoundClip;
    [Header("Scoreboard Labels")]
    public bool scoreLabelEnabled;
    public bool livesLabelEnabled;
    public bool highScoreLabelEnabled;
    [Header("Scene Lables")]
    public bool startLabelActive;
    public bool endLabelActive;
    [Header("scene Buttons")]
    public bool startButtonActive;
    public bool restartButtonActive;


}
