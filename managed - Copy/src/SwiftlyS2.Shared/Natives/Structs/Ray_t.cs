using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct LineTrace
{
    public Vector StartOffset;
    public float Radius;
};

[StructLayout(LayoutKind.Sequential, Size = 16)]
public struct SphereTrace
{
    public Vector Center;
    public float Radius;
}

[StructLayout(LayoutKind.Sequential, Size = 24)]
public struct HullTrace
{
    public Vector Mins;
    public Vector Maxs;
}

// i hate you C# for not letting me do Center[2]
[StructLayout(LayoutKind.Sequential, Size = 28)]
public struct CapsuleTrace
{
    public Vector Center0;
    public Vector Center1;
    public float Radius;
}

[StructLayout(LayoutKind.Sequential, Size = 36)]
public unsafe struct MeshTrace
{
    public Vector Mins;
    public Vector Maxs;
    public Vector* Vertices;
    public int NumVertices;
}

[StructLayout(LayoutKind.Explicit)]
public struct Ray_t
{
    [FieldOffset(0x0)] public LineTrace Line;
    [FieldOffset(0x0)] public SphereTrace Sphere;
    [FieldOffset(0x0)] public HullTrace Hull;
    [FieldOffset(0x0)] public CapsuleTrace Capsule;
    [FieldOffset(0x0)] public MeshTrace Mesh;
    [FieldOffset(0x34)] public RayType_t Type;

    public void Init(Vector StartOffset)
    {
        Line.StartOffset = StartOffset;
        Line.Radius = 0.0f;
        Type = RayType_t.RAY_TYPE_LINE;
    }

    public void Init(Vector Center, float Radius)
    {
        if (Radius > 0.0f)
        {
            Sphere.Center = Center;
            Sphere.Radius = Radius;
            Type = RayType_t.RAY_TYPE_SPHERE;
        }
        else
        {
            Init(Center);
        }
    }

    public void Init(Vector Mins, Vector Maxs)
    {
        if (Mins != Maxs)
        {
            Hull.Mins = Mins;
            Hull.Maxs = Maxs;
            Type = RayType_t.RAY_TYPE_HULL;
        }
        else
        {
            Init(Mins);
        }
    }

    public void Init(Vector CenterA, Vector CenterB, float Radius)
    {
        if (CenterA != CenterB)
        {
            if (Radius > 0.0f)
            {
                Capsule.Center0 = CenterA;
                Capsule.Center1 = CenterB;
                Capsule.Radius = Radius;
                Type = RayType_t.RAY_TYPE_CAPSULE;
            }
            else
            {
                Init(CenterA, CenterB);
            }
        }
        else
        {
            Init(CenterA, Radius);
        }
    }

    public unsafe void Init(Vector Mins, Vector Maxs, Vector* Vertices, int NumVertices)
    {
        Mesh.Mins = Mins;
        Mesh.Maxs = Maxs;
        Mesh.Vertices = Vertices;
        Mesh.NumVertices = NumVertices;
        Type = RayType_t.RAY_TYPE_MESH;
    }
}