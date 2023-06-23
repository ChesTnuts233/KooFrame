Shader "Custom/GridShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GridSize ("Grid Size", Range(0.1, 10)) = 1
        _Translation ("Translation", Vector) = (0, 0, 0, 0)
    }
 
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            float4 _Translation;
            float _GridSize;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + _Translation);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * _GridSize;
                float2 gridUV = frac(uv);
                float2 gridLine = fwidth(uv) * 0.5;
 
                float2 gridX = step(gridLine, gridUV) * step(gridUV, 1.0 - gridLine);
                float2 gridY = step(gridLine, gridUV) * step(gridUV, 1.0 - gridLine);
 
                float2 grid = min(gridX, gridY);
                float alpha = 1.0 - smoothstep(0.45, 0.55, grid);
 
                return fixed4(0, 0, 0, alpha);
            }
            ENDCG
        }
    }
}
