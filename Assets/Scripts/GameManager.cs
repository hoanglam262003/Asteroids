using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;
    public ParticleSystem explosionEffect;
    public float respawnTime = 2.0f;
    public float respawnBlinkTime = 3.0f;
    public int startingLives = 3;
    public int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();

        if (asteroid.size < 1.0f)
        {
            score += 100;
        }
        else if (asteroid.size < 2.0f)
        {
            score += 50;
        }
        else
        {
            score += 25;
        }
    }

    public void PlayerDied()
    {
        this.explosionEffect.transform.position = this.player.transform.position;
        this.explosionEffect.Play();
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
        this.startingLives = 3;
        this.score = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
