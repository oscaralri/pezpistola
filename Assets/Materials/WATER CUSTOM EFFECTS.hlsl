
/////////////////////////////////////////////////////
/////////////////// VORONOI BORDER //////////////////
/////////////////////////////////////////////////////
float2 random(float2 UV, float offset)
{
    UV = frac(UV * 234.34 + 123.45);
    UV = frac(sin(dot(UV, float2(12.9898, 78.233))) * 43758.5453);
    return float2(sin(UV.y * offset), cos(UV.x * offset)) * 0.5 + 0.5;
}

void Voronoi_2Distances_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float2 A, out float2 B)
{
    float2 g = floor(UV * CellDensity);
    float2 f = frac(UV * CellDensity);
    float F1 = 8.0; 
    float F2 = 8.0;

    for (int y = -1; y <= 1; y++)
    {
        for (int x = -1; x <= 1; x++)
        {
            float2 lattice = float2(x, y);
            float2 offset = random(lattice + g, AngleOffset);
            float d = distance(lattice + offset, f);
            if (d < F1)
            {
                B = A;
                F1 = d;
                A = lattice + offset - f;
            }
            else if (d < F2)
            {
                F2 = d;
                B = lattice + offset - f;
            }
        }
    }

    Out = F1;
}

void Voronoi_GetBorder_float(float2 A, float2 B, float Thickness, out float Border)
{
    float dis = dot(0.5*(A+B), normalize(B-A));
    Border = 1.0 - smoothstep(0.0, Thickness, dis);
}

void Voronoi_Border_float(float2 UV, float AngleOffset, float CellDensity, float Thickness, out float Border)
{
    float2 A;
    float2 B;
    float F1;
    Voronoi_2Distances_float(UV, AngleOffset, CellDensity, F1, A, B);
    Voronoi_GetBorder_float(A, B, Thickness, Border);
}


////////////////////////////////////////////////////
////////////////// SMOOTHED NOISE //////////////////
////////////////////////////////////////////////////
void random_2_1_float(float2 i, out float o)
{
    uint r;
    uint2 v = (uint2) (int2) round(i);
    v.y ^= 1103515245U;
    v.x += v.y;
    v.x *= v.y;
    v.x ^= v.x >> 5u;
    v.x *= 0x27d4eb2du;
    o = v.x * (1.0 / float(0xffffffff));
}

void SimpleNoiseValue_float(float2 uv, float Out)
{
    float2 i = floor(uv);
    float2 f = frac(uv);
    f = f * f * (3.0 - 2.0 * f);
    uv = abs(frac(uv) - 0.5);
    float2 c0 = i + float2(0.0, 0.0);
    float2 c1 = i + float2(1.0, 0.0);
    float2 c2 = i + float2(0.0, 1.0);
    float2 c3 = i + float2(1.0, 1.0);
    float r0; random_2_1_float(c0, r0);
    float r1; random_2_1_float(c1, r1);
    float r2; random_2_1_float(c2, r2);
    float r3; random_2_1_float(c3, r3);
    float bottomOfGrid = lerp(r0, r1, f.x);
    float topOfGrid = lerp(r2, r3, f.x);
    Out = lerp(bottomOfGrid, topOfGrid, f.y);
}

void DistortUV_float(float2 UV, float Amount, out float2 Out)
{
    float time = _Time.y;

    UV.y += Amount * 0.01 * (sin(UV.x * 3.5 + time * 0.35) + sin(UV.x * 4.6 + time * 1.05) + sin(UV.x * 7.3 + time * 0.45)) / 3.0;
    UV.x += Amount * 0.12 * (sin(UV.y * 4.0 + time * 0.50) + sin(UV.y * 6.8 + time * 0.75) + sin(UV.y * 11.3 + time * 0.2)) / 3.0;
    UV.y += Amount * 0.12 * (sin(UV.x * 4.2 + time * 0.64) + sin(UV.x * 6.3 + time * 1.65) + sin(UV.x * 8.2 + time * 0.45)) / 3.0;

    Out = UV;
}