Shader "Custom/RemoveBackground"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _RemoveColor ("Remove Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _RemoveColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
                if (abs(col.r - _RemoveColor.r) < 0.1 &&
                    abs(col.g - _RemoveColor.g) < 0.1 &&
                    abs(col.b - _RemoveColor.b) < 0.1)
                {
                    col.a = 0;
                }
                return col;
            }
            ENDCG
        }
    }
}
