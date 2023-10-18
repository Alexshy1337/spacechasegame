Shader "Unlit/GridShader"
{
    Properties
    {
        _MarginX("_MarginX", Float) = -0.5//amount of units to move
        _MarginY("_MarginY", Float) = -0.5
        _ColSize("Column Size (Width)", Float) = 1
        _RowSize("Row Size (Hieght)", Float) = 1
        _BorderSize("Borders Size", Range(0,1)) = 0.02
        _GridColor("Grid Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            //Offset -10, -10
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _MarginX;
            float _MarginY;
            float _ColSize;
            float _RowSize;
            float _BorderSize;
            float4 _GridColor; // Color for grid

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


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = mul(unity_ObjectToWorld, v.vertex).xy;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                //i.uv.xy

                float2 xyFMod = float2(
                    fmod(i.uv.x + _MarginX, _ColSize),
                    fmod(i.uv.y + _MarginY, _RowSize)
                    );

                float x1 = _BorderSize/2;
                float x2 = _ColSize - _BorderSize/2;
                float y1 = _BorderSize/2;
                float y2 = _RowSize - _BorderSize/2;
                float alphaX = step(abs(xyFMod.x), x1) + step(x2, abs(xyFMod.x));
                float alphaY = step(abs(xyFMod.y), y1) + step(y2, abs(xyFMod.y));

                return float4(_GridColor.xyz, _GridColor.w * (alphaX + alphaY));
            }
            ENDCG
        }
    }
}
