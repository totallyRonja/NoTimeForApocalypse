// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "2D/Texture Blend"
 {  
     Properties
     {
        [PerRendererData]_MainTex ("Sprite Texture", 2D) = "white" {}
		_TintMult("Tint Multiplier", Float) = 0
		_TintTex ("Tint Texture", 2D) = "white" {}
        _SubTexR ("Sprite Texture", 2D) = "red" {}
        _SubTexG ("Sprite Texture", 2D) = "green" {}
        _SubTexB ("Sprite Texture", 2D) = "blue" {}
     }
     SubShader
     {
         Tags 
         { 
             "RenderType" = "Opaque" 
             "Queue" = "Transparent" 
         }
 
         Pass
         {
             ZWrite Off
             Blend SrcAlpha OneMinusSrcAlpha 
  
             CGPROGRAM
             #include "UnityCG.cginc"

             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
			 
			 float _TintMult;

             sampler2D _MainTex;
			 sampler2D _TintTex;
             sampler2D _SubTexR;
             sampler2D _SubTexG;
             sampler2D _SubTexB;

             float4 _SubTexR_ST;
             float4 _SubTexG_ST;
             float4 _SubTexB_ST;
 
             struct Vertex
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
             };
     
             struct Fragment
             {
                 float4 vertex : POSITION;
                 float2 uv_MainTex : TEXCOORD0;
                 float2 uvr : TEXCOORD1;
                 float2 uvg : TEXCOORD2;
                 float2 uvb : TEXCOORD3;
             };
  
             Fragment vert(Vertex v)
             {
                 Fragment o;
     
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv_MainTex = v.uv_MainTex;
                 o.uvr = TRANSFORM_TEX(v.uv_MainTex, _SubTexR);
                 o.uvg = TRANSFORM_TEX(v.uv_MainTex, _SubTexG);
                 o.uvb = TRANSFORM_TEX(v.uv_MainTex, _SubTexB);

                 return o;
             }
                                                     
             float4 frag(Fragment IN) : COLOR
             {
                 half4 map = tex2D (_MainTex, IN.uv_MainTex);
                 normalize(map);
                 float4 o = float4(0, 0, 0, 0);
                 o += map.r * tex2D (_SubTexR, IN.uvr);
                 o += map.g * tex2D (_SubTexG, IN.uvg);
                 o += map.b * tex2D (_SubTexB, IN.uvb);
                 
				 o *=  tex2D(_TintTex, IN.uv_MainTex) * _TintMult;
				 
				 o.a = map.a;
                 
                 return o;
             }
 
             ENDCG
         }
     }
 }