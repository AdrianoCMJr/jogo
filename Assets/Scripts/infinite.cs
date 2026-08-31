using UnityEngine;
using UnityEngine.Tilemaps;

public class infinito : MonoBehaviour
{
    public Tilemap originalTilemap;
    public Transform player;

    private Tilemap[,] chunks = new Tilemap[3, 3];

    private float chunkWidth;
    private float chunkHeight;

    private Vector2Int lastPlayerChunk = new Vector2Int(int.MinValue, int.MinValue);

    private Vector3 originalPosition;

    void Start()
    {
        if (originalTilemap == null)
        {
            Debug.LogError("Original Tilemap não foi colocado!");
            return;
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
                player = playerObject.transform;
        }

        // IMPORTANTE:
        // Remove espaço vazio dos bounds do Tilemap
        originalTilemap.CompressBounds();

        BoundsInt bounds = originalTilemap.cellBounds;

        Vector3 cellSize = originalTilemap.layoutGrid.cellSize;

        chunkWidth = bounds.size.x * cellSize.x;
        chunkHeight = bounds.size.y * cellSize.y;

        originalPosition = originalTilemap.transform.position;

        Debug.Log(
            "Bounds: " + bounds +
            "\nTamanho em células: " + bounds.size +
            "\nChunk: " + chunkWidth + " x " + chunkHeight
        );

        CriarChunks();
    }

    void CriarChunks()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (x == 1 && y == 1)
                {
                    chunks[x, y] = originalTilemap;
                }
                else
                {
                    GameObject novoChunk = Instantiate(
                        originalTilemap.gameObject,
                        originalTilemap.transform.parent
                    );

                    novoChunk.name = "Chunk_" + x + "_" + y;

                    chunks[x, y] = novoChunk.GetComponent<Tilemap>();
                }

                chunks[x, y].transform.position =
                    originalPosition +
                    new Vector3(
                        (x - 1) * chunkWidth,
                        (y - 1) * chunkHeight,
                        0
                    );
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        AtualizarChunks();
    }

    void AtualizarChunks()
    {
        Vector3 playerPosition = player.position;

        int playerChunkX = Mathf.FloorToInt(
            (playerPosition.x - originalPosition.x) / chunkWidth
        );

        int playerChunkY = Mathf.FloorToInt(
            (playerPosition.y - originalPosition.y) / chunkHeight
        );

        Vector2Int currentChunk = new Vector2Int(
            playerChunkX,
            playerChunkY
        );

        if (currentChunk == lastPlayerChunk)
            return;

        lastPlayerChunk = currentChunk;

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float newX =
                    originalPosition.x +
                    (playerChunkX + x - 1) * chunkWidth;

                float newY =
                    originalPosition.y +
                    (playerChunkY + y - 1) * chunkHeight;

                chunks[x, y].transform.position =
                    new Vector3(newX, newY, originalPosition.z);
            }
        }
    }
}