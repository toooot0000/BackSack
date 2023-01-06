using System.ComponentModel;

namespace Models.TileObjects{
    public enum TileObjectType{
        [Description("null")]
        Null,
        [Description("enemy")]
        Enemy,
        [Description("treasureBox")]
        Treasure,
    }
}