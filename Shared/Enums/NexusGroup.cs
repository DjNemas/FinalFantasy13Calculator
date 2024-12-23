using System.Runtime.Serialization;

namespace Shared.Enums
{
    public enum NexusGroup
    {
        [EnumMember(Value = "Hoher TP-Wert")]
        HoherTpWert,
        [EnumMember(Value = "Pysische Abwehr")]
        Pysischeabwehr,
        [EnumMember(Value = "Geringer TP-Wert")]
        GeringerTpWert,
        Schadensreduktion,
        [EnumMember(Value = "Magie Abwehr")]
        MagieAbwehr,
        [EnumMember(Value = "Unabhängig")]
        Unabhaengig,
        [EnumMember(Value = "Ultimativ pyhsisch")]
        UltimativPyhsisch,
        [EnumMember(Value = "Ultimativ magisch")]
        UltimativMagisch,
        Feuerresistenz,
        Eisresistenz,
        Blitzresistenz,
        Wasserresistenz,
        Windresistenz,
        Erdresistenz,
        [EnumMember(Value = "Decourage-Resistenz")]
        DecourageResistenz,
        [EnumMember(Value = "Degener-Resistenz")]
        DegenerResistenz,
        [EnumMember(Value = "Deprotes-Resistenz")]
        DeprotesResistenz,
        [EnumMember(Value = "Devall-Resistenz")]
        DevallResistenz,
        [EnumMember(Value = "Gemach-Resistenz")]
        GemachResistenz,
        [EnumMember(Value = "Gift-Resistenz")]
        GiftResistenz,
        [EnumMember(Value = "Morbid-Resistenz")]
        MorbidResistenz,
        [EnumMember(Value = "Fluch-Resistenz")]
        FluchResistenz,
        [EnumMember(Value = "Pein-Resistenz")]
        PeinResistenz,
        [EnumMember(Value = "Nebel-Resistenz")]
        NebelResistenz,
        [EnumMember(Value = "Exanima-Resistenz")]
        ExanimaResistenz,
        Unerbittlich,
        Positiveffekt,
        Beschleunigung,
        Metamorph,

    }
}
