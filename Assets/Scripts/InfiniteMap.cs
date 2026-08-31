using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteMap : MonoBehaviour
{
    public Tilemap originalTilemap;
    public Transform player;

    private Tilemap[,] chunks = new Tilemap[3, 3];

    private float chunkWidth;
    private float chunkHeight;

    private Vector3 lastPlayerChunk;

    void Start()
{
    if (originalTilemap == null)
    {
        Debug.LogError("Original Tilemap não foi colocado!");
        return;
    }

    if (player == null)
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;
    }

    // Remove células vazias das bordas do Tilemap
    originalTilemap.CompressBounds();

    BoundsInt bounds = originalTilemap.cellBounds;

    Vector3 cellSize =
        originalTilemap.layoutGrid.cellSize;

    chunkWidth =
        bounds.size.x * cellSize.x;

    chunkHeight =
        bounds.size.y * cellSize.y;

    Debug.Log(
        "Tamanho do chunk: " +
        chunkWidth + " x " +
        chunkHeight
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
                    // O Tilemap original fica no centro
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
                    originalTilemap.transform.position +
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
            playerPosition.x / chunkWidth
        );

        int playerChunkY = Mathf.FloorToInt(
            playerPosition.y / chunkHeight
        );

        if (new Vector3(playerChunkX, playerChunkY, 0) == lastPlayerChunk)
            return;

        lastPlayerChunk = new Vector3(
            playerChunkX,
            playerChunkY,
            0
        );

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float newX =
                    (playerChunkX + x - 1) * chunkWidth;

                float newY =
                    (playerChunkY + y - 1) * chunkHeight;

                chunks[x, y].transform.position =
                    new Vector3(newX, newY, 0);
            }
        }
    }
}