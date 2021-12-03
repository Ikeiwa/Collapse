Shader "Unlit/Zone Terrain"
{
    Properties
    {
        _Color("Color",Color) = (0,0,0,0)
        _AmbientLighting("Ambient Lighting",Range(0,1)) = 0.5
        _MainTex("Tris Texture", 2D) = "white" {}
        _Tiling("Tiling",Vector) = (1,1,0,0)
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

            sampler2D _MainTex;
            float2 _Tiling;

            v2f vert (appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.pos);

                v.pos.y += (snoise(worldPos.xz * 0.005f) * v.color.r * 10 + v.color.g*8) * v.color.a;

                curveWorld(v.pos);

                //float3 pivot = unity_ObjectToWorld._m03_m13_m23;

                o.pos = UnityObjectToClipPos(v.pos);
                o.uv = mul(unity_ObjectToWorld, v.pos).xz * _Tiling;
                o.normal = UnityObjectToWorldNormal(v.normal);
                
                o.color = v.color;
                TRANSFER_SHADOW(o)
                return o;
            }

            float _AmbientLighting;
            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uvFloor = floor(i.uv);

                float3 tries = tex2D(_MainTex, i.uv).rgb;
                float2 triUV = uvFloor + tries.xy;

                float triMask = tries.b;

                float noise = smoothstep(0.4,0.6,1-saturate(snoise(float3(triUV * 0.02,_Time.x))) - i.color.r);
                float maskedTri = step(triMask, noise);

                clip(maskedTri - 0.01f);

                fixed shadow = SHADOW_ATTENUATION(i);
                float NdotL = saturate(dot(i.normal, _WorldSpaceLightPos0)) * shadow;
                NdotL = saturate(NdotL + _AmbientLighting);

                float4 vertColor = lerp(i.color, 1, i.color.a);
                vertColor = lerp(vertColor, float4(0, 3, 3, 1), step(noise, 0.94));

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
