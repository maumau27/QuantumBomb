using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombaQuantica : MonoBehaviour
{
    public Bounds spawnArea;

    public float respawTime;
    public float unfreezeTime;
    public float predictTime;

    public GameObject ghost;
    public bool frozen = false;
    public bool predict = false;

    private Vector3 nextPos;
    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private SpriteRenderer _ghostRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _ghostRenderer = ghost.GetComponent<SpriteRenderer>();

        _ghostRenderer.enabled = false;
        nextPos = GetRandomLocation();
        ghost.transform.position = nextPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerChangeDir()
    {
        if (!frozen)
        {
            transform.position = nextPos;
            nextPos = GetRandomLocation();
            ghost.transform.position = nextPos;
        }
    }

    public void Freeze()
    {
        frozen = true;
        StartCoroutine(UnFreeze());
    }

    public void Predict()
    {
        predict = true;
        _ghostRenderer.enabled = true;
        StartCoroutine(StopPredict());
    }

    public void Die()
    {
        Hide();
        StartCoroutine(Respawn());
    }

    private void Hide()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
        _ghostRenderer.enabled = false;
    }

    private void Show()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
        if (predict)
            _ghostRenderer.enabled = true;
    }

    public IEnumerator Respawn()
    {
        float timer = 0;
        while (timer < respawTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Show();
        transform.position = GetRandomLocation();
        nextPos = GetRandomLocation();
        ghost.transform.position = nextPos;
        yield return null;
    }

    public IEnumerator UnFreeze()
    {
        float timer = 0;
        while (timer < unfreezeTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        frozen = false;
    }

    public IEnumerator StopPredict()
    {
        float timer = 0;
        while (timer < predictTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _ghostRenderer.enabled = false;
        predict = false;
    }

    public Vector3 GetRandomLocation()
    {
        Vector3 pos = new Vector3(Random.Range(spawnArea.min.x, spawnArea.max.x), Random.Range(spawnArea.min.y, spawnArea.max.y), -1);

        return pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                collision.GetComponent<Player>().Die();
                break;
            case "Bolha":
                this.Die();
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Bounds bounds = spawnArea;

        Vector3 up_left_corner = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 up_rigth_corner = new Vector3(bounds.min.x, bounds.max.y, 0);
        Vector3 down_left_corner = new Vector3(bounds.max.x, bounds.min.y, 0);
        Vector3 down_rigth_corner = new Vector3(bounds.max.x, bounds.max.y, 0);

        Gizmos.DrawLine(up_left_corner, up_rigth_corner);
        Gizmos.DrawLine(up_left_corner, down_left_corner);
        Gizmos.DrawLine(up_rigth_corner, down_rigth_corner);
        Gizmos.DrawLine(down_rigth_corner, down_left_corner);
    }
}
