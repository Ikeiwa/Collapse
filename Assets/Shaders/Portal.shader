Shader "Unlit/Portal"
{
    Properties
    {
        [NoScaleOffset] _Tex("Cubemap   (HDR)", Cube) = "grey" {}
    	_Progress("Progress",Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry+1"}
        LOD 100
    	
        GrabPass{"_GrabTerrain"}
    	
    	CGINCLUDE
        #include "./noiseSimplex.cginc"

        float nrand(float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
        }
        ENDCG

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
        	Name "Main"
        	
        	Stencil{
				Ref 2
        		Comp Always
        		Pass Replace
            }
        	
            CGPROGRAM
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
			

            struct appdata
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
                float3 normal : NORMAL;
                fixed4 color : COLOR;
            };

            float _Progress;

            void RotateAboutAxis(float3 In, float3 Axis, float Rotation, out float3 Out)
            {
                float s = sin(Rotation);
                float c = cos(Rotation);
                float one_minus_c = 1.0 - c;

                Axis = normalize(Axis);
                float3x3 rot_mat =
                { one_minus_c * Axis.x * Axis.x + c, one_minus_c * Axis.x * Axis.y - Axis.z * s, one_minus_c * Axis.z * Axis.x + Axis.y * s,
                    one_minus_c * Axis.x * Axis.y + Axis.z * s, one_minus_c * Axis.y * Axis.y + c, one_minus_c * Axis.y * Axis.z - Axis.x * s,
                    one_minus_c * Axis.z * Axis.x - Axis.y * s, one_minus_c * Axis.y * Axis.z + Axis.x * s, one_minus_c * Axis.z * Axis.z + c
                };
                Out = mul(rot_mat, In);
            }

            v2f vert (appdata v)
            {
                v2f o;

                float4 basePos = UnityObjectToClipPos(v.pos);

                float3 offset = 0;

                offset.xy += _Progress * (v.color.xy - 0.5f);
                offset.z -= _Progress;

                RotateAboutAxis(v.pos.xyz, float3(0, 1, 0), (v.color.b*2-1) * _Progress * v.color.a, v.pos.xyz);
                v.pos.xyz += offset * v.color.a;

                o.pos = UnityObjectToClipPos(v.pos);


                o.uv = ComputeGrabScreenPos(basePos);




                o.normal = UnityObjectToWorldNormal(v.normal);
                
                o.color = v.color;
                return o;
            }

            sampler2D _GrabTerrain;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 background = tex2Dproj(_GrabTerrain,i.uv);
                float4 col = lerp(1,background, i.color.a);
                return col;
            }
            ENDCG
        }
    	
    	
Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            Name "Main"

            Stencil{
                Ref 2
                Comp NotEqual
                Pass Zero
            }

            CGPROGRAM
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            struct appdata
            {
                float4 pos : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
                float3 texcoord : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;

                o.pos = UnityObjectToClipPos(v.pos);
                float3 worldPos = mul(unity_ObjectToWorld, v.pos);
                o.texcoord = normalize(UnityWorldSpaceViewDir(worldPos));
                o.color = v.color;
                return o;
            }

            samplerCUBE _Tex;

            fixed4 frag(v2f i) : SV_Target
            {
                clip(i.color.a - 0.1);
                return texCUBE(_Tex, i.texcoord);
            }
            ENDCG
        }
    
    }
	
    
}
