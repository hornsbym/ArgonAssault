using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    /// <summary>
    /// Fires before "Start()"
    /// </summary>
    void Awake() {
        // Necesary to make MusicPlayer a singleton w/o static objects.
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        print("NumMusicPlayers = " + numMusicPlayers);
        if (numMusicPlayers > 1) {
            print("Destroying self...");
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
