Shader "Unlit/Plain Terrain"
{
    Properties
    {
        _Color("Color",Color) = (0,0,0,0)
        _AmbientLighting("Ambient Lighting",Range(0,1)) = 0.5
	    

        _CurveDirection("Curve Direction",Vector) = (0,0,0,0)
        _CurveDistance("Curve Distance",Float) = 50
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        LOD 100
    	
    	CGINCLUDE
        #include "./noiseSimplex.cginc"
		#include "./worldCurve.cginc"

        float nrand(float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
        }
        ENDCG

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
        	Name "Main"
        	
            CGPROGRAM
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma vertex vert
            #pragma fragment frag
            #include "AutoLight.cginc"
            #include "UnityCG.cginc"
			

            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                SHADOW_COORDS(1)
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.pos);

                v.pos.y += (snoise(worldPos.xz * 0.005f) * v.color.r * 10 + v.color.g*8) * v.color.a;

                curveWorld(v.pos);

                //float3 pivot = unity_ObjectToWorld._m03_m13_m23;

                o.pos = UnityObjectToClipPos(v.pos);
                o.uv = v.uv;
                o.normal = UnityObjectToWorldNormal(v.normal);
                
                o.color = v.color;
                TRANSFER_SHADOW(o)
                return o;
            }

            float _AmbientLighting;
            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed shadow = SHADOW_ATTENUATION(i);
                float NdotL = saturate(dot(i.normal, _WorldSpaceLightPos0)) * shadow;
                NdotL = saturate(NdotL + _AmbientLighting);

                float4 vertColor = lerp(i.color, 1, i.color.a);

                return _Color * NdotL * vertColor;
            }
            ENDCG
        }
    	
        Pass
	    {
	        Tags {"LightMode" = "ShadowCaster"}

	        CGPROGRAM
	        #pragma vertex vert
	        #pragma fragment frag
	        #pragma multi_compile_shadowcaster
	        #include "UnityCG.cginc"


	        struct v2f {
	            V2F_SHADOW_CASTER;
	        };

	        v2f vert(appdata_full v)
	        {
	            v2f o;

                curveWorld(v.vertex);

	            TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
	            return o;
	        }

	        float4 frag(v2f i) : SV_Target
	        {
	            SHADOW_CASTER_FRAGMENT(i)
	        }
	        ENDCG
	    }
    }
	
    
}
