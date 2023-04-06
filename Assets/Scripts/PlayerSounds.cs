using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake() {
        footstepTimer = footstepTimerMax;
        player = GetComponent<Player>();
    }

    private void Update() {
        footstepTimer-=Time.deltaTime;
        if(footstepTimer <0f) {
            if (player.IsWaking()) {
                SoundManager.Instance.PlayFootstepSound(player.transform.position);
            }
            footstepTimer = footstepTimerMax;
        }
    }
}
