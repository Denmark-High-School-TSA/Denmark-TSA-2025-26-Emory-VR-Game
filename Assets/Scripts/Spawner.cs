using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public TextMeshPro pointText;
    public TextMeshPro scoreText;
    public LayerMask blockLayers;
    public GameObject ball;
    public GameObject ServeText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] hitClips;
    private float overlapRadius = 0.1f;
    private int points;
    private int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        Vector3 change = new Vector3(Random.Range(-1.1f,1.1f), Random.Range(-0.5f,0.25f), Random.Range(-1f,0.5f));
        Checker(change);
    }

    void Checker(Vector3 i)
    {
        Collider[] hits = Physics.OverlapSphere(i, overlapRadius, blockLayers);

        if (hits.Length > 0)
        {
            Spawn();
        }
        else
        {
            transform.localPosition = i;
            points = 100;
            if (i.y > -0.25f)
            {
                points = 200;
            }
            if (i.y > 0.0f)
            {
                points = 300;
            }
            SetPointText();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == ball)
        {
            SetScoreText();
            if (hitClips.Length > 0)
            {
                audioSource.PlayOneShot(hitClips[Random.Range(0, hitClips.Length)]);
            }
            Spawn();
            ServeText.SetActive(true);
        }
    }
    void SetPointText()
    {
        pointText.text = "+" + points.ToString();
    }

    void SetScoreText()
    {
        if (int.TryParse(scoreText.text, out int num))
        {
            score = num + points;
        }
        else if (int.TryParse(scoreText.text.Substring(1), out int num2))
        {
            score = num2 + points;
        }
        scoreText.text = score.ToString("D4");
    }

}
