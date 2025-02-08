Shader "Custom/ScopeWithDarkEdgeTransparentCenter"
{
    Properties
    {
        _MainTex ("Scope Image (RGBA)", 2D) = "white" { }
        _Darkness ("Edge Darkness", Range(0, 1)) = 1.0 // 외부 어두운 정도
        _ScopeCenter ("Scope Center", Vector) = (0.5, 0.5, 0, 0) // 스코프의 중심 (0~1, 0~1)
        _ScopeRadius ("Scope Radius", Float) = 0.2 // 스코프의 크기
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" }

        Pass
        {
            // 알파 블렌딩 추가
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 pos : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _Darkness;
            float4 _ScopeCenter; // 스코프의 중심
            float _ScopeRadius; // 스코프 반지름

            // 버텍스 쉐이더
            v2f vert(appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            // 프래그먼트 쉐이더
            half4 frag(v2f i) : SV_Target {
                // 텍스처에서 색상 가져오기
                half4 col = tex2D(_MainTex, i.texcoord);

                // 스코프 바깥쪽을 어둡게 처리 (투명도를 유지하면서 어두운 색을 적용)
                float2 scopeCenter = _ScopeCenter.xy; // 스코프의 중심
                float dist = distance(i.texcoord, scopeCenter); // 현재 픽셀과 스코프 중심과의 거리

                if (dist > _ScopeRadius) {
                    // 바깥쪽 영역의 색상을 강제로 어두운 색으로 만듦
                    col.rgb *= _Darkness; // 색상을 어둡게 조정 (0~1 사이 값으로 조절)
                    col.a = _Darkness; // 외부의 투명도 조정
                }
                else {
                    // 원 안쪽은 완전히 투명하게 설정
                    col.a = 0.0;
                }

                return col;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
