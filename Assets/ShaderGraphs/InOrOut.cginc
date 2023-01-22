
#ifndef _IN_OR_OUT
#define _IN_OR_OUT


void IsInRange_float(UnityTexture2D LinearTexture, float Size, float2 Position, out bool Out)
{
    float2 uv = {0, 0.5};
    for(int i = 0; i<Size; i++)
    {
        half4 color = tex2D(LinearTexture, uv);
    }
    Out = false;
}


#endif