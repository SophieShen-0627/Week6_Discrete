using UnityEngine;

public class DashPoint : MonoBehaviour
{
    private float DetectionRange = 2.0f;

    private Color CloseColor = new Color();
    private Color ChoseColor = new Color();
    private Color InitialColor = new Color();

    public bool IsChosen = false;
    public bool OneTimeUse = false;

    private SpriteRenderer ColorRenderer;
    private PlayerMove player;
    private EnemySpawner spawner;
    private int EnemyNumAround;
    private float Health = 1;
    private float LoseHealthRate;

    // Start is called before the first frame update
    void Start()
    {
        DetectionRange = DataManager.datas.DashPointDetectRange;
        CloseColor = DataManager.datas.DashPointCloseColor;
        ChoseColor = DataManager.datas.DashPointChosenColor;
        if (!OneTimeUse) InitialColor = DataManager.datas.DashPointOriginalColor;
        else InitialColor = DataManager.datas.DashPointOneTimeOriginalColor;
        LoseHealthRate = DataManager.datas.LoseHealthRate;

        ColorRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>();
        spawner = FindObjectOfType<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseposition.z = transform.position.z;

        if (!IsChosen)
        {
            if (Vector2.Distance(mouseposition, transform.position) <= DetectionRange)
            {
                ColorRenderer.color = CloseColor;

                if (Input.GetMouseButtonDown(0))
                {
                    IsChosen = true;
                    player.Target = transform.position;
                }
            }
            else
            {
                ColorRenderer.color = InitialColor;
            }
        }
        if (IsChosen)
        {
            ColorRenderer.color = ChoseColor;
        }

        if (Vector2.Distance(player.transform.position, transform.position) <= 0.05f)
        {
            IsChosen = false;

            if (OneTimeUse) gameObject.SetActive(false);
        }

        EnemyNumAround = DetectEnemy();

        Health -= EnemyNumAround * LoseHealthRate * Time.deltaTime;
        InitialColor = new Color(Health, InitialColor.g, InitialColor.b, 1);

        if (Health <= 0)
        {
            DoDeath();
        }
    }

    private void DoDeath()
    {
        GetComponentInChildren<Explosion>().DoExplosion();
        GetComponentInChildren<Explosion>().transform.parent = null;

        gameObject.SetActive(false);
    }

    private int DetectEnemy()
    {
        int enemyNum = 0;
        GameObject[] enemies = spawner.CurrentEnemies.ToArray();

        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector2.Distance(transform.position, enemies[i].transform.position) <= 0.2f)
            {
                enemyNum += 1;
            }
        }

        return enemyNum;
    }
}
