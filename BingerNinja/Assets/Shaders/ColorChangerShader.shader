Shader "BingerNinja/ColorChangerShader"
{
    Properties 
    {
        _Color1 ("Color1", Color) = (1, 1, 1, 1)
        _Color2 ("Color2", Color) = (1, 1, 1, 1)
        _Color3 ("Color3", Color) = (1, 1, 1, 1)
        _Grey1 ("Grey1", Color) = (0.478, 0.478, 0.478, 1)
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

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;

            float4 _Grey1;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                if(i.color.r == 0.1)           
                    return 1;
                return 0;
            }
            ENDCG
        }
    }
}
