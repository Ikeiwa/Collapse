
Shader "Unlit/Warning"
{
    Properties
    {
    	_Color("Color",Color) = (1,1,1,1)
        _EmitPower("Emit Power", Float) = 5
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
    	Cull Off
    	Offset -1 , 10

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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                curveWorld(v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float _EmitPower;
            fixed4 _Color;

            fixed4 frag(v2f i) : SV_Target
            {
                float4 col = _Color * _EmitPower;
                col.a *= i.uv.y;
                return col;
            }
            ENDCG
        }
    }
}
