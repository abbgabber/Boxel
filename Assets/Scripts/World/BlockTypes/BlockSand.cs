using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class BlockSand : Block
{

    public BlockSand()
        : base()
    {

    }

    public override Tile TexturePosition(Direction direction)
    {
        Tile tile = new Tile();

        tile.x = 1;
        tile.y = 2;

        return tile;
    }
}