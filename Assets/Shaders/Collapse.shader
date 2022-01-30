Shader "Unlit/Collapse"
{
    Properties
    {
        [NoScaleOffset] _Tex("Cubemap   (HDR)", Cube) = "grey" {}
    	_Progress("Progress",Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Overlay"}
        LOD 100
    	ZTest Off
    	ZWrite Off
    	
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "./noiseSimplex.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 uvSky : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uvSky = normalize(UnityWorldSpaceViewDir(worldPos));
                o.uv = v.uv;
                return o;
            }

            samplerCUBE _Tex;
            float _Progress;

            fixed4 frag(v2f i) : SV_Target
            {
                float noise = (snoise(float3(i.uv * 10,_Time.x)) / 2 + 0.5f)*0.05f;

                clip(_Progress-(i.uv.y+ noise));

                return texCUBE(_Tex, i.uvSky);
            }
            ENDCG
        }
    }
}
