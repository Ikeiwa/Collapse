Shader "Unlit/VertexEmit"
{
    Properties
    {
        _EmitPower("Emit Power", Float) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
    	Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "worldCurve.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                curveWorld(v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }

            float _EmitPower;

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color * _EmitPower;
            }
            ENDCG
        }
    }
}