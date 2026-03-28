namespace SwiftlyS2.Shared.Trace;

public struct PhysSurfacePropertiesPhysicsTrace
{
    /// <summary>
    /// The friction coefficient of the surface, affecting how objects slide on it.
    /// </summary>
    public float Friction { get; internal set; }
    /// <summary>
    /// The elasticity of the surface, determining how much energy is retained in collisions.
    /// </summary>
    public float Elasticity { get; internal set; }
    /// <summary>
    /// The density of the surface material.
    /// </summary>
    public float Density { get; internal set; }
    /// <summary>
    /// The thickness of the surface material.
    /// </summary>
    public float Thickness { get; internal set; }
    /// <summary>
    /// The frequency of soft contact interactions with this surface.
    /// </summary>
    public float SoftContactFrequency { get; internal set; }
    /// <summary>
    /// The damping ratio for soft contact interactions with this surface.
    /// </summary>
    public float SoftContactDampingRatio { get; internal set; }
    /// <summary>
    /// The wheel drag value for this surface, affecting vehicle movement.
    /// </summary>
    public float WheelDrag { get; internal set; }
    /// <summary>
    /// The heat conductivity of the surface material.
    /// </summary>
    public float HeatConductivity { get; internal set; }
    /// <summary>
    /// The flashpoint temperature of the surface material.
    /// </summary>
    public float Flashpoint { get; internal set; }

    public override string ToString()
    {
        return $"PhysSurfacePropertiesPhysicsTrace {{ Friction: {Friction}, Elasticity: {Elasticity}, Density: {Density}, Thickness: {Thickness}, SoftContactFrequency: {SoftContactFrequency}, SoftContactDampingRatio: {SoftContactDampingRatio}, WheelDrag: {WheelDrag}, HeatConductivity: {HeatConductivity}, Flashpoint: {Flashpoint} }}";
    }
}

public struct PhysSurfacePropertiesSoundNamesTrace
{
    public PhysSurfacePropertiesSoundNamesTrace()
    {
    }

    /// <summary>
    /// The sound to play for soft impacts on this surface.
    /// </summary>
    public string ImpactSoft { get; internal set; } = "";
    /// <summary>
    /// The sound to play for hard impacts on this surface.
    /// </summary>
    public string ImpactHard { get; internal set; } = "";
    /// <summary>
    /// The sound to play for smooth scraping interactions on this surface.
    /// </summary>
    public string ScrapeSmooth { get; internal set; } = "";
    /// <summary>
    /// The sound to play for rough scraping interactions on this surface.
    /// </summary>
    public string ScrapeRough { get; internal set; } = "";
    /// <summary>
    /// The sound to play when a bullet impacts this surface.
    /// </summary>
    public string BulletImpact { get; internal set; } = "";
    /// <summary>
    /// The sound to play when objects roll on this surface.
    /// </summary>
    public string Rolling { get; internal set; } = "";
    /// <summary>
    /// The sound to play when this surface breaks.
    /// </summary>
    public string Break { get; internal set; } = "";
    /// <summary>
    /// The sound to play when this surface is under strain.
    /// </summary>
    public string Strain { get; internal set; } = "";
    /// <summary>
    /// The sound to play for melee impacts on this surface.
    /// </summary>
    public string MeleeImpact { get; internal set; } = "";
    /// <summary>
    /// The sound to play when pushing off from this surface.
    /// </summary>
    public string PushOff { get; internal set; } = "";
    /// <summary>
    /// The sound to play when skidding to a stop on this surface.
    /// </summary>
    public string SkidStop { get; internal set; } = "";

    public override string ToString()
    {
        return $"PhysSurfacePropertiesSoundNamesTrace {{ ImpactSoft: \"{ImpactSoft}\", ImpactHard: \"{ImpactHard}\", ScrapeSmooth: \"{ScrapeSmooth}\", ScrapeRough: \"{ScrapeRough}\", BulletImpact: \"{BulletImpact}\", Rolling: \"{Rolling}\", Break: \"{Break}\", Strain: \"{Strain}\", MeleeImpact: \"{MeleeImpact}\", PushOff: \"{PushOff}\", SkidStop: \"{SkidStop}\" }}";
    }
}

public struct PhysSurfacePropertiesAudioTrace
{
    /// <summary>
    /// The reflectivity of the surface for audio, affecting how sound bounces off it.
    /// </summary>
    public float Reflectivity { get; internal set; }
    /// <summary>
    /// The hardness factor of the surface for audio processing.
    /// </summary>
    public float HardnessFactor { get; internal set; }
    /// <summary>
    /// The roughness factor of the surface for audio processing.
    /// </summary>
    public float RoughnessFactor { get; internal set; }
    /// <summary>
    /// The threshold for roughness in audio processing.
    /// </summary>
    public float RoughThreshold { get; internal set; }
    /// <summary>
    /// The threshold for hardness in audio processing.
    /// </summary>
    public float HardThreshold { get; internal set; }
    /// <summary>
    /// The velocity threshold for hard surface impacts in audio processing.
    /// </summary>
    public float HardVelocityThreshold { get; internal set; }
    /// <summary>
    /// The static impact volume for this surface.
    /// </summary>
    public float StaticImpactVolume { get; internal set; }
    /// <summary>
    /// The occlusion factor for audio, affecting how sound propagates through or around the surface.
    /// </summary>
    public float OcclusionFactor { get; internal set; }

    public override string ToString()
    {
        return $"PhysSurfacePropertiesAudioTrace {{ Reflectivity: {Reflectivity}, HardnessFactor: {HardnessFactor}, RoughnessFactor: {RoughnessFactor}, RoughThreshold: {RoughThreshold}, HardThreshold: {HardThreshold}, HardVelocityThreshold: {HardVelocityThreshold}, StaticImpactVolume: {StaticImpactVolume}, OcclusionFactor: {OcclusionFactor} }}";
    }
}

public class PhysSurfacePropertiesTrace
{
    /// <summary>
    /// The name of the surface.
    /// </summary>
    public string Name { get; internal set; } = "";
    /// <summary>
    /// The hashed value of the surface name for faster lookup.
    /// </summary>
    public uint NameHash { get; internal set; }
    /// <summary>
    /// The hashed value of the base surface name this surface is derived from.
    /// </summary>
    public uint BaseNameHash { get; internal set; }
    /// <summary>
    /// The index of this surface in the material list.
    /// </summary>
    public int ListIndex { get; internal set; }
    /// <summary>
    /// The index of the base surface in the material list.
    /// </summary>
    public int BaseListIndex { get; internal set; }
    /// <summary>
    /// Indicates whether this surface is hidden or internal.
    /// </summary>
    public bool Hidden { get; internal set; }
    /// <summary>
    /// The description of this surface and its properties.
    /// </summary>
    public string Description { get; internal set; } = "";
    /// <summary>
    /// The physics properties of this surface.
    /// </summary>
    public PhysSurfacePropertiesPhysicsTrace? Physics { get; internal set; } = null;
    /// <summary>
    /// The sound names associated with interactions on this surface.
    /// </summary>
    public PhysSurfacePropertiesSoundNamesTrace? AudioSounds { get; internal set; } = null;
    /// <summary>
    /// The audio processing parameters for this surface.
    /// </summary>
    public PhysSurfacePropertiesAudioTrace? AudioParams { get; internal set; } = null;

    public override string ToString()
    {
        return $"PhysSurfacePropertiesTrace {{ Name: \"{Name}\", NameHash: {NameHash}, BaseNameHash: {BaseNameHash}, ListIndex: {ListIndex}, BaseListIndex: {BaseListIndex}, Hidden: {Hidden}, Description: \"{Description}\", Physics: {Physics}, AudioSounds: {AudioSounds}, AudioParams: {AudioParams} }}";
    }
}