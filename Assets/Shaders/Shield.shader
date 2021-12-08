Shader "Unlit/Shield"
{
    Properties
    {
    	_Color("Color",Color) = (1,1,1,1)
    	_Progress("Progress",Range(0,1)) = 1
    	_Break("Break",Range(0,1)) = 0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
    	Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "noiseSimplex.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 noise : TEXCOORD0;
            };

            float _Progress;
            float _Break;

            v2f vert (appdata v)
            {
                v2f o;

                o.noise = float2(snoise((v.color.rgb * 2 - 1)-float3(0,0,_Time.y))+0.5f,v.color.a- _Break);

                v.vertex.xyz += v.normal * _Break;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                clip(step(i.noise.r,i.noise.g-_Progress) - 0.1f);
                return _Color;
            }
            ENDCG
        }
    }
}
