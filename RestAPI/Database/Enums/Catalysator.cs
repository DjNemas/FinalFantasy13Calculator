using System.Runtime.Serialization;

namespace RestAPI.Database.Enums
{
    public enum Catalysator
    {
        None,
        Millerit,
        Rosenspat,
        Kobalt,
        Perowskit,
        Pechblende,
        [EnumMember(Value = "Mnar-Stein")]
        MnarStein,
        Rotlegierung,
        Adamantit,
        Dunkelkristall,
        Trapezohedrohn
    }
}
