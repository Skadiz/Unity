using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    public FloorType floorType;
    public Entity entity;
    public Field(FloorType floorType, Entity entity = Entity.None) {
        this.floorType = floorType;
        this.entity = entity;
    }
}

public enum FloorType {
    Floor,
    Target,
    Wall
}

public enum Entity {
    Player,
    Crate,
    None
}