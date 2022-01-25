using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public enum TileType { Empty = 0, Grass, Soil, LastIndex }

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images;
    private SpriteRenderer spriteRenderer;
    private TileType tileType;

    public void Setup(TileType tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType = tileType;
    }

    public TileType TileType 
    {
        set
        {
            tileType = value;
            spriteRenderer.sprite = images[(int)tileType - 1];
        }
        get => tileType;

    }
}

