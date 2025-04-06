using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public PlayableDirector timeline; // Asignează Timeline-ul în Inspector
    public int nextSceneName; // Numele scenei de încărcat

    void Start()
    {
        if (timeline != null)
        {
            timeline.stopped += OnCutsceneEnd; // Eveniment când Timeline-ul se termină
        }
    }

    void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == timeline)
        {
            SceneManager.LoadScene(nextSceneName); // Încarcă scena nouă
        }
    }
}
