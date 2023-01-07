using System.ComponentModel;

namespace Components.TileObjects{
    public enum TileObjectType{
        [Description("null")]
        Null,
        [Description("enemy")]
        Enemy,
        [Description("treasureBox")]
        Treasure,
    }
}