Shader "Unlit/Logo"
{
    Properties
    {
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
			#include "../noiseSimplex.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float noise : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.noise = step(snoise(v.uv - float2(_Time.y, 0)) / 2 + 0.5,1-v.uv.x);
                o.noise = lerp(1, o.noise, o.color.a);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                clip(i.noise - 0.5f);

                float4 col = lerp(float4(0,1,0.95,1),1,i.color.r);
                col = lerp(col, 0, i.color.b);
                return col;
            }
            ENDCG
        }
    }
}
