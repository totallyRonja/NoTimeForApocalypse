Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_SubTexR ("Red Replace Texture", 2D) = "white" {}
		_SubTexG ("Green Replace Texture", 2D) = "white" {}
		_SubTexB ("Blue Replace Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
				float2 uvr : TEXCOORD1;
				float2 uvg : TEXCOORD2;
				float2 uvb : TEXCOORD3;
                
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
			sampler2D _SubTexR;
			sampler2D _SubTexG;
			sampler2D _SubTexB;
            float4 _MainTex_ST;
			float4 _SubTexR_ST;
			float4 _SubTexG_ST;
			float4 _SubTexB_ST;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvr = TRANSFORM_TEX(v.uv, _SubTexR);
				o.uvg = TRANSFORM_TEX(v.uv, _SubTexG);
				o.uvb = TRANSFORM_TEX(v.uv, _SubTexB);
                return o;
            }
            
            fixed3 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed3 map = tex2D(_MainTex, i.uv).rgb;
				normalize(map);

				fixed3 col = map.r * tex2D(_SubTexR, i.uvr).rgb;
				col += map.g * tex2D(_SubTexG, i.uvg).rgb;
				col += map.b * tex2D(_SubTexB, i.uvb).rgb;
				
                return col;
            }
            ENDCG
        }
    }
}