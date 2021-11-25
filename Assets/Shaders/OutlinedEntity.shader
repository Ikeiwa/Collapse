Shader "Unlit/OutlinedEntity"
{
    Properties
    {
        _Color("Color",Color) = (0,0,0,0)
        _AmbientLighting("Ambient Lighting",Range(0,1)) = 0.5
        _OutlineThickness("Outline Thickness",Float) = 0.025
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.color = v.color;
                return o;
            }

            float _AmbientLighting;
            float4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                float3 posDY = ddy(i.worldPos);
                float3 posDX = ddx(i.worldPos);

                float3 normal = normalize(cross(posDY, posDX));

                float NdotL = saturate(dot(normal, _WorldSpaceLightPos0));
                NdotL = saturate(NdotL + _AmbientLighting);

                return _Color* i.color * NdotL;
            }
            ENDCG
        }
    	
        Pass
        {
            Name "Outline"
        	Cull Front
        	
            Stencil{
                Ref 1
                Comp NotEqual
                Pass Zero
            }
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float4 grabPos : TEXCOORD0;
            };

            float _OutlineThickness;

            v2f vert(appdata v)
            {
                v2f o;
                v.vertex.xyz += v.normal * _OutlineThickness;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                o.color = v.color;
                return o;
            }

            sampler2D _GrabTerrain;

            fixed4 frag(v2f i) : SV_Target
            {
                float3 background = tex2Dproj(_GrabTerrain,i.grabPos);
                float value = round(1-saturate((background.r + background.g + background.b) / 3));

                return 0;
            }
            ENDCG
        }
    }
}
