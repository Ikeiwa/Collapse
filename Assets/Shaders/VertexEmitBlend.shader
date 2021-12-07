Shader "Unlit/VertexEmit Blend"
{
    Properties
    {
        _EmitPower("Emit Power", Float) = 5
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100
    	
        Pass {
	        ColorMask 0
	    }

        Pass
        {
            ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
        	ColorMask RGB
        	
            Stencil{
                Ref 3
                Comp NotEqual
                Pass Keep
            }
        	
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
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
                UNITY_FOG_COORDS(1)
            };

            v2f vert (appdata v)
            {
                v2f o;
                curveWorld(v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;

                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float _EmitPower;

            fixed4 frag (v2f i) : SV_Target
            {
				float4 col = i.color * _EmitPower;
                UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
            }
            ENDCG
        }
    	
        Pass {
            ColorMask 0
        	
            Stencil{
                Ref 3
                Comp Always
                Pass Replace
            }
        }
    }
}
