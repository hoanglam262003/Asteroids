using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float respawnTime = 2.0f;
    public float respawnBlinkTime = 3.0f;
    public int startingLives = 3;

    public void PlayerDied()
    {
        this.startingLives--;
        if (this.startingLives > 0)
        {
            Invoke(nameof(Respawn), respawnTime);
        }
        else
        {
            GameOver();
        }
    }

    private void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), respawnBlinkTime);
    }

    private void TurnOnCollisions()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        
    }
}
