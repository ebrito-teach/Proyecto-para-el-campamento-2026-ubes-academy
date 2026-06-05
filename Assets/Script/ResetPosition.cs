using UnityEngine;
using UnityEngine.Tilemaps;

public class ResetPosition : MonoBehaviour
{
    public Tilemap[] otherTilemaps;

    private Tilemap myTilemap;

    void Start()
    {
        myTilemap = GetComponent<Tilemap>();
        myTilemap.CompressBounds();

        Debug.Log(gameObject.name + " | worldStart: " + GetWorldStart(myTilemap) + " | worldEnd: " + GetWorldEnd(myTilemap));
    }

    float GetWorldEnd(Tilemap t)
    {
        return t.transform.position.x + t.localBounds.max.x;
    }

    float GetWorldStart(Tilemap t)
    {
        return t.transform.position.x + t.localBounds.min.x;
    }

    void Update()
    {
        float myWorldStart = GetWorldStart(myTilemap);
        float myWorldEnd = GetWorldEnd(myTilemap);

        // Solo resetear cuando el tilemap completo salió por la izquierda
        if (myWorldEnd <= Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect)
        {
            Tilemap rightmost = otherTilemaps[0];
            foreach (var t in otherTilemaps)
            {
                if (GetWorldEnd(t) > GetWorldEnd(rightmost))
                    rightmost = t;
            }

            float targetWorldEnd = GetWorldEnd(rightmost);
            float myOffset = myTilemap.localBounds.min.x;

            transform.position = new Vector3(
                targetWorldEnd - myOffset,
                transform.position.y,
                transform.position.z
            );

            myTilemap.RefreshAllTiles();

            Debug.Log(gameObject.name + " RESET → worldStart: " + GetWorldStart(myTilemap) + " | worldEnd: " + GetWorldEnd(myTilemap));
        }
    }
}