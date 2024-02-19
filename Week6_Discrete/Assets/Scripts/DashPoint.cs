using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPoint : MonoBehaviour
{
    private float DetectionRange = 2.0f;

    private Color CloseColor = new Color();
    private Color ChoseColor = new Color();
    private Color InitialColor = new Color();

    public bool IsChosen = false;

    private SpriteRenderer ColorRenderer;
    private PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        DetectionRange = DataManager.datas.DashPointDetectRange;
        CloseColor = DataManager.datas.DashPointCloseColor;
        ChoseColor = DataManager.datas.DashPointChosenColor;
        InitialColor = DataManager.datas.DashPointOriginalColor;

        ColorRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseposition = Camera.main.ScreenToWorldPoint( Input.mousePosition);
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

        if (Vector2.Distance(player.transform.position, transform.position) <= 0.05f) IsChosen = false;
    }
}
