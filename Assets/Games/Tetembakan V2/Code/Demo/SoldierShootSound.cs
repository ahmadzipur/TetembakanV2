using UnityEngine;

public class SoldierShootSound : MonoBehaviour
{
    public AudioSource shootAudio;

    // Dipanggil dari Animation Event
    public void PlayShoot()
    {
        shootAudio.PlayOneShot(shootAudio.clip);
    }
}
