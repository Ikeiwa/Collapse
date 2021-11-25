Shader "Unlit/Pillar"
{
    Properties
    {
        _Color("Color",Color) = (0,0,0,0)
        _AmbientLighting("Ambient Lighting",Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1"}
        LOD 100
    	
        GrabPass{"_GrabTerrain"}

        Pass
        {
        	Name "Main"
        	
        	Stencil{
				Ref 1
        		Comp Always
        		Pass Replace
            }
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "./noiseSimplex.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                fixed4 color : COLOR;
            };

            float nrand(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;

                float3 pivot = unity_ObjectToWorld._m03_m13_m23;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                float terrain = snoise(o.worldPos.xz * 0.1f) * 0.25f;
                float height = nrand(pivot.xz);

                float floating = sin(_Time.y + v.color.b*2 + height*5) * 0.15f * v.color.g;

                v.vertex.y += ((height+ terrain) * v.color.r) + floating;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                o.color = v.color;
                return o;
            }

            float _AmbientLighting;
            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                float3 posDY = ddy(i.worldPos);
                float3 posDX = ddx(i.worldPos);

                float3 normal = normalize(cross(posDY, posDX));

                float NdotL = saturate(dot(normal, _WorldSpaceLightPos0));
                NdotL = saturate(NdotL + _AmbientLighting);

                return _Color * NdotL;
            }
            ENDCG
        }
    }
}
